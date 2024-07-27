using System.Data;
using System.Data.Common;
using Dapper;
using Npgsql;

namespace Framework.DataSource.DynamicParameter
{
    public class PostgreSqlParameter : SqlMapper.IDynamicParameters
    {
        public List<string> ParamNames { get; private set; } = new List<string>();
        private List<DbParameter> _params = new List<DbParameter>();

        public void Build(object? param)
        {
            if (param == null)
                return;

            ParamNames = new List<string>();
            _params = new List<DbParameter>();

            foreach (var p in param.GetType().GetProperties())
            {
                var value = p.GetValue(param);
                var parameterName = p.Name;

                ParamNames.Add(parameterName);

                switch (p.PropertyType)
                {
                    case Type when p.PropertyType == typeof(NpgsqlParameter):
                        var sqlParam = (NpgsqlParameter)value!;
                        if (sqlParam.ParameterName != "null")
                        {
                            sqlParam.ParameterName = parameterName;
                            _params.Add(sqlParam);
                        }
                        break;

                    case Type when p.PropertyType == typeof(DateTime):
                        if (value is DateTime dateTimeVal)
                        {
                            _params.Add(dateTimeVal == DateTime.MinValue
                                ? new NpgsqlParameter(parameterName, DBNull.Value)
                                : new NpgsqlParameter(parameterName, dateTimeVal));
                        }
                        break;

                    default:
                        _params.Add(new NpgsqlParameter(parameterName, value ?? DBNull.Value));
                        break;
                }
            }
        }
        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            if (command is NpgsqlCommand npgsqlCommand)
            {
                npgsqlCommand.Parameters.AddRange(_params.ToArray());
            }
        }
    }
}

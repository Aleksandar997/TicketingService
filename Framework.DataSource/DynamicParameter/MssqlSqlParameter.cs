using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Framework.DataSource.DynamicParameter
{
    public class MssqlSqlParameter : SqlMapper.IDynamicParameters
    {
        public List<string> ParamNames { get; private set; } = new List<string>();
        private readonly List<SqlParameter> _params = new List<SqlParameter>();

        public MssqlSqlParameter(object? param)
        {
            if (param == null)
                return;

            _params.Clear();
            _params.TrimExcess();

            ParamNames.Clear();
            ParamNames.TrimExcess();

            if (param == null) return;

            foreach (var p in param.GetType().GetProperties())
            {
                var value = p.GetValue(param);
                var parameterName = p.Name;

                ParamNames.Add(parameterName);

                switch (p.PropertyType)
                {
                    case Type when p.PropertyType == typeof(SqlParameter):
                        var sqlParam = (SqlParameter)value!;
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
                                ? new SqlParameter(parameterName, DBNull.Value)
                                : new SqlParameter(parameterName, dateTimeVal));
                        }
                        break;

                    default:
                        _params.Add(new SqlParameter(parameterName, value ?? DBNull.Value));
                        break;
                }
            }
        }
        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            if (command is SqlCommand sqlCommand)
            {
                sqlCommand.Parameters.AddRange(_params.ToArray());
            }
        }
    }
}

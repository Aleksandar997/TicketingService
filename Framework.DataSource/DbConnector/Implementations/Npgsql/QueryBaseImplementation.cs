using System.Data;
using System.Text;
using Npgsql;

namespace Framework.DataSource.DbConnector.Implementations.Npgsql
{
    public abstract class QueryBaseImplementation 
    {
        protected (CommandType commandType, string sql) GetSqlCommand(SqlCommandType sqlCommandType, string source, List<string> parameters)
        {
            switch (sqlCommandType)
            {
                case SqlCommandType.Function:
                    var sb = new StringBuilder();
                    sb.AppendLine("SELECT");
                    sb.AppendLine("*");
                    sb.AppendLine("FROM");
                    sb.AppendLine(source);
                    sb.AppendLine("(");

                    for (int i = 0; i < parameters.Count; i++)
                    {
                        if (i > 0)
                        {
                            sb.Append(",");
                        }
                        sb.Append("@");
                        sb.Append(parameters[i]);
                    }

                    sb.AppendLine(")");
                    return (CommandType.Text, sb.ToString());
                case SqlCommandType.StoredProcedure:
                    throw new NotImplementedException();
                case SqlCommandType.Query:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        protected async Task<(T? data, bool success)> Execute<T>(Func<Task<T>> func)
        {
            try
            {
                var res = await func();
                return (res, true);
            }
            catch (NpgsqlException)
            {
                //log
                //throw;
                return (default, false);
            }
            catch (Exception)
            {
                //log
                //return;
                return (default, false);
            }
        }
    }
}

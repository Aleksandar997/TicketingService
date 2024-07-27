using Dapper;
using Framework.DataSource.DynamicParameter;
using Npgsql;
using static Dapper.SqlMapper;

namespace Framework.DataSource.DbConnector.Implementations.Npgsql
{
    public class NpgsqlDbConnector : IDbConnector
    {
        private readonly string _connectionString;
        public NpgsqlDbConnector(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IQueryAsyncImplementation QueryAsync()
        {
            return new QueryAsyncImplementation(_connectionString);
        }

        public IQueryMultipleAsyncImplementation QueryMultipleAsync()
        {
            return new QueryMultipleAsyncImplementation(_connectionString);
        }

        public IQuerySingleAsyncImplementation QuerySingleAsync()
        {
            return new QuerySingleAsyncImplementation(_connectionString);
        }

        public class QueryAsyncImplementation : QueryBaseImplementation, IQueryAsyncImplementation
        {
            private int? _commandTimeout = null;
            private PostgreSqlParameter _param = new PostgreSqlParameter();
            private readonly string _connectionString;
            public QueryAsyncImplementation(string connectionString)
            {
                _connectionString = connectionString;
            }

            public IQueryAsyncImplementation AddCommandTimeout(int commandTimeout)
            {
                _commandTimeout = commandTimeout;
                return this;
            }

            public IQueryAsyncImplementation AddParameters(object param)
            {
                _param.Build(param);
                return this;
            }

            public Task<(IEnumerable<T>? data, bool success)> ExecuteAsync<T>(string source, SqlCommandType sqlCommandType)
            {
                var sqlCommand = GetSqlCommand(sqlCommandType, source, _param.ParamNames);

                return Execute(async () =>
                {
                    using (var _connection = new NpgsqlConnection(_connectionString))
                    {
                        return await _connection.QueryAsync<T>(sqlCommand.sql, _param, commandTimeout: _commandTimeout, commandType: sqlCommand.commandType);
                    }
                });
            }
        }

        public class QueryMultipleAsyncImplementation : QueryBaseImplementation, IQueryMultipleAsyncImplementation
        {
            private int? _commandTimeout = null;
            private PostgreSqlParameter _param = new PostgreSqlParameter();
            private readonly string _connectionString;
            public QueryMultipleAsyncImplementation(string connectionString)
            {
                _connectionString = connectionString;
            }

            public IQueryMultipleAsyncImplementation AddCommandTimeout(int commandTimeout)
            {
                _commandTimeout = commandTimeout;
                return this;
            }

            public IQueryMultipleAsyncImplementation AddParameters(object param)
            {
                _param.Build(param);
                return this;
            }
            public Task<(T? data, bool success)> ExecuteAsync<T>(string source, SqlCommandType sqlCommandType, Func<GridReader, T> readMap)
            {
                var sqlCommand = GetSqlCommand(sqlCommandType, source, _param.ParamNames);

                return Execute(async () =>
                {
                    using (var _connection = new NpgsqlConnection(_connectionString))
                    {
                        var res = await _connection.QueryMultipleAsync(sqlCommand.sql, _param, commandTimeout: _commandTimeout, commandType: sqlCommand.commandType);
                        return readMap(res);
                    }
                });
            }
        }

        public class QuerySingleAsyncImplementation : QueryBaseImplementation, IQuerySingleAsyncImplementation
        {
            private int? _commandTimeout = null;
            private PostgreSqlParameter _param = new PostgreSqlParameter();
            private readonly string _connectionString;
            public QuerySingleAsyncImplementation(string connectionString)
            {
                _connectionString = connectionString;
            }

            public IQuerySingleAsyncImplementation AddCommandTimeout(int commandTimeout)
            {
                _commandTimeout = commandTimeout;
                return this;
            }

            public IQuerySingleAsyncImplementation AddParameters(object param)
            {
                _param.Build(param);
                return this;
            }
            public Task<(T? data, bool success)> ExecuteAsync<T>(string source, SqlCommandType sqlCommandType)
            {
                var sqlCommand = GetSqlCommand(sqlCommandType, source, _param.ParamNames);

                return Execute(async () =>
                {
                    using (var _connection = new NpgsqlConnection(_connectionString))
                    {
                        return await _connection.QuerySingleAsync<T>(sqlCommand.sql, _param, commandTimeout: _commandTimeout, commandType: sqlCommand.commandType);
                    }
                });
            }
        }
    }
}

using static Dapper.SqlMapper;

namespace Framework.DataSource.DbConnector
{
    public interface IDbConnector
    {
        IQueryAsyncImplementation QueryAsync();
        IQueryMultipleAsyncImplementation QueryMultipleAsync();
        IQuerySingleAsyncImplementation QuerySingleAsync();
    }
    public interface IQueryAsyncImplementation 
    {
        IQueryAsyncImplementation AddCommandTimeout(int commandTimeout);
        IQueryAsyncImplementation AddParameters(object param);
        Task<(IEnumerable<T>? data, bool success)> ExecuteAsync<T>(string source, SqlCommandType sqlCommandType);
    }

    public interface IQueryMultipleAsyncImplementation
    {
        IQueryMultipleAsyncImplementation AddCommandTimeout(int commandTimeout);
        IQueryMultipleAsyncImplementation AddParameters(object param);
        Task<(T? data, bool success)> ExecuteAsync<T>(string source, SqlCommandType sqlCommandType, Func<GridReader, T> readMap);
    }

    public interface IQuerySingleAsyncImplementation
    {
        IQuerySingleAsyncImplementation AddCommandTimeout(int commandTimeout);
        IQuerySingleAsyncImplementation AddParameters(object param);
        Task<(T? data, bool success)> ExecuteAsync<T>(string source, SqlCommandType sqlCommandType);
    }
}

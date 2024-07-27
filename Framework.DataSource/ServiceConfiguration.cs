using Framework.DataSource.DbConnector;
using Framework.DataSource.DbConnector.Implementations.Npgsql;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.DataSource
{
    public static class ServiceConfiguration
    {
        public static void ConfigurePostgreDapper(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnector>(db => new NpgsqlDbConnector(connectionString));
        }
    }
}

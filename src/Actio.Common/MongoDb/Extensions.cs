using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Common.MongoDb
{
    public static class Extensions
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("Mongo"));
            services.AddSingleton<IMongoClient>(c =>
            {
                var options = c.GetRequiredService<IOptions<MongoOptions>>();
                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(c =>
            {
                var options = c.GetRequiredService<IOptions<MongoOptions>>();
                var client = c.GetRequiredService<IMongoClient>();

                return client.GetDatabase(options.Value.Database);
            });

            services.AddScoped<IDatabaseInitializer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();
        }

        public static void InitilizeDatabase(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var service = serviceScope.ServiceProvider;

                try
                {
                    service.GetRequiredService<IDatabaseInitializer>().InitializeDatabaseAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public static async Task<T> FindAndEnsureExistAsync<T>(this IMongoQueryable<T> collection, Expression<Func<T, bool>> predicate, ErrorCode errorCode)
        {
            try
            {
                return await collection
                    .SingleOrDefaultAsync(predicate);
            }
            catch (InvalidOperationException)
            {
                throw new ActioException(errorCode);
            }
        }
    }
}

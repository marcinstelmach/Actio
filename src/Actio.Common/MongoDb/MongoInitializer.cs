using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Actio.Common.MongoDb
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool initialized;
        private bool seed;
        private readonly IMongoDatabase mongoDatabase;

        public MongoInitializer(IMongoDatabase mongoDatabase, IOptions<MongoOptions> options)
        {
            this.mongoDatabase = mongoDatabase;
            this.seed = options.Value.Seed;
        }

        public async Task InitializeDatabaseAsync()
        {
            if (initialized)
            {
                return;
            }

            RegisterConventions();
            initialized = true;
            if (!seed)
            {
                return;
            }


        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConventions", new MongoConvention(), x => true);
        }
    }

    internal class MongoConvention : IConventionPack
    {
        public IEnumerable<IConvention> Conventions => new List<IConvention>
        {
            new IgnoreExtraElementsConvention(true), 
            new EnumRepresentationConvention(BsonType.String),
            new CamelCaseElementNameConvention()
        };
    }
}

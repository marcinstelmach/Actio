using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Actio.Common.MongoDb
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase MongoDatabase;

        public MongoSeeder(IMongoDatabase mongoDatabase)
        {
            this.MongoDatabase = mongoDatabase;
        }

        public async Task SeedAsync()
        {
            var collectionCursor = await MongoDatabase.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();

            if (collections.Any())
            {
                return;
            }

            await CustomSeed();
        }

        protected virtual async Task CustomSeed()
        {
            await Task.CompletedTask;
        }
    }
}

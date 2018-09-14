using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Common.MongoDb;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase mongoDatabase;

        public CategoryRepository(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public async Task<Category> GetAsync(string name)
            => await Collection
                .AsQueryable()
                .FindAndEnsureExistAsync(s => s.Name == name, ErrorCode.CategoryDoesNotExist);

        public async Task<IEnumerable<Category>> BrowseAsync()
            => await Collection.AsQueryable().ToListAsync();


        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);

        private IMongoCollection<Category> Collection
            => mongoDatabase.GetCollection<Category>("Categories");
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.Model;
using Actio.Common.Exceptions;
using Actio.Common.MongoDb;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Api.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase database;

        public ActivityRepository(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task AddAsync(Activity model)
            => await Collection.InsertOneAsync(model);


        public async Task<Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FindAndEnsureExistAsync(s => s.Id == id, ErrorCode.ActivityDoesNotExist);

        public async Task<List<Activity>> BrowseAsync(Guid userId)
            => await Collection
                .AsQueryable()
                .Where(s => s.UserId == userId)
                .ToListAsync();

        private IMongoCollection<Activity> Collection
            => database.GetCollection<Activity>("Activity");
    }
}

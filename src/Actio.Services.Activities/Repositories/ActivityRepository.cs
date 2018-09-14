using System;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Common.MongoDb;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Actio.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase mongoDatabase;

        public ActivityRepository(IMongoDatabase mongoDatabase)
        {
            this.mongoDatabase = mongoDatabase;
        }

        public async Task<Activity> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FindAndEnsureExistAsync(s => s.Id == id, ErrorCode.ActivityDoesNotExist);

        public async Task AddAsync(Activity activity)
            => await Collection
                .InsertOneAsync(activity);

        private IMongoCollection<Activity> Collection
            => mongoDatabase.GetCollection<Activity>("Activity");
    }
}

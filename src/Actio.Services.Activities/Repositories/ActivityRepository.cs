using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .SingleOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(Activity activity)
            => await Collection
                .InsertOneAsync(activity);

        private IMongoCollection<Activity> Collection
            => mongoDatabase.GetCollection<Activity>("Activity");
    }
}

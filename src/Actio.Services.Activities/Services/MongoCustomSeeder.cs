using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.MongoDb;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;

namespace Actio.Services.Activities.Services
{
    public class MongoCustomSeeder : MongoSeeder
    {
        private readonly ICategoryRepository categoryRepository;
        public MongoCustomSeeder(IMongoDatabase mongoDatabase, ICategoryRepository categoryRepository) 
            : base(mongoDatabase)
        {
            this.categoryRepository = categoryRepository;
        }

        protected override async Task CustomSeed()
        {
            var categories = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };

            await Task.WhenAll(categories.Select(s => categoryRepository.AddAsync(new Category(s))));
        }
    }
}

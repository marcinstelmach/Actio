using System;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Common.MongoDb;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;


namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase database;

        public UserRepository(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task<User> GetAsync(Guid id, bool ensureExist = true)
        {
            if (ensureExist)
            {
                return await Collection
                    .AsQueryable()
                    .FindAndEnsureExistAsync(s => s.Id == id, ErrorCode.UserDoesNotExist);
            }

            return await Collection
                .AsQueryable()
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<User> GetAsync(string email, bool ensureExist = true)
        {
            if (ensureExist)
            {
                return await Collection
                    .AsQueryable()
                    .FindAndEnsureExistAsync(s => s.Email == email, ErrorCode.UserDoesNotExist);
            }

            return await Collection
                .AsQueryable()
                .SingleOrDefaultAsync(s => s.Email == email);
        }

        public async Task AddAsync(User user)
            => await Collection
                .InsertOneAsync(user);

        private IMongoCollection<User> Collection
            => database.GetCollection<User>("User");
    }
}

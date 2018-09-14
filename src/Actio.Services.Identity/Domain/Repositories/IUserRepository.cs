using System;
using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;

namespace Actio.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id, bool ensureExist = true);
        Task<User> GetAsync(string email, bool ensureExist = true);
        Task AddAsync(User user);
    }
}

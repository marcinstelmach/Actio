using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actio.Api.Model;

namespace Actio.Api.Repositories
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity model);
        Task<Activity> GetAsync(Guid id);
        Task<List<Activity>> BrowseAsync(Guid userId);
    }
}

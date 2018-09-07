using System;
using System.Threading.Tasks;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;

namespace Actio.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository activityRepository;
        private readonly ICategoryRepository categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            this.activityRepository = activityRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description,
            DateTime createdAt)
        {
            var activityCategory = categoryRepository.GetAsync(category);
            if (activityCategory == null)
            {
                throw new ActioException(ErrorCode.ActivityDoesntExist(category));
            }

            await activityRepository.AddAsync(new Activity(id, name, description, userId, createdAt));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Actio.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task ActivityService_AddMethod_ShouldSucced()
        {
            //arrange
            var category = "Test1";
            var activityRepository = new Mock<IActivityRepository>();
            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(s => s.GetAsync(category)).ReturnsAsync(new Category(category));
            var id = Guid.NewGuid();
            var activityService = new ActivityService(activityRepository.Object, categoryRepository.Object);
            //act
            await activityService.AddAsync(id, Guid.NewGuid(), category, "name", "description",
                DateTime.Now);

            //assert
            categoryRepository.Verify(s => s.GetAsync(category), Times.Once);
            activityRepository.Verify(s => s.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}

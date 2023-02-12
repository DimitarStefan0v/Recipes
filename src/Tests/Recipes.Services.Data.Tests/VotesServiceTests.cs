namespace Recipes.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task WhenUserVotesMoreThanOnceOnlyLastVoteShouldCount()
        {
            // Arrange
            var list = new List<Vote>();
            var mockRepo = new Mock<IDeletableEntityRepository<Vote>>();
            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));
            var service = new VotesService(mockRepo.Object);

            // Act
            await service.SetVoteAsync(1, "someUserId", 2);
            await service.SetVoteAsync(1, "someUserId", 1);
            await service.SetVoteAsync(1, "someUserId", 5);
            await service.SetVoteAsync(1, "someUserId", 4);

            // Assert
            Assert.Equal(4, list.First().Value);
        }

        
    }
}

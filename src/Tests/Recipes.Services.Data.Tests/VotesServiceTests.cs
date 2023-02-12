namespace Recipes.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Moq;
    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Data.Repositories;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels;
    using Xunit;

    public class VotesServiceTests
    {
        private readonly EfDeletableEntityRepository<Vote> votesRepository;
        private readonly IVotesService votesService;

        public VotesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = InMemoryDbContext.InitializeContext();
            this.votesRepository = new EfDeletableEntityRepository<Vote>(context);
            this.votesService = new VotesService(this.votesRepository);
        }

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
            mockRepo.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Once());
            Assert.Equal(4, list.First().Value);
        }

        [Fact]
        public async Task NoMatterHowManyTimesAUserVotesOnlyTheLastVoteIsSavedToDb()
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
            await service.SetVoteAsync(1, "someUserId", 2);
            await service.SetVoteAsync(1, "someUserId", 1);
            await service.SetVoteAsync(1, "someUserId", 3);
            await service.SetVoteAsync(1, "someUserId", 3);
            await service.SetVoteAsync(1, "someUserId", 4);
            await service.SetVoteAsync(1, "someUserId", 5);
            await service.SetVoteAsync(1, "someUserId", 2);

            // Assert
            mockRepo.Verify(x => x.AddAsync(It.IsAny<Vote>()), Times.Once());
            Assert.Single(list);
        }

        [Fact]
        public async Task WhenTwoUsersVoteForTheSameRecipeItShouldReturnAverageScore()
        {
            await this.votesService.SetVoteAsync(2, "mitkoId", 3);
            await this.votesService.SetVoteAsync(2, "peshoId", 5);
            await this.votesService.SetVoteAsync(2, "mitkoId", 4);

            Assert.Equal(4.5, this.votesService.GetAverageVotes(2));
        }
    }
}

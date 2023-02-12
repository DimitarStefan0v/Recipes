namespace Recipes.Services.Data.Tests
{
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Recipes.Data.Models;
    using Recipes.Data.Repositories;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels;
    using Recipes.Web.ViewModels.Home;
    using Xunit;

    public class MessagesServiceTests
    {
        private readonly EfDeletableEntityRepository<Message> messagesRepository;
        private readonly IMessagesService messagesService;

        public MessagesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var context = InMemoryDbContext.InitializeContext();
            this.messagesRepository = new EfDeletableEntityRepository<Message>(context);
            this.messagesService = new MessagesService(this.messagesRepository);
        }

        [Fact]
        public async Task CreateMessageAsyncShouldWorkAsExpected()
        {
            var input = new ContactInputModel
            {
                Name = "Test",
                Email = "test@gmail.com",
                Title = "Test",
                Content = "Test content",
                Token = "Test",
            };

            var totalMessagesCountBeforeAdding = this.messagesRepository.AllAsNoTracking().Count();

            await this.messagesService.CreateMessageAsync(input, "userId");
            await this.messagesService.CreateMessageAsync(input, "userId");
            await this.messagesService.CreateMessageAsync(input, "userId");

            var totalMessagesCountAfterAdding = this.messagesRepository.AllAsNoTracking().Count();

            Assert.Equal(0, totalMessagesCountBeforeAdding);
            Assert.Equal(3, totalMessagesCountAfterAdding);
        }

        [Fact]

        public async Task DeleteAsyncShouldWorkAsExpected()
        {
            var input = new ContactInputModel
            {
                Name = "Test",
                Email = "test@gmail.com",
                Title = "Test",
                Content = "Test content",
                Token = "Test",
            };

            var totalMessagesCountBeforeAdding = this.messagesRepository.AllAsNoTracking().Count();

            await this.messagesService.CreateMessageAsync(input, "userId");

            var totalMessagesCountAfterAdding = this.messagesRepository.AllAsNoTracking().Count();

            var messageId = this.messagesRepository.AllAsNoTracking().Select(x => x.Id).FirstOrDefault();

            await this.messagesService.DeleteAsync(messageId);

            var totalMessagesCountAfterDeleting = this.messagesRepository.AllAsNoTracking().Count();

            Assert.Equal(0, totalMessagesCountBeforeAdding);
            Assert.Equal(1, totalMessagesCountAfterAdding);
            Assert.Equal(0, totalMessagesCountAfterDeleting);
        }
    }
}

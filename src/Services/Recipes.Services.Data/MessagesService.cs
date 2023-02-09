namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels.Home;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessagesService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task CreateMessageAsync(ContactInputModel input, string userId)
        {
            var message = new Message
            {
                Title = input.Title.Trim(),
                Content = input.Content.Trim(),
                Name = input.Name.Trim(),
                Email = input.Email.Trim(),
                Ip = input.Ip,
                AddedByUserId = userId,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var message = this.messagesRepository
                                .All()
                                .Where(x => x.Id == id)
                                .FirstOrDefault();
            if (message == null)
            {
                return;
            }

            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            return this.messagesRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }
    }
}

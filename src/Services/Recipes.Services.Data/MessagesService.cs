﻿namespace Recipes.Services.Data
{
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
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
    }
}

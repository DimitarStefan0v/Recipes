namespace Recipes.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface IMessagesService
    {
        Task CreateMessageAsync(ContactInputModel input, string userId);
    }
}

namespace Recipes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface IMessagesService
    {
        Task CreateMessageAsync(ContactInputModel input, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        Task DeleteAsync(int id);
    }
}

namespace Recipes.Services.Data
{
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface IUsersService
    {
        Task CreateMessageAsync(ContactInputModel input, string userId);
    }
}

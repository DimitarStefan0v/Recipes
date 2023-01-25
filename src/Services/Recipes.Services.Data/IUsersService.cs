namespace Recipes.Services.Data
{
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface IUsersService
    {
        Task SendMessage(ContactInputModel input, string userId);
    }
}

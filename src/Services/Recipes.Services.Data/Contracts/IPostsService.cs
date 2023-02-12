namespace Recipes.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Posts;

    public interface IPostsService
    {
        IEnumerable<T> GetAll<T>(string sort, int page, int itemsPerPage);

        Task CreateAsync(CreatePostInputModel input, string userId);

        T GetById<T>(int id);
    }
}

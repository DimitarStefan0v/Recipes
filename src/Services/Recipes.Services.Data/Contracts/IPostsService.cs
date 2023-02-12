namespace Recipes.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface IPostsService
    {
        IEnumerable<T> GetAll<T>(string sort, int page, int itemsPerPage);
    }
}

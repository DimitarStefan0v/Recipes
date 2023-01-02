namespace Recipes.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        ICollection<T> GetCategories<T>();
    }
}

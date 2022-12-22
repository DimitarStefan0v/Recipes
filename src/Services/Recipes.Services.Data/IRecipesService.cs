namespace Recipes.Services.Data
{
    using Recipes.Web.ViewModels.Recipes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel)
    }
}

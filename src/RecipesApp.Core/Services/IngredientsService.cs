using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApp.Core.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IApplicationDbRepository repo;

        public IngredientsService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public IEnumerable<IngredientNameIdViewModel> GetAll()
        {
            var ingredients = repo.All<Ingredient>()
                .Select(x => new IngredientNameIdViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList();

            return ingredients;
        }

        public string GetIngredientNamesById(IList<int> ingredientIds)
        {
            var query = repo.All<Ingredient>();

            var sb = new StringBuilder();

            for (int i = 0; i < ingredientIds.Count(); i++)
            {
                var ingredient = query
                    .Where(x => x.Id == ingredientIds[i])
                    .Select(x => x.Name)
                    .FirstOrDefault();

                if (ingredientIds.Count() == 1)
                {
                    sb.Append(ingredient);
                    break;
                }

                if (i == ingredientIds.Count() - 1)
                {
                    sb.Append("и ");
                    sb.Append(ingredient);
                }
                else
                {
                    sb.Append(ingredient);
                    sb.Append(", ");
                }

            }

            return sb.ToString();
        }
    }
}

using RecipesApp.Core.Constants;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;

namespace RecipesApp.Core.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IApplicationDbRepository repo;

        public CategoriesService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public ICollection<CategoriesViewModel> GetAllCategories()
        {
            var categories = repo.All<Category>()
                .Select(c => new CategoriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();

            return categories;
        }

        public ICollection<IndexCategoryViewModel> GetFirstFiveCategories()
        {
            var categories = repo.All<Category>()
                .Select(c => new IndexCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            foreach (var category in categories)
            {
                if (category.Name.ToLower() == "основни ястия")
                {
                    category.ImgUrl = DefaultImages.MainDishImage;
                }
                else if (category.Name.ToLower() == "супи")
                {
                    category.ImgUrl = DefaultImages.SoupImage;
                }
                else if (category.Name.ToLower() == "салати")
                {
                    category.ImgUrl = DefaultImages.SaladImage;
                }
                else if (category.Name.ToLower() == "предястия")
                {
                    category.ImgUrl = DefaultImages.AppetizerImage;
                }
                else if (category.Name.ToLower() == "десерти")
                {
                    category.ImgUrl = DefaultImages.DesertImage;
                }
                else if (category.Name.ToLower() == "тестени")
                {
                    category.ImgUrl = DefaultImages.DoughImage;
                }
                else if (category.Name.ToLower() == "сосове")
                {
                    category.ImgUrl = DefaultImages.SauseImage;
                }
                else if (category.Name.ToLower() == "вегетариански и веган")
                {
                    category.ImgUrl = DefaultImages.VeganImage;
                }
                else if (category.Name.ToLower() == "други")
                {
                    category.ImgUrl = DefaultImages.OthersImage;
                }
            }

            return categories;
        }
    }
}

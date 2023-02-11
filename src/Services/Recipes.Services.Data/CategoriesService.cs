namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;
    using Recipes.Web.ViewModels.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly ICloudImagesService imagesService;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository,
            ICloudImagesService imagesService)
        {
            this.categoriesRepository = categoriesRepository;
            this.imagesService = imagesService;
        }

        public async Task CreateAsync(CreateCategoryInputModel input, string userId)
        {
            var category = this.categoriesRepository
                .AllAsNoTracking()
                .Where(x => x.Name.ToLower() == input.Name.ToLower().Trim())
                .FirstOrDefault();

            if (category == null)
            {
                category = new Category
                {
                    Name = input.Name.Trim(),
                    Color = input.Color == null ? null : input.Color.Trim(),
                };

                if (input.Image != null)
                {
                    var imgResult = await this.imagesService.UploadImageAsync(input.Image);

                    string imgUrl = imgResult.SecureUrl.AbsoluteUri;
                    string imgPubId = imgResult.PublicId;

                    var imageToWrite = new CloudImage
                    {
                        PictureUrl = imgUrl,
                        PicturePublicId = imgPubId,
                        AddedByUserId = userId,
                    };

                    category.Image = imageToWrite;
                }

                await this.categoriesRepository.AddAsync(category);
                await this.categoriesRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categoriesRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            return this.categoriesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<CategoryInListViewModel> GetCategories()
        {
            var categories = this.categoriesRepository
                .AllAsNoTracking()
                .Select(x => new CategoryInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Color = x.Color,
                    ImageUrl = x.Image.PictureUrl,
                })
                .ToList();

            return categories;
        }

        public ICollection<T> GetCategoryNames<T>()
        {
            return this.categoriesRepository.AllAsNoTracking().To<T>().ToList();
        }

        public async Task UpdateAsync(int id, EditCategoryInputModel input, string userId)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);
            if (category.Name != input.Name.Trim())
            {
                category.Name = input.Name.Trim();
            }

            if (category.Color != input.Color)
            {
                category.Color = input.Color.Trim();
            }

            if (input.NewImage != null)
            {
                var imgResult = await this.imagesService.UploadImageAsync(input.NewImage);

                string imgUrl = imgResult.SecureUrl.AbsoluteUri;
                string imgPubId = imgResult.PublicId;

                var imageToWrite = new CloudImage
                {
                    PictureUrl = imgUrl,
                    PicturePublicId = imgPubId,
                    AddedByUserId = userId,
                };

                category.Image = imageToWrite;
            }

            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}

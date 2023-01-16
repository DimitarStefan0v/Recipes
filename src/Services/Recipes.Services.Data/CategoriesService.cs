namespace Recipes.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
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
                .Where(x => x.Name == input.Name.Trim())
                .FirstOrDefault();

            if (category == null)
            {
                category = new Category
                {
                    Name = input.Name.Trim(),
                    Color = input.Color.Trim(),
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

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<T> GetCategories<T>()
        {
            return this.categoriesRepository.AllAsNoTracking().To<T>().ToList();
        }
    }
}

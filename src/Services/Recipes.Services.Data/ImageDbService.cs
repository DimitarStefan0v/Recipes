namespace Recipes.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;

    public class ImageDbService : IImageDbService
    {
        private readonly IDeletableEntityRepository<CloudImage> cloudImagesRepository;

        public ImageDbService(IDeletableEntityRepository<CloudImage> cloudImagesRepository)
        {
            this.cloudImagesRepository = cloudImagesRepository;
        }

        public async Task<int> WriteToDatabasebAsync(string imageUrl, string imagePublicId)
        {
            var image = new CloudImage
            {
                PictureUrl = imageUrl,
                PicturePublicId = imagePublicId,
            };

            await this.cloudImagesRepository.AddAsync(image);
            await this.cloudImagesRepository.SaveChangesAsync();

            return image.Id;
        }

        public string GetPublicId(int id)
        {
            var image = this.cloudImagesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            var pubId = image.PicturePublicId;

            return pubId;
        }
    }
}

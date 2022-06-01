using RecipesApp.Core.Contracts;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApp.Core.Services
{
    public class ImageDbService : IImageDbService
    {
        private readonly IApplicationDbRepository repo;

        public ImageDbService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<int> WriteToDatabasebAsync(string imageUrl, string imagePublicId)
        {
            var image = new CloudImage
            {
                PictureUrl = imageUrl,
                PicturePublicId = imagePublicId,
            };

            await repo.AddAsync(image);
            await repo.SaveChangesAsync();

            return image.Id;
        }

        public string GetPublicId(int id)
        {
            var image = repo.All<CloudImage>().FirstOrDefault(i => i.Id == id);
            var pubId = image.PicturePublicId;

            return pubId;
        }
    }
}

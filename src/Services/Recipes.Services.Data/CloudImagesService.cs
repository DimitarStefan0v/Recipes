namespace Recipes.Services.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Recipes.Services.Data.Contracts;

    public class CloudImagesService : ICloudImagesService
    {
        private readonly Cloudinary cloudUtility;

        public CloudImagesService(Cloudinary cloudUtility)
        {
            this.cloudUtility = cloudUtility;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                await imageFile.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            var fileName = imageFile.FileName;

            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "RecipesAppImages",
                    File = new FileDescription(fileName, ms),
                    Transformation = new Transformation().Crop("limit").Width(300).Height(250),
                };

                var uploadResult = await this.cloudUtility.UploadAsync(uploadParams);

                return uploadResult;
            }
        }

        public Task DeleteImages(params string[] publicIds)
        {
            throw new NotImplementedException();
        }

        public string GetImageUrl(string imagePublicId)
        {
            throw new NotImplementedException();
        }

        public string GetThumbnailUrl(string imagePublicId)
        {
            throw new NotImplementedException();
        }

        public string GetTinyThumbnailUrl(string imagePublicId)
        {
            throw new NotImplementedException();
        }
    }
}

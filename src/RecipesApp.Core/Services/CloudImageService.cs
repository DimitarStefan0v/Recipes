using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using RecipesApp.Core.Contracts;
using RecipesApp.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApp.Core.Services
{
    public class CloudImageService : ICloudImageService
    {
        private readonly Cloudinary cloudUtility;

        public CloudImageService(Cloudinary _cloudUtility)
        {
            cloudUtility = _cloudUtility;
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

            // int index = fileName.LastIndexOf('.');
            // var trueFileName = index == -1 ? fileName : fileName.Substring(0, index);

            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "RecipesAppImages",
                    File = new FileDescription(fileName, ms),
                    Transformation = new Transformation().Crop("limit").Width(300).Height(250),
                };

                // $"{PubPrefix}_{Guid.NewGuid().ToString().Substring(0, 5)}"

                var uploadResult = await cloudUtility.UploadAsync(uploadParams);

                return uploadResult;
            }
        }

        public string GetImageUrl(string imagePublicId)
        {
            string imageUrl = cloudUtility
                                      .Api
                                      .UrlImgUp
                                      .Transform(new Transformation().Quality("auto"))
                                      .BuildUrl(string.Format("{0}.jpg", imagePublicId));

            return imageUrl;
        }

        public string GetThumbnailUrl(string imagePublicId)
        {
            var imageUrl = cloudUtility
                                 .Api
                                 .UrlImgUp
                                 .Transform(new Transformation()
                                     .Height(200)
                                     .Width(200)
                                     .Crop("thumb"))
                                 .BuildUrl(string.Format("{0}.jpg", imagePublicId));

            return imageUrl;
        }

        public string GetTinyThumbnailUrl(string imagePublicId)
        {
            var imageUrl = cloudUtility
                                 .Api
                                 .UrlImgUp
                                 .Transform(new Transformation()
                                     .Height(40)
                                     .Width(60)
                                     .Crop("thumb"))
                                 .BuildUrl(string.Format("{0}.jpg", imagePublicId));

            return imageUrl;
        }

        public async Task DeleteImages(params string[] publicIds)
        {
            var delParams = new DelResParams
            {
                PublicIds = publicIds.ToList(),
                Invalidate = true,
            };

            await cloudUtility.DeleteResourcesAsync(delParams);
        }     
    }
}

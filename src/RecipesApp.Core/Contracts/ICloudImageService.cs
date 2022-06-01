using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace RecipesApp.Core.Contracts
{
    public interface ICloudImageService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile);

        string GetImageUrl(string imagePublicId);

        string GetThumbnailUrl(string imagePublicId);

        string GetTinyThumbnailUrl(string imagePublicId);

        Task DeleteImages(params string[] publicIds);
    }
}

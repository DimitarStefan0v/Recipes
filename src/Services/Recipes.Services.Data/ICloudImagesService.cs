namespace Recipes.Services.Data
{
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public interface ICloudImagesService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile);

        string GetImageUrl(string imagePublicId);

        string GetThumbnailUrl(string imagePublicId);

        string GetTinyThumbnailUrl(string imagePublicId);

        Task DeleteImages(params string[] publicIds);
    }
}

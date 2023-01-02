namespace Recipes.Services.Data
{
    using System.Threading.Tasks;

    public interface IImageDbService
    {
        Task<int> WriteToDatabasebAsync(string imageUrl, string imagePublicId);

        string GetPublicId(int id);
    }
}

namespace Recipes.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IImageDbService
    {
        Task<int> WriteToDatabasebAsync(string imageUrl, string imagePublicId);

        string GetPublicId(int id);
    }
}

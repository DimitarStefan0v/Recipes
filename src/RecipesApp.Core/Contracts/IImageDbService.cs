namespace RecipesApp.Core.Contracts
{
    public interface IImageDbService
    {
        Task<int> WriteToDatabasebAsync(string imageUrl, string imagePublicId);

        string GetPublicId(int id);
    }
}

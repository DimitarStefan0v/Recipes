namespace Recipes.Services.Data
{
    using System.Threading.Tasks;

    using Recipes.Web.ViewModels.Home;

    public interface ICountsService
    {
        IndexStatsViewModel GetStats();

        Task IncreaseViews(int id, bool forCategory);
    }
}

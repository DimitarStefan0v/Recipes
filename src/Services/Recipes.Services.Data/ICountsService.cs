namespace Recipes.Services.Data
{
    using Recipes.Web.ViewModels.Home;

    public interface ICountsService
    {
        IndexStatsViewModel GetStats();
    }
}

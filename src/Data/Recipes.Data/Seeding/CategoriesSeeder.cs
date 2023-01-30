namespace Recipes.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Recipes.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Основни ястия",
                Color = "#F53B50",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Супи",
                Color = "#FDCE01",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Салати",
                Color = "#99CD00",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Предястия",
                Color = "#00CDA4",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Десерти",
                Color = "#674DA6",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Тестени",
                Color = "#D4006E",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Сосове",
                Color = "#FCA103",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Вегетариански и веган",
                Color = "#00CC39",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Напитки",
                Color = "#34B8EF",
            });
            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Други",
                Color = "#FF6600",
            });
        }
    }
}

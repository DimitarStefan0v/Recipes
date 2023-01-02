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

            await dbContext.Categories.AddAsync(new Category { Name = "Основни ястия" });
            await dbContext.Categories.AddAsync(new Category { Name = "Супи" });
            await dbContext.Categories.AddAsync(new Category { Name = "Салати" });
            await dbContext.Categories.AddAsync(new Category { Name = "Предястия" });
            await dbContext.Categories.AddAsync(new Category { Name = "Десерти" });
            await dbContext.Categories.AddAsync(new Category { Name = "Тестени" });
            await dbContext.Categories.AddAsync(new Category { Name = "Сосове" });
            await dbContext.Categories.AddAsync(new Category { Name = "Вегетариански и веган" });
            await dbContext.Categories.AddAsync(new Category { Name = "Зимнина" });
            await dbContext.Categories.AddAsync(new Category { Name = "Други" });
        }
    }
}

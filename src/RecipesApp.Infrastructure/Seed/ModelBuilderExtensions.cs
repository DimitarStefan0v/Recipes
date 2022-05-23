using Microsoft.EntityFrameworkCore;
using RecipesApp.Infrastructure.Data;

namespace RecipesApp.Infrastructure.Seed
{
    internal static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Основни ястия" },
                new Category { Id = 2, Name = "Супи" },
                new Category { Id = 3, Name = "Салати" },
                new Category { Id = 4, Name = "Предястия" },
                new Category { Id = 5, Name = "Десерти" },
                new Category { Id = 6, Name = "Тестени" },
                new Category { Id = 7, Name = "Сосове" },
                new Category { Id = 8, Name = "Вегетариански и веган" },
                new Category { Id = 9, Name = "Зимнина" },
                new Category { Id = 10, Name = "Други" });
        }
    }
}

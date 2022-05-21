using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Models;
using RecipesApp.Core.Services;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesApp.Test
{
    public class CategoriesServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();

            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicationDbRepository, ApplicationDbRepository>()
                .AddSingleton<ICategoriesService, CategoriesService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicationDbRepository>();

            await SeedDbAsync(repo);
        }

        [Test]
        public void GetAllCategoriesShouldReturnCorrectNumberOfCategories()
        {
            var service = serviceProvider.GetService<ICategoriesService>();
            var categories = service.GetAllCategories();

            Assert.AreEqual(2, categories.Count);
        }

        [Test]
        public void GetAllCategoriesShouldReturnCollectionOfCategoryViewModel()
        {
            var service = serviceProvider.GetService<ICategoriesService>();
            var categories = service.GetAllCategories();

            Assert.IsInstanceOf<ICollection<CategoriesViewModel>>(categories);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IApplicationDbRepository repo)
        {
            var firstCategory = new Category()
            {
                Name = "Салати"
            };

            var secondCategory = new Category()
            {
                Name = "Ястия с месо"
            };

            await repo.AddAsync(firstCategory);
            await repo.AddAsync(secondCategory);
            await repo.SaveChangesAsync();
        }
    }
}
namespace Recipes.Services.Data.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Recipes.Data;

    public class InMemoryDbContext
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}

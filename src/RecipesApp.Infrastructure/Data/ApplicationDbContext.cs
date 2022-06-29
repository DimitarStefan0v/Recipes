using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipesApp.Infrastructure.Data.Identity;
using RecipesApp.Infrastructure.Seed;

namespace RecipesApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<CloudImage> CloudImages { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Category>()
            //    .HasMany(c => c.Recipes)
            //    .WithOne(r => r.Category)
            //    .IsRequired();
            //.OnDelete(DeleteBehavior.SetNull);

            // Initial Seed of categories
            builder.Seed();

            base.OnModelCreating(builder);
        }
    }
}
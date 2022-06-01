using Microsoft.EntityFrameworkCore;
using RecipesApp.Core.Contracts;
using RecipesApp.Core.Services;
using RecipesApp.Infrastructure.Data;
using RecipesApp.Infrastructure.Data.Repositories;
using RecipesApp.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();

            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICloudImageService, CloudImageService>();
            services.AddTransient<IImageDbService, ImageDbService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}

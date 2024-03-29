namespace Recipes.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Recipes.Data;
    using Recipes.Data.Common;
    using Recipes.Data.Common.Repositories;
    using Recipes.Data.Models;
    using Recipes.Data.Repositories;
    using Recipes.Data.Seeding;
    using Recipes.Services.Data;
    using Recipes.Services.Data.Contracts;
    using Recipes.Services.Mapping;
    using Recipes.Services.Messaging;
    using Recipes.Web.Infrastructure;
    using Recipes.Web.ViewModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddRazorRuntimeCompilation();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ICountsService, CountsService>();
            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICloudImagesService, CloudImagesService>();
            services.AddTransient<IImageDbService, ImageDbService>();
            services.AddTransient<IVotesService, VotesService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IPostsService, PostsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient(typeof(GoogleCaptchaService));

            // SendGrid Setup
            services.AddTransient<IEmailSender, SendGridEmailSender>(x => new SendGridEmailSender(configuration["SendGrid:ApiKey"]));

            // Cloudinary Setup
            var cloudinaryAccount = new Account(
                configuration.GetValue<string>("CloudinarySettings:CloudName"),
                configuration.GetValue<string>("CloudinarySettings:ApiKey"),
                configuration.GetValue<string>("CloudinarySettings:ApiSecret"));

            var cloudUtility = new Cloudinary(cloudinaryAccount);

            services.AddSingleton(cloudUtility);

            // GoogleReCaptcha Setup
            services.Configure<GoogleCaptchaConfig>(configuration.GetSection("GoogleReCaptcha"));
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute("recipe", "r-{id}/{name:minlength(2)}", new { controller = "Recipes", action = "ById" });
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}

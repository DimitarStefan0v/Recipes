using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RecipesApp.Core.Constants;
using RecipesApp.Infrastructure.Data.Identity;

namespace RecipesApp.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(RoleManager<IdentityRole> _roleManager, 
            UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        //public async Task CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = Roles.Administrator
        //    });

        //    await Task.CompletedTask;
        //}
    }
}

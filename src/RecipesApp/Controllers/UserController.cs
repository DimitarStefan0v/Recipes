using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipesApp.Core.Constants;
using RecipesApp.Core.Contracts;
using RecipesApp.Infrastructure.Data.Identity;

namespace RecipesApp.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService userService;

        public UserController(RoleManager<IdentityRole> _roleManager, 
            UserManager<ApplicationUser> _userManager, 
            IUserService _userService)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            userService = _userService;
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await userService.GetUsersAsync();

            return Json(users);
        }

        public async Task<IActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Administrator"
            //});

            return Ok();
        }
    }
}

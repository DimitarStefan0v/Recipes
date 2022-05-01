using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesApp.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}

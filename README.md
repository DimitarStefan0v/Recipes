# RecipesApp

## :point_right: Project Introduction :point_left:

**RecipesApp** is a ready-to-use ASP.NET Core application.

# Link
https://bgrecipes.azurewebsites.net/

## :pencil: Project Description
- Guest visitors can:
  - view home index page where it shows all categories, most recently added recipes, most commented recipes and recipes with the highest vote count 
  - view recipes by categories by clicking on the category image in home index page, chosing from the dropdown menu in the navbar navigation or from the footer links;
  - view all recipes by choosing from the dropdown menu in the navbar navigation or from the footer links;
  - view single recipe in ById page by clicking on chosen recipe and view its public details: name, image, preparation time, cooking time, portions count, ingredients, instructions, recipe author, recipe added time, vote rate, comments(for each comment it shows the comment content, comment author and date and time of the added comment);
  - search for recipes by chosen ingredients;
  - search for recipe by recipe name;
- Logged Users can do all of the above and:
  - add new recipes;
  - add new comment to recipe;
  - vote for recipe in ById page;
  - view favorites page;
  - add recipe to his own favorite recipes by clicking on the heart font awesome icon in chosen ById page;
  - remove recipe from his own favorite recipes by clicking on the heart icon again;
- Administrator can do all of the above and:
  - delete recipes;
  - delete comments;
  - view UncheckedRecipes page where it shows all recipes sorted by oldest which are unchecked by administrator and by clicking on chosen recipe redirect to ById page with option to approve recipe after viewing it;
	
## :hammer: Used technologies
* .NET 6.0
* ASP.NET Core MVC
* Entity Framework CORE 6.0
* SQL Server
* Cloudinary
* JavaScript
* AJAX
* nUnit
* TagHelpers
* PartialViews
* Pagination
* HTML
* Bootstrap
* CSS
  * Flexbox
  * Media Queries
  * Positioning
  * etc
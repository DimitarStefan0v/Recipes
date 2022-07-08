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

## Pictures

* Logged-In User

**Home Index Page**

![Unregistered user](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Home%20Index%20Page.png)

**Recipes ById Page**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Recipes%20ById%20Page.png) 

**Caregories ById Page(main-dishes)**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Categories%20ById%20Page.png)

**Caregories ById Page(deserts)**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Categories%20ById(deserts)%20Page.png) 

**Categories Index Page**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Categories%20Index%20Page.png)

**Recipes All Page**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Recipes%20All%20Page.png)

**Dropdown In Layout**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Dropdown%20In%20Layout.png)

**Recipes Favorites Page (with no added recipes for the current user)**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Recipes%20Favorites%20Page(with%20no%20added%20recipes%20for%20current%20user).png)

**Recipes Favorites Page (with eight added recipes for the current user)**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Recipes%20Favorites%20Page.png)

**Recipes Create Page**

![Logged-In User](https://github.com/DimitarStefan0v/Recipes/blob/main/Screenshots/Recipes%20Create%20Page.png)
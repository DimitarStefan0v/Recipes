# Recipes


# Link
https://recipes.bsite.net/




## :pencil: Project Description 

**_Guest users_** are able to view all recipes, recipes by category, search recipe by name, single recipe with all its details and comments. Guest users are also able to view forum posts and comments, send a message through the contact form, but can't add new recipes, write posts, comments, vote for recipes or add recipes to favorites.

**_Registered users_** are able to add new recipes with: title, image, instructions, ingredients, preparation time, cooking time, portions count and category. After recipe is uploaded to the system it must be approved by the Administrator first before its visible in the website. Registered users can view all personal uploaded recipes to the system and edit or delete their own recipes. Registered users are also able to vote for recipes, add/remove recipe to/from favorites, write comments to recipes, write forum posts or respond to posts.

**_Administators_** are able to delete and edit all recipes, comments and posts. All new recipes must be approved by administrator before being visible in the website.


# :hammer: Built With:
* ASP.NET Core 6.0
* MS SQL Server
* Entity Framework Core 
* JS
* Automapper
* Repository Pattern 
* Service Layer
* SendGrid
* Cloudinary
* Ajax
* Web Api
* ReCaptcha
* Paging and Sorting with EF Core
* PartialViews
* MomentJs
* FontAwesome
* HTML
* CSS
* Responsive design with Media Queries
* Bootstrap
* Moq
* xUnit

## :floppy_disk: Database Diagram
![RecipesDbDiagram](https://user-images.githubusercontent.com/64395262/220134596-742a11eb-c052-48ec-bffc-79db73e104a6.png)

## Screenshots:

### Home/Index 
![Home_Index](https://user-images.githubusercontent.com/64395262/220135493-12808757-9623-469e-a454-e4912fbfbdcc.png)

### Recipes/All
![Recipes_All](https://user-images.githubusercontent.com/64395262/220135698-05aeeee5-f90f-44cf-8063-9f8b669c83fc.png)

### Recipes/ById
![Recipes_ById](https://user-images.githubusercontent.com/64395262/220135980-9226a5a6-ed86-42ae-b1d5-bfee65196f75.png)

### Recipes/AllByName
![Recipes_AllByName](https://user-images.githubusercontent.com/64395262/220136213-0371759d-ed5d-4ac1-9bd9-aa1ab6494cfa.png)

### Recipes/Create (for Registered Users only)
![Recipes_Create(Registered_User)](https://user-images.githubusercontent.com/64395262/220136370-c5fed3a5-92ac-43a0-aeb2-d3bb2b4bd245.png)

### Categories/All
![Categories_All](https://user-images.githubusercontent.com/64395262/220136777-f7f7dc18-7091-49a4-b503-42a4de2564d6.png)

### Categories/ById
![Categories_ById](https://user-images.githubusercontent.com/64395262/220137077-f49023a7-820d-4f40-a418-87fcb4f529b5.png)

### Categories/Create (for Admin access only)
![Categories_Create(With_Administration_Role)](https://user-images.githubusercontent.com/64395262/220137348-605d929f-af8b-4d27-a77d-d2ce15ec567c.png)

### Categories/Edit (for Admin access only)
![Categories_Edit(With_Administration_Role)](https://user-images.githubusercontent.com/64395262/220137701-855f0dab-ec5f-41af-8780-afe1c5781d47.png)

### Forum/Index
![Forum_Index](https://user-images.githubusercontent.com/64395262/220138071-37d66945-07a1-4834-ac07-c7b5f797b654.png)

### Posts/ById (for Registered Users it shows the form for comments and have access to post comment)
![Posts_ById(Registered_User)](https://user-images.githubusercontent.com/64395262/220138266-a97041c6-cd8a-49c9-95ea-11d99ccf58f4.png)

### Posts/Create (for Registered Users only)
![Posts_Create(Registered_User)](https://user-images.githubusercontent.com/64395262/220138715-0bd5e0a6-5b0a-484a-86b5-dee5c4ce4000.png)

### Users/FavoriteRecipes (for Registered Users only)
![Users_FavoriteRecipes](https://user-images.githubusercontent.com/64395262/220138946-d44c9100-6533-42be-b1cb-881c24f05d48.png)

### Users/PersonalRecipes (for Registered Users only)
![Users_PersonalRecipes](https://user-images.githubusercontent.com/64395262/220139128-d5126269-4aa2-4682-ae56-550ace073c45.png)

### Home/Contacts
![Home_Contacts](https://user-images.githubusercontent.com/64395262/220139311-04186a90-c312-4fd2-a597-38da789141c1.png)

### Home/Privacy
![Home_Privacy](https://user-images.githubusercontent.com/64395262/220139459-16be77a5-413a-41d4-8a7a-73847680cb66.png)



## Author

- [Dimitar Stefanov](https://github.com/DimitarStefan0v)


## Template authors

- [Nikolay Kostov](https://github.com/NikolayIT)
- [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)
- [Stoyan Shopov](https://github.com/StoyanShopov)


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

const AddRecipeToUserFavorites = function () {
    const article = document.querySelector('.favorites');
    const i = article.querySelector('i');
    i.addEventListener('click' async () => {
        const value = document.querySelector('.favorites span');
        if (value.textContent == 'False') {
            const recipeId = document.getElementById('recipe-id').value;
            let data = { recipeId: recipeId };
            const url = '/api/Favorites';
            const options = {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    },
                body: JSON.stringify(data),
            };

            const response = await fetch(url, options);
            const result = await response.json();

            value.textContent = result;
        }
    });
}

window.onload = AddRecipeToUserFavorites();
﻿window.onload = addRecipeToUserFavorites();

function addRecipeToUserFavorites() {
    const article = document.querySelector('.favorites');
    const i = article.querySelector('i');
    i.addEventListener('click', async () => {
        const value = document.querySelector('.favorites span');
            const recipeId = document.getElementById('recipe-id').value;
            const antiForgeryToken = document.querySelector('#antiForgeryForm input[name=__RequestVerificationToken]').value;
            let data = { recipeId: recipeId };
            const url = '/api/Favorites';
            const options = {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': antiForgeryToken,
                    },
                body: JSON.stringify(data),
            };

            const response = await fetch(url, options);
            const result = await response.json();

            value.textContent = result;
    });
}
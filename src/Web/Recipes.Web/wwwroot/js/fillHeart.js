const AddClassToHeart = function () {
    let heart = document.querySelector('.favorites i');
    heart.addEventListener('click', fillHeart);
    let paragrapth = document.getElementById('favorites-p');
    function fillHeart() {
        CheckIfUserCanAddRecipeToFavorites(heart);
        let isRecipeInFavorites = document.getElementById('is-recipe-favorite');
        if (isRecipeInFavorites.textContent == 'true') {
            heart.setAttribute('class', 'fa-regular fa-heart');
            paragrapth.textContent = 'Добави в любими'
        } else if (isRecipeInFavorites.textContent == 'false') {
            heart.setAttribute('class', 'fa-solid fa-heart');
            paragrapth.textContent = 'Премахни от любими'
        }
    }
}

const CheckIfUserCanAddRecipeToFavorites = function (childToRemove) {
    let authenticated = document.querySelector('.paragraph-warning');
    if (authenticated != null && authenticated.textContent == 'unregistered') {
        authenticated.textContent = 'Само за регистрирани потребители!';
        authenticated.style.display = 'block';
        let voteContainer = document.querySelector('.votes');
        voteContainer.style.display = 'none';
        let favoritesContainer = document.querySelector('.favorites');
        favoritesContainer.style.display = 'none';
    }
}

window.onload = AddClassToHeart();
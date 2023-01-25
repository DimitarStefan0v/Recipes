const AddClassToHeart = function () {
    let heart = document.querySelector('.favorites i');
    heart.addEventListener('click', fillHeart);

    function fillHeart() {
        CheckIfUserCanAddRecipeToFavorites(heart);
        let isRecipeInFavorites = document.getElementById('is-recipe-favorite');
        if (isRecipeInFavorites.textContent == 'True') {
            isRecipeInFavorites.textContent = 'False';
            heart.setAttribute('class', 'fa-regular fa-heart');
        } else if (isRecipeInFavorites.textContent == 'False') {
            isRecipeInFavorites.textContent = 'True';
            heart.setAttribute('class', 'fa-solid fa-heart');
        }
    }
}

const CheckIfUserCanAddRecipeToFavorites = function (childToRemove) {
    let p = document.querySelector('.paragraph-warning');
    if (p != null && p.textContent == 'unregistered') {
        p.textContent = 'Само за регистрирани потребители!!!';
        p.style.display = 'block';
        let ul = document.querySelector('.votes ul');
        let article = document.querySelector('.votes-container .votes');
        let articleFavorites = document.querySelector('.votes-container .favorites');
        article.removeChild(ul);
        articleFavorites.removeChild(childToRemove);

    }
}

window.onload = AddClassToHeart();
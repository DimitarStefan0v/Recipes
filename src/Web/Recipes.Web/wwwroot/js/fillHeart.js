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
    let p = document.querySelector('.paragraph-warning');
    if (p != null && p.textContent == 'unregistered') {
        p.textContent = 'Само за регистрирани потребители!!!';
        p.style.display = 'block';
        let ul = document.querySelector('.votes ul');
        let article = document.querySelector('.votes-container .votes');
        let articleFavorites = document.querySelector('.votes-container .favorites');
        let paragrapthToRemove = document.getElementById('favorites-p');
        article.removeChild(ul);
        articleFavorites.removeChild(childToRemove);
        articleFavorites.removeChild(paragrapthToRemove);
    }
}

window.onload = AddClassToHeart();
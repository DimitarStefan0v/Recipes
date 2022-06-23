const ChangeColor = function () {
    let listOfRecipes = document.querySelectorAll('.single-recipe-container');

    for (let i = 0; i < listOfRecipes.length; i++) {
        var category = listOfRecipes[i].querySelector('.singe-recipe-category');
        if (category.textContent.toLowerCase().trim() == 'основни ястия') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #F53B50';
        } else if (category.textContent.toLowerCase().trim() == 'супи') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #FDCE01';
        } else if (category.textContent.toLowerCase().trim() == 'салати') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #99CD00';
        } else if (category.textContent.toLowerCase().trim() == 'предястия') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #00CDA4';
        } else if (category.textContent.toLowerCase().trim() == 'десерти') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #674DA6';
        } else if (category.textContent.toLowerCase().trim() == 'тестени') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #D4006E';
        } else if (category.textContent.toLowerCase().trim() == 'сосове') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #FCA103';
        } else if (category.textContent.toLowerCase().trim() == 'вегетариански и веган') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #00CC39';
        } else if (category.textContent.toLowerCase().trim() == 'други') {
            listOfRecipes[i].querySelector('.single-recipe-container-name').style.borderLeft = '8px solid #FF6600';
        }
    }

    let recipesById = document.querySelectorAll('.recipe-container');

    for (let i = 0; i < recipesById.length; i++) {
        var category = recipesById[i].querySelector('#singleRecipe-categoryName');

        if (category.textContent.toLowerCase().trim() == 'основни ястия') {
            recipesById[i].style.backgroundColor = '#F53B50';
        } else if (category.textContent.toLowerCase().trim() == 'супи') {
            recipesById[i].style.backgroundColor = '#FDCE01';
        } else if (category.textContent.toLowerCase().trim() == 'салати') {
            recipesById[i].style.backgroundColor = '#99CD00';
        } else if (category.textContent.toLowerCase().trim() == 'предястия') {
            recipesById[i].style.backgroundColor = '#00CDA4';
        } else if (category.textContent.toLowerCase().trim() == 'десерти') {
            recipesById[i].style.backgroundColor = '#674DA6';
        } else if (category.textContent.toLowerCase().trim() == 'тестени') {
            recipesById[i].style.backgroundColor = '#D4006E';
        } else if (category.textContent.toLowerCase().trim() == 'сосове') {
            recipesById[i].style.backgroundColor = '#FCA103';
        } else if (category.textContent.toLowerCase().trim() == 'вегетариански и веган') {
            recipesById[i].style.backgroundColor = '#00CC39';
        } else if (category.textContent.toLowerCase().trim() == 'други') {
            recipesById[i].style.backgroundColor = '#FF6600';
        }
    }
}

window.onload = ChangeColor();
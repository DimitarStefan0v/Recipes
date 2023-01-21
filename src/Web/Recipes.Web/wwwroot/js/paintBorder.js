const PaintRecipeInListBorderLeft = function () {
    const recipes = document.querySelectorAll('.recipe-in-list-container');
    for (let i = 0; i < recipes.length; i++) {
        let color = recipes[i].querySelector('.recipe-in-list-color').value;
        let elementToPaint = recipes[i].querySelector('.recipe-in-list-container-name');
        elementToPaint.style.borderLeft = "10px solid " + color;
    }
}

window.onload = PaintRecipeInListBorderLeft();
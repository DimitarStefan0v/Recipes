const AddIngredient = function () {
    let button = document.getElementById('addIngredient');
    let ingredientsContainer = document.getElementById('IngredientsContainer');

    button.onclick = function (ev) {
        ev.preventDefault();
        let num = ingredientsContainer.getElementsByTagName('input').length
        AddIngredientAndQuantity(ingredientsContainer, num);
    }
}

const AddIngredientAndQuantity = function (container, number) {

    let count = 0 + number;

    let ingredientInput = CreateInput();
    ingredientInput.setAttribute('name', 'Ingredients[' + count + '].IngredientName');
    let ingredientLabel = CreateLabel();
    ingredientLabel.setAttribute('for', 'Ingredients[' + count + '].IngredientName');
    ingredientLabel.textContent = 'Име на съставката';
    
    let quantityInput = CreateInput();
    quantityInput.setAttribute('name', 'Ingredients[' + count + '].Quantity');
    let quantityLabel = CreateLabel();
    quantityLabel.setAttribute('for', 'Ingredients[' + count + '].Quantity');
    quantityLabel.textContent = 'Количество';

    container.appendChild(ingredientLabel);
    container.appendChild(ingredientInput);
    container.appendChild(quantityLabel);
    container.appendChild(quantityInput);
} 

const CreateInput = function () {
    let input = document.createElement("input");
    return input;
}

const CreateLabel = function () {
    let label = document.createElement("label");
    return label;
}

window.onload = AddIngredient();
const AddIngredient = function () {
    let button = document.getElementById('addIngredient');
    let ingredientsContainer = document.getElementById('IngredientsContainer');

    button.onclick = function (ev) {
        ev.preventDefault();
        let num = ingredientsContainer.getElementsByTagName('input').length;
        AddIngredientAndQuantity(ingredientsContainer, num);
    }
}

const AddIngredientAndQuantity = function (container, number) {

    let count = Math.ceil(number / 2);

    let divRow = CreateDiv();
    divRow.setAttribute('class', 'row');

    let ingredientDivCol = CreateDiv();
    ingredientDivCol.setAttribute('class', 'col');

    let ingredientInput = CreateInput();
    ingredientInput.setAttribute('name', 'Ingredients[' + count + '].IngredientName');
    ingredientInput.setAttribute('class', 'form-control');
    ingredientInput.setAttribute('placeholder', 'Съставка');

    ingredientDivCol.appendChild(ingredientInput);

    //let ingredientLabel = CreateLabel();
    //ingredientLabel.setAttribute('for', 'Ingredients[' + count + '].IngredientName');
    //ingredientLabel.textContent = 'Име на съставката';

    let quantityDivCol = CreateDiv();
    quantityDivCol.setAttribute('class', 'col')
    
    let quantityInput = CreateInput();
    quantityInput.setAttribute('name', 'Ingredients[' + count + '].Quantity');
    quantityInput.setAttribute('class', 'form-control');
    quantityInput.setAttribute('placeholder', 'Количество');

    quantityDivCol.appendChild(quantityInput);

    divRow.appendChild(ingredientDivCol);
    divRow.appendChild(quantityDivCol);

    container.appendChild(divRow);

    //let quantityLabel = CreateLabel();
    //quantityLabel.setAttribute('for', 'Ingredients[' + count + '].Quantity');
    //quantityLabel.textContent = 'Количество на съставката';

    //container.appendChild(ingredientLabel);
    //container.appendChild(ingredientInput);
    //container.appendChild(quantityLabel);
    //container.appendChild(quantityInput);
} 

const CreateDiv = function () {
    let div = document.createElement('div');
    return div;
}

const CreateInput = function () {
    let input = document.createElement('input');
    return input;
}

const CreateLabel = function () {
    let label = document.createElement('label');
    return label;
}

window.onload = AddIngredient();
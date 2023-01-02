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
    divRow.setAttribute('class', 'row mb-3');

    let ingredientDivCol = CreateDiv();
    ingredientDivCol.setAttribute('class', 'col');

    let ingredientInput = CreateInput();
    ingredientInput.setAttribute('name', 'Ingredients[' + count + '].IngredientName');
    ingredientInput.setAttribute('class', 'form-control');
    ingredientInput.setAttribute('placeholder', 'Съставка');

    let validationForInput = CreateSpan();
    validationForInput.setAttribute('class', 'text-danger');
    validationForInput.setAttribute('asp-validation-for', 'Ingredients[' + count + '].IngredientName');
    ingredientInput.appendChild(validationForInput);

    ingredientDivCol.appendChild(ingredientInput);

    let quantityDivCol = CreateDiv();
    quantityDivCol.setAttribute('class', 'col')

    let quantityInput = CreateInput();
    quantityInput.setAttribute('name', 'Ingredients[' + count + '].Quantity');
    quantityInput.setAttribute('class', 'form-control');
    quantityInput.setAttribute('placeholder', 'Количество');

    let validationForQuantity = CreateSpan();
    validationForQuantity.setAttribute('class', 'text-danger');
    validationForQuantity.setAttribute('asp-validation-for', 'Ingredients[' + count + '].Quantity');
    quantityInput.appendChild(validationForQuantity);

    quantityDivCol.appendChild(quantityInput);

    divRow.appendChild(ingredientDivCol);
    divRow.appendChild(quantityDivCol);

    container.appendChild(divRow);
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

const CreateSpan = function () {
    let span = document.createElement('span');
    return span;
}

window.onload = AddIngredient();
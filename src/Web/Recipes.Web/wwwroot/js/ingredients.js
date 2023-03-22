window.onload = addIngredient();

function addIngredient() {
    let button = document.getElementById('addIngredient');
    let ingredientsContainer = document.getElementById('IngredientsContainer');

    button.onclick = function (ev) {
        ev.preventDefault();
        let num = ingredientsContainer.getElementsByTagName('input').length;
        addIngredientAndQuantity(ingredientsContainer, num);
    }
}

function addIngredientAndQuantity(container, number) {

    let count = Math.ceil(number / 2);

    let divRow = createDiv();
    divRow.setAttribute('class', 'row mb-3');

    let ingredientDivCol = createDiv();
    ingredientDivCol.setAttribute('class', 'col');

    let ingredientInput = createInput();
    ingredientInput.setAttribute('name', 'Ingredients[' + count + '].IngredientName');
    ingredientInput.setAttribute('class', 'form-control');
    ingredientInput.setAttribute('placeholder', 'Съставка');

    let validationForInput = createSpan();
    validationForInput.setAttribute('class', 'text-danger');
    validationForInput.setAttribute('asp-validation-for', 'Ingredients[' + count + '].IngredientName');
    ingredientInput.appendChild(validationForInput);

    ingredientDivCol.appendChild(ingredientInput);

    let quantityDivCol = createDiv();
    quantityDivCol.setAttribute('class', 'col')

    let quantityInput = createInput();
    quantityInput.setAttribute('name', 'Ingredients[' + count + '].Quantity');
    quantityInput.setAttribute('class', 'form-control');
    quantityInput.setAttribute('placeholder', 'Количество');

    let validationForQuantity = createSpan();
    validationForQuantity.setAttribute('class', 'text-danger');
    validationForQuantity.setAttribute('asp-validation-for', 'Ingredients[' + count + '].Quantity');
    quantityInput.appendChild(validationForQuantity);

    quantityDivCol.appendChild(quantityInput);

    divRow.appendChild(ingredientDivCol);
    divRow.appendChild(quantityDivCol);

    container.appendChild(divRow);
}

function createDiv() {
    let div = document.createElement('div');
    return div;
}

function createInput() {
    let input = document.createElement('input');
    return input;
}

function createLabel() {
    let label = document.createElement('label');
    return label;
}

function createSpan() {
    let span = document.createElement('span');
    return span;
}
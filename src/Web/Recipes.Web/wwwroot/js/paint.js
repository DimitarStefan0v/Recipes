const PaintRecipeByIdWrapper = function () {
    const color = document.getElementById('recipe-container-color');
    const wrapper = color.parentElement;
    wrapper.style.backgroundColor = '#' + color.value;
}

window.onload = PaintRecipeByIdWrapper();
const PaintWrapper = function () {
    const color = document.querySelector('.container-color');
    const wrapper = color.parentElement;
    wrapper.style.backgroundColor = color.value;
}

window.onload = PaintWrapper();


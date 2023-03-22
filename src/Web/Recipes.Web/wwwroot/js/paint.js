window.onload = paintWrapper();

function paintWrapper() {
    const color = document.querySelector('.container-color');
    const wrapper = color.parentElement;
    wrapper.style.backgroundColor = color.value;
}
const ScrollToCommentForm = function () {
    let addCommentBtn = document.getElementById('add-recipe-comment');
    const authenticated = document.querySelector('.paragraph-warning');
    let wrapper = document.querySelector('.wrapper-for-comments-elements');

    addCommentBtn.addEventListener('click', (ev) => {
        ev.preventDefault();
        if (authenticated != null) {
            let warning = document.createElement('h3');
            warning.setAttribute('class', 'text-danger text-center');
            warning.textContent = 'Само за регистрирани потребители!';
            wrapper.prepend(warning);
            addCommentBtn.style.display = 'none';
        } else {
            document.getElementById('recipe-comment-form').scrollIntoView({
                behavior: 'smooth'
            });
        }
    });

}

window.onload = ScrollToCommentForm();
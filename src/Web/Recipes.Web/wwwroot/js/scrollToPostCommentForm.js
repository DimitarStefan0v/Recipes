const ScrollToCommentForm = function () {
    let btn = document.querySelector('.forum-btn');
    const authenticated = document.getElementById('check-if-authenticated').value;

    btn.addEventListener('click', (ev) => {
        ev.preventDefault();
        if (authenticated) {
            document.querySelector('.comment-form').scrollIntoView({
                behavior: 'smooth'
            });
        } else {
            document.getElementById('post-anster-warning').style.display = 'block';
        }
    });
}

window.onload = ScrollToCommentForm();
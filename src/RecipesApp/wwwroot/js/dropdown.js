let dropBtns = document.querySelectorAll('.custom-dropdown-btn');

dropBtns.forEach((button => {
    button.addEventListener('click', () => {
        let dropContent = button.parentElement.querySelector(':scope > div');
        if (dropContent.style.display === '') {
            dropContent.style.display = 'block';
        } else {
            dropContent.style.display = '';
        }
    });
}));
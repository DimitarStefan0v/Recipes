const DisableCurrentAnchorSortElement = function () {
    let sortValue = document.querySelector('.input-sort-order-value').value;
    let asc = document.querySelector('.ascending-sort-link');
    let desc = document.querySelector('.descending-sort-link');
    let popularity = document.querySelector('.popularity-sort-link');
    let comments = document.querySelector('.comments-sort-link');
    let borderBottomValue = '5px solid red';

    if (sortValue == 'ascending') {
        asc.style.color = 'red';
        asc.style.borderBottom = borderBottomValue;
        asc.style.pointerEvents = 'none';
        asc.style.cursor = 'default';
    } else if (sortValue == 'descending') {
        desc.style.color = 'red';
        desc.style.borderBottom = borderBottomValue;
        desc.style.pointerEvents = 'none';
        desc.style.cursor = 'default';
    } else if (sortValue == 'popularity') {
        popularity.style.color = 'red';
        popularity.style.borderBottom = borderBottomValue;
        popularity.style.pointerEvents = 'none';
        popularity.style.cursor = 'default';
    } else if (sortValue == 'comments') {
        comments.style.color = 'red';
        comments.style.borderBottom = borderBottomValue;
        comments.style.pointerEvents = 'none';
        comments.style.cursor = 'default';
    } else {
        desc.style.color = 'red';
        desc.style.borderBottom = borderBottomValue;
        desc.style.pointerEvents = 'none';
        desc.style.cursor = 'default';
    }
}

window.onload = DisableCurrentAnchorSortElement();
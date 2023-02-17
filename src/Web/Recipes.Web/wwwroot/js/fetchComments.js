const LoadComments = function () {
    const toggleCommentsBtn = document.getElementById('toggle-comments');
    let commentsWrapper = document.querySelector('.comments-wrapper');
    let commentsCount = document.getElementById('comments-count').value;
    let itemsPerPage = document.getElementById('comments-per-page-count').value;
    let pagesCount = document.getElementById('comments-pages-count').value;
    let id = document.getElementById('recipe-id').value;
    let page = 1;
    const showMoreCommentsBtn = document.getElementById('show-more-comments');

    let hasNextPage = page < pagesCount;

    toggleCommentsBtn.addEventListener('click', async (ev) => {
        ev.preventDefault();

        if (commentsWrapper.style.display == 'block') {
            commentsWrapper.style.display = 'none';
            toggleCommentsBtn.textContent = `Покажи коментари(${commentsCount})`;
        } else {
            commentsWrapper.style.display = 'block'
            toggleCommentsBtn.textContent = `Скрий коментарите(${commentsCount})`;
        }

        if (page == 1) {
            const url = `/Comments/LoadRecipeComments/${id}?page=${page}&itemsPerFetch=${itemsPerPage}`;
            const response = await fetch(url);
            const data = await response.json();

            if (data.length == 0) {
                let noCommentsMessage = createTagWithAttribute('h3', 'class', 'text-center');
                noCommentsMessage.textContent = 'Все още няма коментари към рецептата.';
                commentsWrapper.appendChild(noCommentsMessage);
            }

            createComment(data);
            page += 1;
        }

    });

    if (hasNextPage == true) {
        showMoreCommentsBtn.addEventListener('click', async (ev) => {
            const url = `/Comments/LoadRecipeComments/${id}?page=${page}&itemsPerFetch=${itemsPerPage}`;
            let hasNextPage = page < pagesCount;
            if (hasNextPage == false) {
                showMoreCommentsBtn.style.display = 'none';
            }

            page += 1;

            const response = await fetch(url);
            const data = await response.json();

            createComment(data);

            if (hasNextPage == false) {
                showMoreCommentsBtn.style.display = 'none';
            }
        });
    } else {
        showMoreCommentsBtn.style.display = 'none';
    }

    function createComment(data) {
        for (const comment of data) {
            // create comment wrapper
            let commentWrapper = createTagWithAttribute('article', 'class', 'recipe-comment-wrapper');

            // create comment header
            let commentHeader = createTagWithAttribute('article', 'class', 'recipe-comment-wrapper-header');

            // create comment header author
            let commentHeaderAuthor = createTagWithAttribute('p', 'class', 'recipe-comment-wrapper-header-author');
            commentHeaderAuthor.textContent = comment.addedByUserUserName;

            // append comment header author to comment header
            commentHeader.appendChild(commentHeaderAuthor);

            // create comment header date
            let commentHeaderDateWrapper = createTagWithAttribute('p', 'class', 'recipe-comment-wrapper-header-date');
            commentHeaderDateWrapper.textContent = comment.dateAsForRecipeCommentString;

            // append comment header date to comment header
            commentHeader.appendChild(commentHeaderDateWrapper);

            // append comment header to comment wrapper
            commentWrapper.appendChild(commentHeader);

            // create comment content
            let commentContent = createTagWithAttribute('article', 'class', 'recipe-comment-wrapper-content');

            // create comment content text
            let commentContentText = createTagWithAttribute('p', 'class', 'recipe-comment-wrapper-content-text');
            commentContentText.textContent = comment.content;

            // append comment content text to comment content
            commentContent.appendChild(commentContentText);

            // append comment content to comment wrapper
            commentWrapper.appendChild(commentContent);

            // append comment wrapper to comments wrapper before show more comment btn
            commentsWrapper.insertBefore(commentWrapper, showMoreCommentsBtn);
        }
    }

    function createTagWithAttribute(tagName, attributeName, attributeValue) {
        let tag = document.createElement(tagName);
        tag.setAttribute(attributeName, attributeValue);
        return tag;
    }
}

window.onload = LoadComments();
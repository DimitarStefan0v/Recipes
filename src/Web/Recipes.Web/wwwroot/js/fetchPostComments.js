const LoadComments = function () {
    const btn = document.querySelector('.forum-btn.comments');
    let commentsWrapper = document.querySelector('.post-byId-wrapper');
    let postName = document.getElementById('post-content-value').value;

    btn.addEventListener('click', async (ev) => {
        const id = document.getElementById('post-id').value;
        const url = '/Comments/LoadComments/' + id;
        const response = await fetch(url);
        const data = await response.json();

        for (const comment of data) {
            // create comment article wrapper
            let postComment = createTag('article');
            postComment = setTagAttribute(postComment, 'class', 'post-comment');

            // create comment article header
            let postCommentHeader = createTag('article');
            postCommentHeader = setTagAttribute(postCommentHeader, 'class', 'post-comment-header');

            // create article with comment author name and append it to postCommentHeader
            let postCommentHeaderArticleForAuthor = createTag('article');
            postCommentHeaderArticleForAuthor.textContent = comment.addedByUserUserName;
            postCommentHeader.appendChild(postCommentHeaderArticleForAuthor);

            // create article with nested tag time with attribute datetime and value dateAsString and append it to postCommentHeader
            let postCommentHeaderArticleForDateWrapper = createTag('article');
            let postCommentHeaderArticleForDate = createTag('time');
            postCommentHeaderArticleForDate = setTagAttribute(postCommentHeaderArticleForDate, 'datetime', comment.dateAsString);
            postCommentHeaderArticleForDateWrapper.appendChild(postCommentHeaderArticleForDate);
            postCommentHeader.appendChild(postCommentHeaderArticleForDateWrapper);

            // append the post comment header to post comment
            postComment.appendChild(postCommentHeader);

            // create post comment content wrapper
            let postCommentContent = createTag('article');
            postCommentContent = setTagAttribute(postCommentContent, 'class', 'post-comment-content');

            // create post comment content paragraph for post name and append it to postCommentContent
            let postCommentContentParagraphForPostTitle = createTag('p');
            postCommentContentParagraphForPostTitle.textContent = 'Тема: ' + postName;
            postCommentContent.appendChild(postCommentContentParagraphForPostTitle);
            let hr = createTag('hr');
            postCommentContent.appendChild(hr);

            // create post comment content paragraph for comment content and append it to postCommentContent
            let postCommentContentParagraphForCommentContent = createTag('p');
            postCommentContentParagraphForCommentContent.textContent = comment.content;
            postCommentContent.appendChild(postCommentContentParagraphForCommentContent);

            // append the post comment content to post comment
            postComment.appendChild(postCommentContent);

            // append the finished comment to commentsWrapper
            commentsWrapper.insertBefore(postComment, btn);
            //commentsWrapper.appendChild(postComment);
        }

    });

    function createTag(tagName) {
        let tag = document.createElement(tagName);
        return tag;
    }

    function setTagAttribute(tag, attribute, value) {
        tag.setAttribute(attribute, value);
        return tag;
    }
}

window.onload = LoadComments();
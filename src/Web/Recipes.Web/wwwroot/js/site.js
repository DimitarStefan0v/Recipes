$(function () {
    moment.locale('bg');
    $('time').each(function (i, e) {
        const dateTimeValue = $(e).attr('datetime');
        if (!dateTimeValue) {
            return;
        }

        const time = moment.utc(dateTimeValue).local();
        $(e).html(time.format('LLL'));
        $(e).attr('title', $(e).attr('datetime'));
    });
});


let backToTopButton = document.getElementById('btn-back-to-top');
backToTopButton.addEventListener('click', backToTop);

window.onscroll = function () {
    scrollFunction();
};

function scrollFunction() {
    if (
        document.body.scrollTop > 20 ||
        document.documentElement.scrollTop > 20
    ) {
        backToTopButton.style.display = 'block';
    } else {
        backToTopButton.style.display = 'none';
    }
}

function backToTop() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}
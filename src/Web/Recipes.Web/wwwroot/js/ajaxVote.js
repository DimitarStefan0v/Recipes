const SendVote = function () {
    let elements = document.querySelectorAll('.votes-container .votes i');

    for (let el of elements) {
        el.addEventListener('click', async () => {
            CheckIfUserShouldVote();
            const value = el.getAttribute('data-vote');
            const recipeId = document.getElementById('recipe-id').value;
            const antiForgeryToken = document.querySelector('#antiForgeryForm input[name=__RequestVerificationToken]').value;
            let data = { recipeId: recipeId, value: value };
            const url = '/api/Votes';
            const options = {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': antiForgeryToken,
                },
                body: JSON.stringify(data),
            };

            const response = await fetch(url, options);
            const result = await response.json();

            document.getElementById('avg-votes').textContent = result.averageVote.toFixed(1) + ' / 5';
            document.getElementById('votes-count').textContent = 'Общо гласували: ' + result.votesCount;
        });
    }
}

const CheckIfUserShouldVote = function () {
    let p = document.querySelector('.paragraph-warning');
    if (p != null && p.textContent == 'unregistered') {
        p.textContent = 'Само за регистрирани потребители!!!';
        p.style.display = 'block';
        let i = document.getElementById('first-star');
        let ul = i.parentElement.parentElement;
        let sectionFavorites = document.querySelector('.votes-container .favorites');
        let heartToRemove = sectionFavorites.querySelector('i');
        let paragrapthToRemove = document.getElementById('favorites-p');
        ul.style.display = 'none';
        sectionFavorites.removeChild(heartToRemove);
        sectionFavorites.removeChild(paragrapthToRemove);
    }
}

window.onload = SendVote();

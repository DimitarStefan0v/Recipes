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
    let authenticated = document.querySelector('.paragraph-warning');
    if (authenticated != null && authenticated.textContent == 'unregistered') {
        authenticated.textContent = 'Само за регистрирани потребители!';
        authenticated.style.display = 'block';
        let voteContainer = document.querySelector('.votes');
        voteContainer.style.display = 'none';
        let favoritesContainer = document.querySelector('.favorites');
        favoritesContainer.style.display = 'none';
    }
}

window.onload = SendVote();

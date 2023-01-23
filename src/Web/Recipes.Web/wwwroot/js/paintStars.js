const AddClassesToStars = function () {
    const voteValue = document.getElementById('avg-votes-hidden').textContent;
    let firstStar = document.getElementById('first-star');
    let secondStar = document.getElementById('second-star');
    let thirdStar = document.getElementById('third-star');
    let fourthStar = document.getElementById('fourth-star');
    let fifthStar = document.getElementById('fifth-star');

    switch (true) {
        case voteValue > 0 && voteValue < 1:
            firstStar.setAttribute('class', 'fa-regular fa-star-half-stroke');
            break;
        case voteValue == 1:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            break;
        case voteValue > 1 && voteValue < 2:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-regular fa-star-half-stroke');
            break;
        case voteValue == 2:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-solid fa-star');
            break;
        case voteValue > 2 && voteValue < 3:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-solid fa-star');
            thirdStar.setAttribute('class', 'fa-regular fa-star-half-stroke');
            break;
        case voteValue == 3:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-solid fa-star');
            thirdStar.setAttribute('class', 'fa-solid fa-star');
            break;
        case voteValue > 3 && voteValue < 4:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-solid fa-star');
            thirdStar.setAttribute('class', 'fa-solid fa-star');
            fourthStar.setAttribute('class', 'fa-regular fa-star-half-stroke');
            break;
        case voteValue == 4:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-solid fa-star');
            thirdStar.setAttribute('class', 'fa-solid fa-star');
            fourthStar.setAttribute('class', 'fa-solid fa-star');
            break;
        case voteValue > 4 && voteValue < 5:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-solid fa-star');
            thirdStar.setAttribute('class', 'fa-solid fa-star');
            fourthStar.setAttribute('class', 'fa-solid fa-star');
            fifthStar.setAttribute('class', 'fa-regular fa-star-half-stroke');
            break;
        case voteValue == 5:
            firstStar.setAttribute('class', 'fa-solid fa-star');
            secondStar.setAttribute('class', 'fa-solid fa-star');
            thirdStar.setAttribute('class', 'fa-solid fa-star');
            fourthStar.setAttribute('class', 'fa-solid fa-star');
            fifthStar.setAttribute('class', 'fa-solid fa-star');
            break;
        default:
            break;
    }
}

window.onload = AddClassesToStars();
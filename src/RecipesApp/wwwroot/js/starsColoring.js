const FillStart = function () {
    let starOne = document.getElementById('firstStar');
    let starTwo = document.getElementById('secondStar');
    let starThree = document.getElementById('thirdStar');
    let starFour = document.getElementById('fourthStar');
    let starFive = document.getElementById('fifthStar');

    let voteValue = document.getElementById('span-avg-votes').textContent;

    if (voteValue == 5) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-solid fa-star');
        starThree.setAttribute('class', 'fa-solid fa-star');
        starFour.setAttribute('class', 'fa-solid fa-star');
        starFive.setAttribute('class', 'fa-solid fa-star');
    } else if (voteValue > 4 && voteValue < 5) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-solid fa-star');
        starThree.setAttribute('class', 'fa-solid fa-star');
        starFour.setAttribute('class', 'fa-solid fa-star');
        starFive.setAttribute('class', 'fa-regular fa-star-half-stroke');
    } else if (voteValue >= 4) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-solid fa-star');
        starThree.setAttribute('class', 'fa-solid fa-star');
        starFour.setAttribute('class', 'fa-solid fa-star');
    } else if (voteValue > 3 && voteValue < 4) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-solid fa-star');
        starThree.setAttribute('class', 'fa-solid fa-star');
        starFour.setAttribute('class', 'fa-regular fa-star-half-stroke');
    } else if (voteValue >= 3) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-solid fa-star');
        starThree.setAttribute('class', 'fa-solid fa-star');
    } else if (voteValue > 2 && voteValue < 3) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-solid fa-star');
        starThree.setAttribute('class', 'fa-regular fa-star-half-stroke');
    } else if (voteValue >= 2) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-solid fa-star');
    } else if (voteValue > 1 && voteValue < 2) {
        starOne.setAttribute('class', 'fa-solid fa-star');
        starTwo.setAttribute('class', 'fa-regular fa-star-half-stroke');
    } else if (voteValue >= 1) {
        starOne.setAttribute('class', 'fa-solid fa-star');
    } else if (voteValue > 0 && voteValue < 1) {
        starOne.setAttribute('class', 'fa-regular fa-star-half-stroke');
    }   
}

window.onload = FillStart();
const options = ["rock", "paper", "scissors"];

// Element selectors
const playAgainBtn = document.querySelector('save-btn');
const images = document.querySelectorAll("img");
const gameTopMessage = document.getElementById('top-message');
const gameBottomMessage = document.getElementById("bottom-message");
const saveButton = document.getElementById("save-btn");
const rockWinStats = document.getElementById("rock-wins");
const paperWinStats = document.getElementById("paper-wins");
const scissorWinStats = document.getElementById("scissors-wins"); 
const currentWinStats = document.getElementById("current-wins-count");
const highScoreCount = document.getElementById("high-score-count");
const totalGamesStats = document.getElementById("total-games");


let rockWins = 0;
let paperWins = 0;
let scissorsWins = 0;
let currentWins = 0;
let highScore = 0;
let totalGames = 0;
let counter;

function computerPlay() {
    return options[Math.floor(Math.random() * options.length)];
}

function playRound(playerSelection, computerSelection) {
    playerSelection = playerSelection.toLowerCase();
    computerSelection = computerSelection.toLowerCase();

    if (playerSelection === computerSelection) {
        totalGames++;
        return "It's a tie!";
    } else if (
        (playerSelection === "rock" && computerSelection === "scissors") ||
        (playerSelection === "paper" && computerSelection === "rock") ||
        (playerSelection === "scissors" && computerSelection === "paper")
    ) {
        switch (playerSelection) {
            case "rock":
                rockWins++;
                totalGames++;
                break;
            case "paper":
                paperWins++;
                totalGames++;
                break;
            case "scissors":
                totalGames++;
                scissorsWins++;
                break;
        }
        currentWins++;
        if (currentWins > highScore) {
            highScore = currentWins;
        }
        return "You win!";
    } else {
        currentWins = 0;
        totalGames++;
        return "You lose!";
    }
}


images.forEach((image) => {
    image.addEventListener("click", () => {

        const playerSelection = image.id;
        const computerSelection = computerPlay();
        const result = playRound(playerSelection, computerSelection);

        gameTopMessage.textContent = `Weapon locked!`;

        removeSelected();
        image.classList.add('weapon-selected');

        // Adds countdown before showing what computer chose
        counter = 3;
        startCountDown(result, computerSelection);

    });
});


saveButton.addEventListener('click', () => {
    sendData();
    resetStats();
    saveButton.blur();
});


function startCountDown(result, computerSelection) {
    const interval = setInterval(function countDown() {
            disableClick();
            if (counter > 0) {
                gameBottomMessage.textContent = counter;
            } else {
                gameTopMessage.textContent = `Your opponent chose ${computerSelection}`;
                gameBottomMessage.textContent = result;
                updateGameStats();
                clearInterval(interval);
                enableClick();
            }

        counter--;
        return countDown;
    }(), 1000);
}

function updateGameStats() {
    rockWinStats.textContent = rockWins;
    paperWinStats.textContent = paperWins;
    scissorWinStats.textContent = scissorsWins;
    currentWinStats.textContent = currentWins;
    highScoreCount.textContent = highScore;
    totalGamesStats.textContent = totalGames;
}

function disableClick() {
    images.forEach(image => image.classList.add('disable-clicks'));
}

function enableClick() {
    images.forEach(image => image.classList.remove('disable-clicks'));
}

function removeSelected() {
    images.forEach(image => image.classList.remove('weapon-selected'));
}

async function sendData() {
    const data = [rockWins, paperWins, scissorsWins, highScore, totalGames];
    await fetch('/RockPaperScissors',
        {
            method: 'POST',
            headers: {
                'Content-type': 'application/json'
            },
            body: JSON.stringify(data)
        }
    );
};

function resetStats() {
    rockWins = 0;
    paperWins = 0;
    scissorsWins = 0;
    currentWins = 0;
    highScore = 0;
    totalGames = 0;
    updateGameStats();
}

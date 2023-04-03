const options = ["rock", "paper", "scissors"];

const playAgainBtn = document.querySelector('save-btn');

let rockWins = 0;
let paperWins = 0;
let scissorsWins = 0;
let currentWins = 0;
let highScore = 0;
let totalGames = 0;

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
        return "You win! " + playerSelection + " beats " + computerSelection ;
    } else {
        currentWins = 0;
        totalGames++;
        return "You lose! " + computerSelection + " beats " + playerSelection;
    }
}

const images = document.querySelectorAll("img");
images.forEach((image) => {
    image.addEventListener("click", () => {
        const playerSelection = image.id;
        const computerSelection = computerPlay();
        const result = playRound(playerSelection, computerSelection);
        document.getElementById("result").textContent = result;
        document.getElementById("rock-wins").textContent = rockWins;
        document.getElementById("paper-wins").textContent = paperWins;
        document.getElementById("scissors-wins").textContent = scissorsWins;
        document.getElementById("current-wins-count").textContent = currentWins;
        document.getElementById("high-score-count").textContent = highScore;
        document.getElementById("Total-Games").textContent = totalGames;
    });
});

const saveButton = document.getElementById("save-btn");
saveButton.addEventListener('click', () => {
    sendData();
});

async function sendData() {
    const data = [rockWins, paperWins, scissorsWins, highScore];
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
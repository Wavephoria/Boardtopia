const options = ["rock", "paper", "scissors"];

const playAgainBtn = document.querySelector('save-btn');

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

        // Adds countdown before showing what computer chose
        counter = 3;
        const interval = setInterval(function countDown() {
            if (counter > 0) {
                document.getElementById("result").textContent = counter;
            } else {
                document.getElementById("result").textContent = result;
                clearInterval(interval);
            }

            counter--;

            return countDown;
        }(), 1000);

        removeSelected();
        image.classList.add('weapon-selected');

        
        document.getElementById("rock-wins").textContent = rockWins;
        document.getElementById("paper-wins").textContent = paperWins;
        document.getElementById("scissors-wins").textContent = scissorsWins;
        document.getElementById("current-wins-count").textContent = currentWins;
        document.getElementById("high-score-count").textContent = highScore;
        document.getElementById("total-games").textContent = totalGames;
    });
});

const saveButton = document.getElementById("save-btn");
saveButton.addEventListener('click', () => {
    sendData();
    resetStats();
});


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
    document.getElementById("rock-wins").textContent = rockWins;
    document.getElementById("paper-wins").textContent = paperWins;
    document.getElementById("scissors-wins").textContent = scissorsWins;
    document.getElementById("current-wins-count").textContent = currentWins;
    document.getElementById("high-score-count").textContent = highScore;
    document.getElementById("total-games").textContent = total
}

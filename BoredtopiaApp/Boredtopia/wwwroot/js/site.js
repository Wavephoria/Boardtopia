
// Queryselectors
const gameBoard = document.querySelector('.game-board');

// Add game variables
const WORD_LENGTH = 6;
const NUMBER_OF_GUESSES = 6;
let currentGuess = '';
let currentNum = 0;

// Word during development
const word = "REACT";


// Create game board
function createGameBoard() {
    for (let i = 0; i < NUMBER_OF_GUESSES; i++) {

        const row = document.createElement('div');
        row.classList.add('board-row');
        gameBoard.appendChild(row);

        for (let j = 0; j < WORD_LENGTH; j++) {
            const wordBox = document.createElement('div');
            wordBox.classList.add('word-box');
            row.appendChild(wordBox);
        }
    }
}


// Add event listener
document.addEventListener('keyup', (e) => {
    console.log(e);
});




// Init game
createGameBoard();
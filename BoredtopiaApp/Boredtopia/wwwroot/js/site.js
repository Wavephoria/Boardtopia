
// Queryselectors
const gameBoard = document.querySelector('.game-board');

// Add game variables
const WORD_LENGTH = 5;
const NUMBER_OF_GUESSES = 6;
let currentGuess = '';
let currentNum = 0;

const regEx = /^[A-Za-zåäöÅÄÖ]$/;

// Word during development
const word = "REACT";


// Create game board
function createGameBoard() {
    for (let i = 0; i < NUMBER_OF_GUESSES; i++) {

        const row = document.createElement('div');
        row.dataset.row = `row${i + 1}`;
        row.classList.add('board-row');
        gameBoard.appendChild(row);

        for (let j = 0; j < WORD_LENGTH; j++) {
            const letterBox = document.createElement('div');
            letterBox.classList.add('word-box');
            row.appendChild(letterBox);
        }
    }
}


// Add event listener
document.addEventListener('keyup', (e) => {
    const row = document.querySelector('[data-row="row1"]');
    if (regEx.test(e.key) && currentNum < WORD_LENGTH) {
        row.children[currentGuess.length].textContent = e.key.toUpperCase();
        currentGuess += e.key;
    } else if (e.key === 'Enter' && currentGuess.length === WORD_LENGTH) {
        console.log(e.key);
    } else if (e.key === 'Backspace') {
        console.log(e.key);
    }
});


// Init game
createGameBoard();
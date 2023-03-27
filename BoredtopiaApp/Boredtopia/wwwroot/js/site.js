﻿
// Queryselectors
const gameBoard = document.querySelector('.game-board');

// Add game variables
const WORD_LENGTH = 5;
const NUMBER_OF_GUESSES = 6;
let currentGuess = '';
let currentNum = 0;

const regEx = /^[A-Za-zåäöÅÄÖ]$/;

// Word during development
const correctWord = "REACT";


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

function checkIfGuessIsCorrect() {
    if (currentGuess.toLowerCase() === correctWord.toLowerCase()) {
        gameOver = true;
        gameOverMessage.textContent = 'You Win!';
    }

    if (currentNum === NUMBER_OF_GUESSES) {
        gameover = true;
        if (currentGuess.toLowerCase() === correctWord.toLowerCase()) {
            gameOverMessage.textContent = 'You Win!';
        } else {
            gameOverMessage.textContent = 'You Lose!';
        }
    }
}


// Add event listener
document.addEventListener('keyup', (e) => {
    if (currentNum < NUMBER_OF_GUESSES) {
        const row = document.querySelector(`[data-row=row${currentNum + 1}]`);
        if (regEx.test(e.key) && currentGuess.length < WORD_LENGTH) {
            row.children[currentGuess.length].textContent = e.key.toUpperCase();
            currentGuess += e.key;
        } else if (e.key === 'Enter' && currentGuess.length === WORD_LENGTH) {
            currentGuess = '';
            currentNum++;
        } else if (e.key === 'Backspace' && currentGuess.length > 0) {
            row.children[currentGuess.length - 1].textContent = '';
            currentGuess = currentGuess.slice(0, -1);
        }

        if (e.key === 'Enter' && currentGuess.length === WORD_LENGTH) {
            currentNum++;

            checkIfGuessIsCorrect();

            currentGuess = '';
        }
    }
});


// Init game
createGameBoard();
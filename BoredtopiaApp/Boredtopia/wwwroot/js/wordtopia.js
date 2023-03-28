﻿import { WORDS_ENGLISH } from './words-english.js';
import { WORDS_SWEDISH } from './words-swedish.js';

// QuerySelectors
const gameBoard = document.querySelector('.game-board');
const gameOverDiv = document.querySelector('.game-over-div');
const gameOverMessage = document.querySelector('.game-over-message');
const playAgainBtn = document.querySelector('.btn-play-again');

// Game variables
const WORD_LENGTH = 5;
const NUMBER_OF_GUESSES = 6;
let currentGuess = '';
let currentNum = 0;
let gameOver = false;
let correctWord;
let wordArray = WORDS_ENGLISH;

const regEx = /^[A-Za-zåäöÅÄÖ]$/;

function getRandomWord(wordArray) {
    let random = Math.floor(Math.random() * wordArray.length);
    correctWord = wordArray[random].toLowerCase();
}

function createGameBoard() {
    for (let i = 0; i < NUMBER_OF_GUESSES; i++) {

        const row = document.createElement('div');
        row.dataset.row = `row${i + 1}`;
        row.classList.add('board-row');
        gameBoard.appendChild(row);

        for (let j = 0; j < WORD_LENGTH; j++) {
            const letterBox = document.createElement('div');
            letterBox.classList.add('letter-box');
            row.appendChild(letterBox);
        }
    }
}

function checkIfGuessIsCorrect() {
    if (currentGuess === correctWord) {
        gameOver = true;
        gameOverMessage.textContent = 'You Win!';
        gameOverDiv.classList.remove('hidden');
    }

    if (currentNum === NUMBER_OF_GUESSES) {
        gameOver = true;
        if (currentGuess === correctWord) {
            gameOverMessage.textContent = 'You Win!';
        } else {
            gameOverMessage.textContent = `You Lose! The correct word was ${correctWord}`;
        }
        gameOverDiv.classList.remove('hidden');
    }
}

function AddColorsToLetterBox(row) {
    for (let i = 0; i < WORD_LENGTH; i++) {
        if (currentGuess.charAt(i) === correctWord.charAt(i)) {
            row.children[i].classList.add('letter-box__green');
        } else if (correctWord.includes(currentGuess.charAt(i))) {
            row.children[i].classList.add('letter-box__yellow');
        } else {
            row.children[i].classList.add('letter-box__grey');
        }
    }
}

// Event listener for keyboard event
document.addEventListener('keyup', (e) => {
    if (gameOver) {
        return;
    }

    console.log(correctWord);

    const row = document.querySelector(`[data-row=row${currentNum + 1}]`);

    if (currentNum < NUMBER_OF_GUESSES) {
        if (regEx.test(e.key) && currentGuess.length < WORD_LENGTH) {
            row.children[currentGuess.length].textContent = e.key.toUpperCase();
            currentGuess += e.key.toLowerCase();
        }

        if (e.key === 'Backspace' && currentGuess.length > 0) {
            row.children[currentGuess.length - 1].textContent = '';
            currentGuess = currentGuess.slice(0, -1);
        }

        if (e.key === 'Enter' && currentGuess.length === WORD_LENGTH) {
            currentNum++;

            AddColorsToLetterBox(row);
            checkIfGuessIsCorrect();

            currentGuess = '';
        }
    }
});

playAgainBtn.addEventListener('click', () => {
    currentNum = 0;
    gameOver = false;
    currentGuess = '';
    gameOverDiv.classList.add('hidden');
    getRandomWord(wordArray);

    // Clear and recreate board
    gameBoard.replaceChildren();
    createGameBoard();

    // Remove focus from button
    playAgainBtn.blur();
});

// Init game
getRandomWord(wordArray);
createGameBoard();
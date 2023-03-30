// create game variables
const gameBoard = document.querySelector('.puzzle-board');
const rowSize = 3;
const numberOfTiles = rowSize ** 2;


function createBoard() {
    for (let i = 1; i <= numberOfTiles; i++) {
        const tile = document.createElement('div');
        tile.classList.add(`tile${i}`);
        tile.addEventListener('click', moveTile);
        gameBoard.appendChild(tile);
    }


    // - add eventlistener to each tile (move on click)
}



// shuffle tiles
// - create random function
// - shuffle tiles to random locations



// move tile to empty spot
// - check for empty spot
// - move tile if it is next to empty spot
// - call check if player has won function
function moveTile() {
    console.log("hello");
}


// start game
// - call create board function
// - call shuffle tiles function
// - check that it's not in win state

function startGame() {
    createBoard();
}


// check if player has won (puzzle completed)
// - check if puzzle board is in win state
// - if won end game with win message



startGame();
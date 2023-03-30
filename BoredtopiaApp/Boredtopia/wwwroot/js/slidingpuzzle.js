// create game variables
const gameBoard = document.querySelector('.puzzle-board');
const rowSize = 3;
const numberOfTiles = rowSize ** 2;

// Empty tile starts at the last tile
let emptyTile = numberOfTiles;

function createBoard() {
    for (let i = 1; i <= numberOfTiles; i++) {
        const tile = document.createElement('div');
        tile.dataset.tileNumber = i;
        tile.classList.add(`picture-component${i}`);
        tile.addEventListener('click', () => { moveTile(tile) });
        gameBoard.appendChild(tile);
    }
}



// shuffle tiles
// - create random function
// - shuffle tiles to random locations



// move tile to empty spot
// - check for empty spot
// - move tile if it is next to empty spot
// - call check if player has won function
function moveTile(tile) {
    const tileNumber = parseInt(tile.dataset.tileNumber);
    if (tileNumber !== emptyTile) {
        // Move right
        if ((tileNumber + 1) === emptyTile) {
            console.log("tile to the right");
        // Move left
        } else if (tileNumber - 1 === emptyTile) {
            console.log("tile to the left");
        }
    }
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
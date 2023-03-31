// create game variables
const gameBoard = document.querySelector('.puzzle-board');
const rowSize = 3;
const numberOfTiles = rowSize ** 2;

// Empty tile starts at the last tile
let emptyTileNumber = numberOfTiles;

function createBoard() {
    for (let i = 1; i <= numberOfTiles; i++) {
        const tile = document.createElement('div');
        tile.dataset.tileNumber = i;
        if (i === 9) {
            tile.classList.add('.empty')
        } else {
            tile.classList.add(`picture-component${i}`);
        }
        tile.addEventListener('click', () => { moveTile(tile) });
        gameBoard.appendChild(tile);
    }

}


function shuffleTiles() {

    const set = new Set();
    while (set.size !== 8) {
        set.add(Math.floor(Math.random() * 8) + 1);
    }
    const numbers = [...set];
    const tiles = gameBoard.children;

    for (let i = 0; i < tiles.length - 1; i++) {
        tiles[i].className = `picture-component${numbers[i]}`;
    }
}


function moveTile(tile) {
    const emptyTile = document.querySelector(`[data-tile-number="${emptyTileNumber}"]`);
    const tileNumber = parseInt(tile.dataset.tileNumber);

    if (tileNumber !== emptyTileNumber) {

        // Move right
        if ((tileNumber + 1) === emptyTileNumber) {

            if (emptyTileNumber % 3 !== 1) {
                swapPicture(emptyTile, tile, tileNumber);
            }
            
        // Move left
        } else if ((tileNumber - 1) === emptyTileNumber) {

            if (emptyTileNumber % 3 !== 0) {
                swapPicture(emptyTile, tile, tileNumber);               
            }

        // Move up
        } else if ((tileNumber + 3) === emptyTileNumber) {
            swapPicture(emptyTile, tile, tileNumber);

        // Move down
        } else if ((tileNumber - 3) === emptyTileNumber) {
            swapPicture(emptyTile, tile, tileNumber);
        } 
    }

    // - call check if player has won function
    if (checkWinConditions()) {
        replaceWithPicture();
    }
}


function swapPicture(emptyTile, tile, tileNumber) {
    emptyTile.className = tile.className;
    tile.className = `empty`;
    emptyTileNumber = tileNumber;
}


function checkWinConditions() {
    const tiles = gameBoard.children
    let allTilesInRightPlace = true;

    for (let i = 0; i < tiles.length - 1; i++) {
        if (tiles[i].className !== `picture-component${tiles[i].dataset.tileNumber}`) {
            allTilesInRightPlace = false;
        }
    }

    return allTilesInRightPlace;
}


function replaceWithPicture() {
    // Remove all children and eventlisteners etc.
    while (gameBoard.firstChild) {
        gameBoard.removeChild(gameBoard.firstChild);
    }

    gameBoard.classList.remove('game-grid');

    const img = new Image(600, 600);
    img.src = '../image/cat.jpg';   
    gameBoard.appendChild(img);
}


// start game
// - call create board function
// - call shuffle tiles function
// - check that it's not in win state
function startGame() {
    createBoard();

    // Check that it doesn't start in winning state
    while (checkWinConditions()) {
        shuffleTiles();
    }
}

startGame();
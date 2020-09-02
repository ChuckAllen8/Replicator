// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var desiredInterval = 100;

var gridWidth = 1200;
var gridHeight = 780;
var cellWidth = 6;
var cellHeight = 6;
var cellsPerRow = gridWidth / cellWidth;
var rowsOfCells = gridHeight / cellHeight;
var grid = [];
var lastGrid = [];

var canvas = document.getElementById("gol");
var display = canvas.getContext("2d");

var gameRunning;

function startGame() {
	if (gameRunning) {
		clearInterval(gameRunning);
	}
	createGrid();
	gameLoop();
}

function gameLoop() {
	gameRunning = setInterval(() => next(), desiredInterval);
}

function next() {
	lastGrid = grid;
	nextGrid();
	updateHTML();
}

function createGrid() {
	grid = [];
	for (var j = 0; j < rowsOfCells; j++) {
		var line = new Array(cellsPerRow).fill(false);
		for (var i = 0; i < cellsPerRow; i++) {
			if (Math.floor(Math.random() * 10) < 7) {
				line[i] = true;
			}
		}
		grid.push(line);
	}
}

function nextGrid() {
	grid = [];
	for (var thisRow = 0; thisRow < rowsOfCells; thisRow++) {
		var newline = new Array(cellsPerRow).fill(false);
		for (var thisCol = 0; thisCol < cellsPerRow; thisCol++) {
			var neighbors = countNeighbors(thisRow, thisCol);
			newline[thisCol] = lastGrid[thisRow][thisCol];
			if (neighbors > 3) {
				newline[thisCol] = false;
			}
			if (neighbors < 2) {
				newline[thisCol] = false;
			}
			if (neighbors === 3 && !(lastGrid[thisRow][thisCol])) {
				newline[thisCol] = true;
			}
		}
		grid.push(newline);
	}
}

function countNeighbors(x, y) {
	var count = 0;
	for (var checkRow = x - 1; checkRow <= x + 1; checkRow++) {
		for (var checkCol = y - 1; checkCol <= y + 1; checkCol++) {
			if (checkRow >= 0 && checkRow < rowsOfCells) {
				if (checkCol >= 0 && checkCol < cellsPerRow) {
					if (!(checkCol == y && checkRow == x) && lastGrid[checkRow][checkCol]) {
						count++;
					}
				}
			}
		}
	}
	return count;
}

function updateHTML() {
	document.body.style.background = "url(" + canvas.toDataURL() + ") no-repeat fixed center center /cover";
	//display.globalAlpha = 0.5; //opacity, causes shadow effect
	for (var row = 0; row < rowsOfCells; row++) {
		for (var col = 0; col < cellsPerRow; col++) {
			if (grid[row][col]) {
				display.fillStyle = "#000000";
			}
			else {
				display.fillStyle = "#FFFFFF";
			}
			display.fillRect(col * cellWidth, row * cellHeight, cellWidth, cellHeight);
		}
	}
}

$(document).ready(function () {
	startGame();//ready function will be executed after document elements are loaded.
});
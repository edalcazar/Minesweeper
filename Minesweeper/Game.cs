using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Game
    {
        int columns;
        int rows;
        bool mineHit = false;
        Tile[,] tiles;
        TileState[,] tileStates;

        int totalMines;
        int tilesUntouched;
        int flagsLeft;

        public int Rows
        {
            get
            {
                return rows;
            }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
        }

        #region Initialization methods

        /// <summary>
        /// Constructor: Initialize the game with the number of rows, columns, and mines
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="numberOfMines"></param>
        public Game(int rows, int columns, int numberOfMines)
        {
            Exception mineOverflowException = new 
                Exception("Number of mines exceeds number of squares! Game board can not be drawn.");
            try
            {
                if (rows * columns < numberOfMines)
                    throw mineOverflowException;
                this.rows = rows;
                this.columns = columns;
                totalMines = numberOfMines;
                tiles = new Tile[rows, columns];
                tileStates = new TileState[rows, columns];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Adds each tile to the game object
        /// </summary>
        /// <param name="tile">The Tile instance to add</param>
        public void AddTile(Tile tile)
        {
            tiles[tile.Row, tile.Column] = tile;
        }

        /// <summary>
        /// Initialize a new game by unflipping all tiles and laying new mines
        /// </summary>
        public void LayMines()
        {
            tilesUntouched = rows * columns;
            flagsLeft = totalMines;
            mineHit = false;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    tileStates[row, col] = new TileState();
                    tiles[row, col].SetStatus(TileStatus.Unflippped);
                    if (row == 0 && col == 0)
                        tiles[row, col].Focus();
                }
            }

            // Lay the mines randomly
            int mines = 0;
            Random randomNumber = new Random();
            while (mines < totalMines)
            {
                int row = randomNumber.Next(0, rows);
                int col = randomNumber.Next(0, columns);
                
                if (tileStates[row, col].Mine)   //Skip if already mined
                    continue;
                tileStates[row, col].Mine = true;
                // Set warnings for each surrounding tile
                PopulateWarnings(row, col);
                mines++;
            }
        }

        /// <summary>
        /// Increment the warning count for each surrounding tile
        /// </summary>
        /// <param name="row">Row location of a mined tile</param>
        /// <param name="col">Column location of a mined tile</param>
        void PopulateWarnings(int row, int col)
        {
            // Get surrounding tile locations, without exceeding limits of grid
            int previousRow = row == 0 ? 0 : row - 1;
            int nextRow = row >= rows - 1 ? rows - 1 : row + 1;
            int previousCol = col == 0 ? 0 : col - 1;
            int nextCol = col >= columns - 1 ? columns - 1 : col + 1;

            // Loop through each surrounding tile to increment warning count
            int startCol = previousCol;
            for (; previousRow <= nextRow; previousRow++)
            {
                previousCol = startCol;
                for (; previousCol <= nextCol; previousCol++)
                {
                    tileStates[previousRow, previousCol].Warning++;
                }
            }
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Flag/unflag a given tile
        /// </summary>
        /// <param name="row">Row location of tile to flag/unflag</param>
        /// <param name="column">Column location of tile to flag/unflag</param>
        public void FlagTile(int row, int column)
        {
            TileState tileState = tileStates[row, column];

            // Do nothing if already flipped
            if (tileState.Flip && !tileState.Flag)
                return;
            // If tile already flagged, unflag it
            if (tileState.Flag)
            {
                tileState.Flag = false;
                tilesUntouched++;
                flagsLeft++;
            }
            else
            {
                if (flagsLeft == 0)
                {
                    MessageBox.Show("There are no flags left! " +
                        "To place a flag here, you must Remove another flag first.",
                        "No Flags Available", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                tileState.Flag = true;
                tilesUntouched--;
                flagsLeft--;
            }
            RefreshBoard();
        }

        /// <summary>
        /// Flip a given tile
        /// </summary>
        /// <param name="row">Row location of tile to flip</param>
        /// <param name="column">Column  location of tile to flip</param>
        public void FlipTile(int row, int column)
        {
            TileState tileState = tileStates[row, column];

            // If flipping a flagged tile, first remove the flag
            if (tileState.Flag)
            {
                tileState.Flag = false;
                tilesUntouched++;
                flagsLeft++;
            }
            // If we hit a mine, raise mineHit flag
            if (tileState.Mine)
                mineHit = true;

            PropagateFlips(row, column);
            RefreshBoard();
        }

        /// <summary>
        /// Flip a tile and all adjacent tiles that have no warnings
        /// </summary>
        /// <param name="row">Row location of the flipping tile</param>
        /// <param name="column">Column location of the flipping tile</param>
        void PropagateFlips(int row, int column)
        {
            // Don't go out of bounds
            if (row < 0)
                return;
            if (row >= rows)
                return;
            if (column < 0)
                return;
            if (column >= columns)
                return;

            // Can't reflip a tile
            if (tileStates[row, column].Flip)
                return;

            // Flip it
            tileStates[row, column].Flip = true;
            tilesUntouched--;

            // Stop propogation when we hit a tile with warnings
            if (tileStates[row, column].Warning != 0)
                return;

            // Recursive call for adjacent rows
            for (int adjacentRow = row - 1; adjacentRow <= row + 1; adjacentRow++)
            {
                for (int adjacentColumn = column - 1; adjacentColumn <= column + 1; adjacentColumn++)
                {
                        PropagateFlips(adjacentRow, adjacentColumn);
                }
            }
        }

        /// <summary>
        /// Repaint all tiles and check if game over
        /// </summary>
        void RefreshBoard()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Tile tile = tiles[row, col];
                    TileState tileState = tileStates[row, col];

                    if (tileState.Flag)  // Flagging tiles
                        tile.SetStatus(TileStatus.Flagged);
                    else if (tileState.Flip) // Flipping tiles
                    {
                        if (tileState.Mine && mineHit) 
                            tile.SetStatus(TileStatus.Mine);
                        else
                        {
                            if (tileState.Warning != 0)
                                tile.SetStatus(TileStatus.Warning, tileState.Warning);
                            else
                                tile.SetStatus(TileStatus.Flipped);
                        }
                    }
                    else // Unflagging tiles and revealing mines
                    {
                        if (tileState.Mine && mineHit)
                            // Reveal all mines if a mine was hit
                            tile.SetStatus(TileStatus.Mine);
                        else
                            // Unflagging tiles
                            tile.SetStatus(TileStatus.Unflippped);
                    }
                }
            }

            // Check if we are done
            // Alternate ending: stop when all unmined tiles are flipped, i.e. don't require complete flagging
            //(mineHit || tilesUntouched <= totalMines)
            if (mineHit || tilesUntouched == 0)
            {
                if (mineHit)
                    MessageBox.Show("Oops! You stepped on a mine.", "Game Over",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("You won! The minefield is clear.", "Success");
                LayMines();
            }
        }

        #endregion
    }
}


using System.Windows.Forms;

namespace Minesweeper
{
    public partial class GameBoard : Form
    {
        /// <summary>
        /// Initialize the game board
        /// </summary>
        public GameBoard()
        {

            // TO DO: Build options UI to allow user to set rows, columns, mines.
            // Display number of flags remaining.
            // Use a Singleton or static class to store settings, capture start/end times, 
            // serialize user's best times to a local file.
            // Allow form to auto-expand to fit larger games.
            // Set reasonable lower/upper limits to game size. (i.e., 100x100).

            int rows = 10;
            int columns = 10;
            int numberOfMines = 10;

            Game gameEngine = new Game(rows, columns, numberOfMines);

            SuspendLayout();

            // Add the tiles to the form
            for (int row = 0; row < gameEngine.Rows; row++)
            {
                for (int column = 0; column < gameEngine.Columns; column++)
                {
                    Tile tile = new Tile(column, row, gameEngine);
                    Controls.Add(tile);
                }
            }

            ResumeLayout(false);

            InitializeComponent();
            MineCountLabel.Text = "There are " + numberOfMines.ToString() + " mines scattered on the board.";
            Icon = Properties.Resources.Mine;

            gameEngine.LayMines();
        }

    }
}

using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    // Enum of all possible statuses of tiles
    public enum TileStatus
    {
        Unflippped,
        Flipped,
        Mine,
        Warning,
        Flagged
    }

    public partial class Tile : Button
    {
        #region Tile Layout

        // Layout characteristics of the tile
        static readonly Size tileSize = new Size(20, 20);
        static readonly Point startPoint = new Point(50, 160); 
        int row;
        int column;
        Game gameEngine;

        public int Row
        {
            get
            {
                return row;
            }
        }

        public int Column
        {
            get
            {
                return column;
            }
        }

        #endregion

        /// <summary>
        /// Create a tile
        /// </summary>
        /// <param name="row">Zero-based row of the tile</param>
        /// <param name="column">Zero-based column of the tile</param>
        /// <param name="engine">Instance of the game engine</param>
        public Tile(int row, int column, Game engine)
        {
            this.row = row;
            this.column = column;
            gameEngine = engine;
            engine.AddTile(this);
            Size = tileSize;
            Location = new Point(startPoint.X + (tileSize.Width + 1) * column, startPoint.Y + (tileSize.Height + 1) * row);
        }

        /// <summary>
        /// Handles the MouseUp event for the tile
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
                gameEngine.FlipTile(row, column);
            else
                gameEngine.FlagTile(row, column);
        }

        /// <summary>
        /// Display the status of the tile
        /// </summary>
        /// <param name="status">The status to set</param>
        /// <param name="warning">Number of warnings to show</param>
        public void SetStatus(TileStatus status, int warning = 0)
        {
            Image = null;
            switch (status)
            {
                case TileStatus.Unflippped:
                    BackColor = Color.LightGray;
                    Text = string.Empty;
                    break;
                case TileStatus.Flipped:
                    BackColor = Color.White;
                    break;
                case TileStatus.Flagged:
                    Image = Properties.Resources.flag;
                    break;
                case TileStatus.Warning:
                    Text = warning.ToString();
                    break;
                case TileStatus.Mine:
                    Image = Properties.Resources.boom;
                    break;
            }
        }

    }
}

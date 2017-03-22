
namespace Minesweeper
{
    public class TileState
    {

        // The actual state of the tile
        bool flip;
        bool flag;
        bool mine;
        int warningCount;

        public bool Flip
        {
            get { return flip; }
            set { flip = value; }
        }

        public bool Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public bool Mine
        {
            get { return mine; }
            set { mine = value; }
        }

        public int Warning
        {
            get { return warningCount; }
            set { warningCount = value; }
        }
    }
}

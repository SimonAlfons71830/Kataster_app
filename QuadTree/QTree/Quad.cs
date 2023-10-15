using System.ComponentModel;
using QuadTree.Structures;

namespace QuadTree.QTree
{
    /// <summary>
    /// also called Node (Vrchol)
    /// </summary>
    internal class Quad
    {
        /*
        struct that represents a rectangle
        double x, y; //start of the recatngle (left bottom as xy axis)
        double width; //width from x to right border
        double height; //height from y to upper border 
        */

        // in every node/quad there is
        // List of pointers to each child (NW, NE, SW, SE)
        // coordinates of area that node represents
        // list to save data 

        private MyPoint _key; //the starting points of the quad
        public Boundaries _boundaries; //X, Y, Heigth, Width

        //list of the points that belongs to the Quad
        public List<ISpatialObject> _objects = new List<ISpatialObject>();

        //public List<Quad> children = new List<Quad>();

        private Quad? _northWest { get; set; }
        private Quad? _northEast { get; set; }
        private Quad? _southEast { get; set; }
        private Quad? _southWest { get; set; }

        public Quad(Boundaries boundaries) {
            _key = new MyPoint(boundaries.X, boundaries.Y, "Starting MyPoint");
            _boundaries = boundaries;
        }

        public Quad? getNW() { return _northWest; }
        public Quad? getNE() { return _northEast; }
        public Quad? getSE() { return _southEast; }
        public Quad? getSW() { return _southWest; }

        /// <summary>
        /// Splits the current Quad into four child Quads, 
        /// dividing its boundaries into equal-sized Rectangles.
        /// </summary>
        /// <remarks>
        /// This method divides the current Quad into four child Quads, each covering a distinct quadrant
        /// within the boundaries of the parent Quad. The Quad is split into the following child Quads:
        /// - North-West (Top-left) Quadrant
        /// - North-East (Top-right) Quadrant
        /// - South-East (Bottom-right) Quadrant
        /// - South-West (Bottom-left) Quadrant
        /// </remarks>
        public void splitQuad() {

            double newHeight = _boundaries.Height / 2;
            double newWidth = _boundaries.Width / 2;

            _northWest = new Quad(new Boundaries(_boundaries.X, (float)(_boundaries.Y + newHeight), (float)newWidth, (float)newHeight));
            _northEast = new Quad(new Boundaries((float)(_boundaries.X + newWidth), (float)(_boundaries.Y + newHeight), (float)newWidth, (float)newHeight));
            _southEast = new Quad(new Boundaries((float)(_boundaries.X + newWidth), _boundaries.Y, (float)newWidth, (float)newHeight));
            _southWest = new Quad(new Boundaries(_boundaries.X , _boundaries.Y , (float)newWidth, (float)newHeight));

        }

        public bool Equals(Quad? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _boundaries.X == other._boundaries.X &&
          _boundaries.Y == other._boundaries.Y &&
          _boundaries.Width == other._boundaries.Width &&
          _boundaries.Height == other._boundaries.Height;
        }
    }
}

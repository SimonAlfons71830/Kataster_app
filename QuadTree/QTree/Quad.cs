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
        double _x0, _y0; //start of the rectangle (left bottom as xy axis)
        double width; //width from _x0 to right border
        double height; //height from _y0 to upper border 
        */

        // in every node/quad there is
        // List of pointers to each child (NW, NE, SW, SE)
        // coordinates of area that node represents
        // list to save data 

        //primary key
        private MyPoint _key; //the starting points of the quad
        //secondary key
        public Boundaries _boundaries; //X0, Y0, Heigth, Width

        //list of the points that belongs to the Quad
        public List<ISpatialObject> _objects = new List<ISpatialObject>();

        //public List<Quad> children = new List<Quad>();

        private Quad? _northWest { get; set; }
        private Quad? _northEast { get; set; }
        private Quad? _southEast { get; set; }
        private Quad? _southWest { get; set; }

        public Quad(Boundaries boundaries) {
            _key = new MyPoint(boundaries.X0, boundaries.Y0, "Starting MyPoint");
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

            _northWest = new Quad(new Boundaries(_boundaries.X0, (_boundaries.Y0 + _boundaries.Yk) /2, (_boundaries.Xk + _boundaries.X0) /2 , _boundaries.Yk));
            _northEast = new Quad(new Boundaries((_boundaries.Xk + _boundaries.X0) / 2, (_boundaries.Yk + _boundaries.Y0) / 2, _boundaries.Xk,_boundaries.Yk));
            _southEast = new Quad(new Boundaries((_boundaries.Xk + _boundaries.X0) / 2,_boundaries.Y0,_boundaries.Xk, (_boundaries.Yk + _boundaries.Y0) / 2));
            _southWest = new Quad(new Boundaries(_boundaries.X0,_boundaries.Y0, (_boundaries.Xk + _boundaries.X0) / 2, (_boundaries.Yk + _boundaries.Y0) / 2));

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

            double epsylon = 0.0000001;

            return (_boundaries.X0 + epsylon > other._boundaries.X0 && _boundaries.X0 - epsylon < other._boundaries.X0 ) &&
          (_boundaries.Y0 + epsylon > other._boundaries.Y0 && _boundaries.Y0 - epsylon < other._boundaries.Y0) &&
          (_boundaries.Xk + epsylon > other._boundaries.Xk && _boundaries.Xk - epsylon < other._boundaries.Xk) &&
          (_boundaries.Yk + epsylon > other._boundaries.Yk && _boundaries.Yk - epsylon < other._boundaries.Yk);
        }
    }
}

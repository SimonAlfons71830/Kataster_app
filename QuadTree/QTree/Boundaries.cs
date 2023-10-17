using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.QTree
{
    internal class Boundaries : IEquatable<Boundaries>
    {
        private double _x0; //pociatocne suradnice stvoruholniku
        private double _y0;

        private double _xk; //koncove suradnice stvoruholniku
        private double _yk;

        private double width; // Do not rename (binary serialization)
        private double height; // Do not rename (binary serialization)

        /// <summary>
        /// Initializes a new instance of the Boundaries class with the specified starting position
        /// and size.
        /// </summary>
        public Boundaries(double x0, double y0, double xk, double yk)
        {
            this._x0 = x0;
            this._y0 = y0;
            this._xk = xk;
            this._yk = yk;
        }

        /// <summary>
        /// Gets or sets the _x0-coordinate of the upper-left corner of the rectangular region defined by this
        /// </summary>
        public double X0
        {
            get => _x0;
            set => _x0 = value;
        }

        /// <summary>
        /// Gets or sets the _y0-coordinate of the upper-left corner of the rectangular region defined by this
        /// </summary>
        public double Y0
        {
            get => _y0;
            set => _y0 = value;
        }

        /// <summary>
        /// Gets or sets the _x0-coordinate of the upper-left corner of the rectangular region defined by this
        /// </summary>
        public double Xk
        {
            get => _xk;
            set => _xk = value;
        }

        /// <summary>
        /// Gets or sets the _y0-coordinate of the upper-left corner of the rectangular region defined by this
        /// </summary>
        public double Yk
        {
            get => _yk;
            set => _yk = value;
        }

        /// <summary>
        /// Gets or sets the width of the rectangular region defined by this <see cref='System.Drawing.RectangleF'/>.
        /// </summary>
        public double Width
        {
            get => width;
            set => width = value;
        }

        public bool Equals(Boundaries other)
        {
            return this == other;
        }

        public bool IsContainedInQuad(Quad quad) {
            //_x0 _y0 width length
            //4 points  x0y0, xky0, x0yk xkyk
            //4 tops of the rectangular area
            List<MyPoint> points = new List<MyPoint>();
            points.Add(new MyPoint(this._x0, this._y0, "x0y0"));
            points.Add(new MyPoint(this._x0, this._yk, "x0yk"));
            points.Add(new MyPoint(this._xk ,this._y0, "xky0"));
            points.Add(new MyPoint(this._xk, this._yk, "xkyk"));

            foreach (var t in points)
            {
                // Check if the vertex is within the quad's boundaries.
                bool withinXBounds = t._x > quad._boundaries.X0 && t._x < quad._boundaries.Xk;
                bool withinYBounds = t._y > quad._boundaries.Y0 && t._y < quad._boundaries.Xk;

                if (!withinXBounds || !withinYBounds)
                {
                    // If any vertex is outside the quad, the polygon is not fully contained.
                    return false;
                }
            }
            // If all vertices are within the quad, the polygon is contained.
            return true;
        }
    }
}

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
        private double x; // Do not rename (binary serialization)
        private double y; // Do not rename (binary serialization)
        private double width; // Do not rename (binary serialization)
        private double height; // Do not rename (binary serialization)

        /// <summary>
        /// Initializes a new instance of the Boundaries class with the specified starting position
        /// and size.
        /// </summary>
        public Boundaries(double x, double y, double width, double height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='System.Drawing.RectangleF'/>.
        /// </summary>
        public double X
        {
            get => x;
            set => x = value;
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the upper-left corner of the rectangular region defined by this
        /// <see cref='System.Drawing.RectangleF'/>.
        /// </summary>
        public double Y
        {
            get => y;
            set => y = value;
        }

        /// <summary>
        /// Gets or sets the width of the rectangular region defined by this <see cref='System.Drawing.RectangleF'/>.
        /// </summary>
        public double Width
        {
            get => width;
            set => width = value;
        }

        /// <summary>
        /// Gets or sets the height of the rectangular region defined by this <see cref='System.Drawing.RectangleF'/>.
        /// </summary>
        public double Height
        {
            get => height;
            set => height = value;
        }

        public bool Equals(Boundaries other)
        {
            return this == other;
        }

        public bool IsContainedInQuad(Quad quad) {
            //x y width length
            //4 points  x0y0, xky0, x0yk xkyk
            //4 tops of the rectangular area
            List<MyPoint> points = new List<MyPoint>();
            points.Add(new MyPoint(this.x, this.y, "x0y0"));
            points.Add(new MyPoint(this.x, this.y + this.height, "x0yk"));
            points.Add(new MyPoint(this.x + this.width, this.y, "xky0"));
            points.Add(new MyPoint(this.x + this.width, this.y + this.height, "xkyk"));


            foreach (var t in points)
            {
                // Check if the vertex is within the quad's boundaries.
                bool withinXBounds = t._x > quad._boundaries.X && t._x < quad._boundaries.X + quad._boundaries.Width;
                bool withinYBounds = t._y > quad._boundaries.Y && t._y < quad._boundaries.Y + quad._boundaries.Height;

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

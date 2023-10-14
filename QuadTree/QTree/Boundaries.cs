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
    }
}

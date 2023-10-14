using System;
using System.Drawing;

namespace QuadTree.QTree
{
    //trieda ktora reprezentuje bod v koreni QStromu
    //ma svoje suradnice x a y ktore reprezentuju presne umiestnenie v ramci korena stromu
    internal class MyPoint : ISpatialObject, IEquatable<MyPoint>
    {
        public double _x { get; }
        public double _y { get; }
        public String _name { get; }


        public MyPoint(double x, double y, String name) { 
            _x = x;
            _y = y;
            _name = name;
        }

        public double DistanceTo(MyPoint other)
        {
            dynamic x1 = _x;
            dynamic y1 = _y;
            dynamic x2 = other._x;
            dynamic y2 = other._y;

            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public Quad? FindQuad(Quad quad) {
            //find the center of quad -> from there the boundaries will be determined
            double centerX = (quad._boundaries.X + (quad._boundaries.X + quad._boundaries.Width)) / 2;
            double centerY = (quad._boundaries.Y + (quad._boundaries.Y + quad._boundaries.Height)) / 2;

            //SW || NW
            if (_x < centerX)
            {
                //SW
                if (_y < centerY)
                {
                    return quad.getSW();
                }
                //NW
                else if (_y > centerY)
                {
                    return quad.getNW();
                }

            }
            else //NE || SE
            {
                //SE
                if (_y < centerY)
                {
                    return quad.getSE();
                }
                //NE
                else if (_y > centerY)
                {
                    return quad.getNE();
                }
            }

            //if its not in any childQuad then its set on boundaries
            return null;

            //rewritten with ternaty operators

            /*Quad? result = point._x < centerX ? (point._y < centerY ? current.getSW() : current.getNW()) : (point._y < centerY ? current.getSE() : current.getNE());

            // If result is null, the point is outside all child quadrants and should be placed in the current quad
            return result ?? null;*/
        }

        public bool Equals(MyPoint? other)
        {
            return this._x == other._x && this._y == other._y;
        }

        public bool IsContainedInQuad(Quad quad)
        {
            // Check if the point is within the quad's boundaries.
            bool withinXBounds = _x > quad._boundaries.X &&_x < (quad._boundaries.X + quad._boundaries.Width);
            bool withinYBounds = _y > quad._boundaries.Y && _y < (quad._boundaries.Y + quad._boundaries.Height);

            return withinXBounds && withinYBounds;
        }
    }
}

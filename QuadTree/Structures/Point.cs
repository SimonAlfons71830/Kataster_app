﻿using System;
using System.Drawing;
using QuadTree.QTree;

namespace QuadTree.Structures
{
    //trieda ktora reprezentuje bod v koreni QStromu
    //ma svoje suradnice _x0 a _y0 ktore reprezentuju presne umiestnenie v ramci korena stromu
    internal class MyPoint : ISpatialObject, IEquatable<MyPoint>
    {
        public double _x { get; }
        public double _y { get; }
        public string _name { get; }


        public MyPoint(double x, double y, string name)
        {
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

            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public Quad? FindQuad(Quad quad)
        {
            //find the center of quad -> from there the boundaries will be determined
            double centerX = (quad._boundaries.X0 + quad._boundaries.Xk) / 2;
            double centerY = (quad._boundaries.Y0 + quad._boundaries.Yk) / 2;

            //SW || NW
            if (_x < centerX && _x > quad._boundaries.X0)
            {
                //SW
                if (_y < centerY && _y > quad._boundaries.Y0)
                {
                    return quad.getSW();
                }
                //NW
                else if (_y > centerY && _y < quad._boundaries.Yk)
                {
                    return quad.getNW();
                }

            }
            else if(_x > centerX && _x < quad._boundaries.Xk)//NE || SE
            {
                //SE
                if (_y < centerY && _y > quad._boundaries.Y0)
                {
                    return quad.getSE();
                }
                //NE
                else if (_y > centerY && _y < quad._boundaries.Yk)
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
            double epsylon = 0.000001;
            //TODO: osetrit porovnavanie double!
            return (_x + epsylon > other._x && _x - epsylon < other._x) 
                && (_y + epsylon > other._y && _y - epsylon < other._y);
        }

        /*public bool IsContainedInQuad(Quad quad)
        {
            // Check if the point is within the quad's boundaries.
            bool withinXBounds = _x >= quad._boundaries.X0 && _x <= quad._boundaries.Xk;
            bool withinYBounds = _y >= quad._boundaries.Y0 && _y <= quad._boundaries.Yk;

            return withinXBounds && withinYBounds;
        }*/

        public bool IsContainedInArea(Boundaries boundaries) {
            bool withinXBounds = _x >= boundaries.X0 && _x <= boundaries.Xk;
            bool withinYBounds = _y >= boundaries.Y0 && _y <= boundaries.Yk;

            return withinXBounds && withinYBounds;
        }
    }
}

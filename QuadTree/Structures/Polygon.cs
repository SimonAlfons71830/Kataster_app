using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using QuadTree.QTree;

namespace QuadTree.Structures
{
    public class Polygon: IEquatable<Polygon>, ISpatialObject
    {
        public (MyPoint startP, MyPoint endP) _borders;
        public int _registerNumber { get; set; }
        // Calculate the center (centroid) of the polygon based on its vertices.
        public double _x
        {
            get
            {
                double sumX = 0;
                sumX += _borders.startP._x;
                sumX += _borders.endP._x;
                return sumX / 2;
            }
        }

        public double _y
        {
            get
            {
                double sumY = 0;
                sumY += _borders.startP._y;
                sumY += _borders.endP._y;
                return sumY / 2;
            }
        }


        public Polygon(int registerNumber, (MyPoint startP, MyPoint endP) borders)
        {
            _registerNumber = registerNumber;
            _borders = borders;
        }

        public bool Equals(Polygon? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            //same reference means same objects
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            //comparing pairs of tops
            if (!(_borders.startP._x == other._borders.startP._x && _borders.startP._y == other._borders.startP._y && 
                _borders.endP._x == other._borders.endP._x && _borders.endP._y == other._borders.endP._y))
            {
                return false;
            }


            if (other._registerNumber != this._registerNumber)
            {
                return false;
            }

            return true; 
        }


        /// <summary>
        /// returns a reference to a quad that can fit whole polygon
        /// if the return value is null, the polygon cant fit into any quad
        /// </summary>
        /// <param name="quad"></param>
        /// <returns></returns>
        public Quad? FindQuad(Quad quad)
        {
            //find the center of quad -> from there the boundaries will be determined
            double centerX = (quad._boundaries.X0 + quad._boundaries.Xk) / 2;
            double centerY = (quad._boundaries.Y0 + quad._boundaries.Yk) / 2;


            double PcenterX = (_borders.startP._x + _borders.endP._x) / 2; 
            double PcenterY = (_borders.startP._y + _borders.endP._y) / 2; 

            if (PcenterX < centerX)
            {
                if (PcenterY < centerY)
                {
                    if (_borders.startP.IsContainedInArea(quad.getSW()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getSW()._boundaries, false))
                    {
                        return quad.getSW();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (_borders.startP.IsContainedInArea(quad.getNW()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getNW()._boundaries, false))
                    {
                        return quad.getNW();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                if (PcenterY < centerY)
                {
                    if (_borders.startP.IsContainedInArea(quad.getSE()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getSE()._boundaries, false))
                    {
                        return quad.getSE();
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    if (_borders.startP.IsContainedInArea(quad.getNE()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getNE()._boundaries, false))
                    {
                        return quad.getNE();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool IsContainedInArea(Boundaries boundaries, bool interfere) {

            if (interfere)
            {
                var pointIntersection = false;
                bool sideIntersection = false;

                bool startPointWithinBounds = _borders.startP._x >= boundaries.X0 && _borders.startP._x <= boundaries.Xk &&
                                                _borders.startP._y >= boundaries.Y0 && _borders.startP._y <= boundaries.Yk;

                bool endPointWithinBounds = _borders.endP._x >= boundaries.X0 && _borders.endP._x <= boundaries.Xk &&
                                           _borders.endP._y >= boundaries.Y0 && _borders.endP._y <= boundaries.Yk;

                pointIntersection = startPointWithinBounds || endPointWithinBounds;




                //checking the side intersection only applies to rectangles 
                var top0 = _borders.startP;
                //var top0 = _tops.ElementAt(0);
                var topK = _borders.endP;
                //var topK = _tops.ElementAt(1);
                sideIntersection = (top0._x < boundaries.Xk && topK._x > boundaries.X0 && top0._y < boundaries.Yk && topK._y > boundaries.Y0) ||
                    (boundaries.X0 < topK._x && boundaries.Xk > top0._x && boundaries.Y0 < topK._y && boundaries.Yk > top0._y);


                return pointIntersection || sideIntersection;

            }
            else
            {
                bool startPointWithinBounds = _borders.startP._x >= boundaries.X0 && _borders.startP._x <= boundaries.Xk &&
                                                _borders.startP._y >= boundaries.Y0 && _borders.startP._y <= boundaries.Yk;

                bool endPointWithinBounds = _borders.endP._x >= boundaries.X0 && _borders.endP._x <= boundaries.Xk &&
                                           _borders.endP._y >= boundaries.Y0 && _borders.endP._y <= boundaries.Yk;

                return startPointWithinBounds && endPointWithinBounds;

            }
        }

        public Quad FindQuadUpdate(Quad quad)
        {
            //find the center of quad -> from there the boundaries will be determined
            if (quad._northEast == null)
            {
                return null;
            }
            double centerX = quad._northEast._boundaries.X0;
            double centerY = quad._northEast._boundaries.Y0;


            double PcenterX = (_borders.startP._x + _borders.endP._x) / 2; 
            double PcenterY = (_borders.startP._y + _borders.endP._y) / 2; 


            if (PcenterX < centerX)
            {
                if (PcenterY < centerY)
                {
                    if (_borders.startP.IsContainedInArea(quad.getSW()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getSW()._boundaries, false))
                    {
                        return quad.getSW();
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    if (_borders.startP.IsContainedInArea(quad.getNW()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getNW()._boundaries, false))
                    {
                        return quad.getNW();
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            else
            {
                if (PcenterY < centerY)
                {
                    if (_borders.startP.IsContainedInArea(quad.getSE()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getSE()._boundaries, false))
                    {
                        return quad.getSE();
                    }
                    else
                    {
                        return null;
                    }


                }
                else
                {
                    if (_borders.startP.IsContainedInArea(quad.getNE()._boundaries, false) &&
                        _borders.endP.IsContainedInArea(quad.getNE()._boundaries, false))
                    {
                        return quad.getNE();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}

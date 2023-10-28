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
        private List<MyPoint> _tops;
        public int _registerNumber { get; set; }
        // Calculate the center (centroid) of the polygon based on its vertices.
        public double _x
        {
            get
            {
                if (_tops.Count == 0)
                    return 0;

                double sumX = 0;
                foreach (var top in _tops)
                {
                    sumX += top._x;
                }

                return sumX / _tops.Count;
            }
        }

        public double _y
        {
            get
            {
                if (_tops.Count == 0)
                    return 0;

                double sumY = 0;
                foreach (var top in _tops)
                {
                    sumY += top._y;
                }

                return sumY / _tops.Count;
            }
        }


        public Polygon(int id)
        {
            _registerNumber = id;
            _tops = new List<MyPoint>();
        }

        public void AddTop(MyPoint top)
        {
            _tops.Add(top);
        }

        public List<MyPoint> GetTops()
        {
            return _tops;
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

            //differetn number of tops
            if (_tops.Count != other._tops.Count)
            {
                return false;
            }

            //comparing pairs of tops
            for (int i = 0; i < _tops.Count; i++)
            {
                if (!_tops[i].Equals(other._tops[i]))
                {
                    return false;
                }
            }

            if (other._registerNumber != this._registerNumber)
            {
                return false;
            }

            return true; // All tops are equal, so the Polygons are equal.

            //return _tops.SequenceEqual(other._tops); //each point from seqence is equal
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

            double PcenterX = _tops.Average(point => point._x);
            double PcenterY = _tops.Average(point => point._y);

            if (PcenterX < centerX)
            {
                if (PcenterY < centerY)
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getSW()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }


                    //also all points of the polygon needs to be in this quad
                    return quad.getSW();
                }
                else
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getNW()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return quad.getNW();


                }
            }
            else
            {
                if (PcenterY < centerY)
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getSE()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return quad.getSE();

                }
                else
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getNE()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return quad.getNE();

                }
            }
        }

        public bool IsContainedInArea(Boundaries boundaries, bool interfere) {

            if (interfere)
            {
                var pointIntersection = false;
                bool sideIntersection = false;
                //at least one of the tops
                foreach (var t in _tops)
                {
                    // Check if the vertex is within the quad's boundaries.
                    bool withinXBounds = t._x >= boundaries.X0 && t._x <= boundaries.Xk;
                    bool withinYBounds = t._y >= boundaries.Y0 && t._y <= boundaries.Yk;

                    if (withinXBounds && withinYBounds)
                    {
                        // If any vertex is outside the quad, the polygon is not fully contained.
                        pointIntersection = true;
                    }
                }


                //checking the side intersection only applies to rectangles 
                var top0 = _tops.ElementAt(0);
                var topK = _tops.ElementAt(1);
                sideIntersection = (top0._x < boundaries.Xk && topK._x > boundaries.X0 && top0._y < boundaries.Yk && topK._y > boundaries.Y0) ||
                    (boundaries.X0 < topK._x && boundaries.Xk > top0._x && boundaries.Y0 < topK._y && boundaries.Yk > top0._y);


                return pointIntersection || sideIntersection;

            }
            else
            {

                // Assuming you have a list of vertices in your polygon.
                foreach (var t in _tops)
                {
                    // Check if the vertex is within the quad's boundaries.
                    bool withinXBounds = t._x >= boundaries.X0 && t._x <= boundaries.Xk;
                    bool withinYBounds = t._y >= boundaries.Y0 && t._y <= boundaries.Yk;

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

        public Quad FindQuadUpdate(Quad quad)
        {
            //find the center of quad -> from there the boundaries will be determined
            if (quad._northEast == null)
            {
                return null;
            }
            double centerX = quad._northEast._boundaries.X0;
            double centerY = quad._northEast._boundaries.Y0;


            double PcenterX = _tops.Average(point => point._x);
            double PcenterY = _tops.Average(point => point._y);

            //double PcenterX =  _tops.Average(point => point._x);
            //double PcenterY = _tops.Average(point => point._y);

            if (PcenterX < centerX)
            {
                if (PcenterY < centerY)
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getSW()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    //also all points of the polygon needs to be in this quad
                    return quad.getSW();

                }
                else
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getNW()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return quad.getNW();
                    
                }
            }
            else
            {
                if (PcenterY < centerY)
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getSE()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return quad.getSE();


                }
                else
                {
                    foreach (var point in _tops)
                    {
                        if (point.IsContainedInArea(quad.getNE()._boundaries, false))
                        {
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return quad.getNE();


                }
            }
        }
    }
}

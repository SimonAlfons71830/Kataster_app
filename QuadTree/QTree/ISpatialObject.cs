using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.QTree
{
    internal interface ISpatialObject
    {
        // Properties that are common to both points and polygons.
        double _x { get; }
        double _y { get; }

        public bool IsContainedInQuad(Quad quad);
        Quad FindQuad(Quad quad); // You can customize the method as per your needs.
    }
}

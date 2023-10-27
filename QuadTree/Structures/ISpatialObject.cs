using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuadTree.QTree;

namespace QuadTree.Structures
{
    public interface ISpatialObject
    {
        // Properties that are common to both points and polygons.
        double _x { get; }
        double _y { get; }
        int _id { get; }

        Quad FindQuad(Quad quad);
        public bool IsContainedInArea(Boundaries boundaries);
        Quad FindQuadUpdate(Quad quad);
    }
}

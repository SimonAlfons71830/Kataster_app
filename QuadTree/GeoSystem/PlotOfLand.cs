using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.GeoSystem
{
    public class PlotOfLand : Polygon
    {
        public string _description;
        public (Coordinates startPos, Coordinates endPos) _coordinates;
        public List<Property> _properties;

        public PlotOfLand(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates, List<Property> properties) 
            : base(registerNumber, coordinates)
        {
            RegisterNumber = registerNumber;
            Description = description;
            Coordinates = coordinates;
            Properties = properties;
        }

        public int RegisterNumber { get => _registerNumber; set => _registerNumber = value; }
        public string Description { get => _description; set => _description = value; }
        internal (Coordinates startPos, Coordinates endPos) Coordinates { get => _coordinates; set => _coordinates = value; }
        internal List<Property> Properties { get => _properties; set => _properties = value; }
    }
}

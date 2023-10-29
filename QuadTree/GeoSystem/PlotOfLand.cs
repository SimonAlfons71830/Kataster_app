using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.GeoSystem
{
    internal class PlotOfLand : Polygon
    {
        private string _description;
        private (Coordinates startPos, Coordinates endPos) _coordinates;
        private List<Property> _properties;

        public PlotOfLand(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates, List<Property> properties) 
            : base(registerNumber)
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

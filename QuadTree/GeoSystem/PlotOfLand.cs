using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.GeoSystem
{
    internal class PlotOfLand
    {
        private int _registerNumber;
        private string _description;
        private Tuple<Coordinates, Coordinates> _coordinates;
        private List<Property> _properties;

        public PlotOfLand(int registerNumber, string description, Tuple<Coordinates,Coordinates> coordinates, List<Property> properties) 
        {
            RegisterNumber = registerNumber;
            Description = description;
            Coordinates = coordinates;
            Properties = properties;
        }

        public int RegisterNumber { get => _registerNumber; set => _registerNumber = value; }
        public string Description { get => _description; set => _description = value; }
        internal Tuple<Coordinates, Coordinates> Coordinates { get => _coordinates; set => _coordinates = value; }
        internal List<Property> Properties { get => _properties; set => _properties = value; }
    }
}

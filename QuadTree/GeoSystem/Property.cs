using QuadTree.QTree;
using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.GeoSystem
{
    public class Property : Polygon
    {
        //public int _registerNumber { get; set; }
        public (Coordinates x, Coordinates y) suradnice { get; set; }
        private string _description;
        private List<PlotOfLand> _lands;

        public Property(int registerNumber, string description, (Coordinates x, Coordinates y) coordinates, List<PlotOfLand> lands) 
            : base(registerNumber, coordinates)
        {
            RegisterNumber = registerNumber;
            Description = description;
            Coordinates = coordinates;
            Lands = lands;
        }

        public int RegisterNumber { get => _registerNumber; set => _registerNumber = value; }
        public string Description { get => _description; set => _description = value; }
        internal (Coordinates x, Coordinates y) Coordinates { get => suradnice; set => suradnice = value; }
        internal List<PlotOfLand> Lands { get => _lands; set => _lands = value; }
    }
}

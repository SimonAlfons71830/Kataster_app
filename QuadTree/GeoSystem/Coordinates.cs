using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuadTree.GeoSystem
{
    internal class Coordinates
    {
        //_longitude >=0  -> E  
        //_longitude < 0  -> W
        //_latitude   >=0  -> N
        //_latitude   < 0  -> S

        /// <summary>
        /// x
        /// </summary>
        private double _longitude;
        /// <summary>
        /// y
        /// </summary>
        private double _latitude;
        private char _longHem;
        private char _latHem;

        public Coordinates(double longitude, double latitude)
        {
            Longitude = longitude; //x
            Latitude = latitude; //y
            LongHem = (Longitude >= 0) ? 'E' : 'W';
            LatHem = (Latitude >= 0) ? 'N' : 'S';
        }

        public double Longitude { get => _longitude; set => _longitude = value; }
        public double Latitude { get => _latitude; set => _latitude = value; }
        public char LongHem { get => _longHem; set => _longHem = value; }
        public char LatHem { get => _latHem; set => _latHem = value; }
    }
}

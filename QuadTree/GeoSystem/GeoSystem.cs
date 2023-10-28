using QuadTree.QTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.GeoSystem
{
    internal class GeoSystem
    {
        public QuadTreeStruct _area;

        public GeoSystem(QuadTreeStruct area) 
        {
            //ked sa vytvori system, vytvori sa area zo zadanych suradnic od pouzivatela
            //naseeduju sa tu nejake parcely a nehnutelnosti do nich
            _area = area;
            //seed
        }

        /*public bool AddProperty(Property property) 
        { 
            
        }*/

        /*public bool AddPlotOfLand(int registerNumber, string desc, Tuple<Coordinates, Coordinates> coordinates) 
        { 
            var plotOfLand = new PlotOfLand(registerNumber, desc, coordinates, new List<Property>());

            //Polygon?
            _area.InsertUpdate(plotOfLand);

        }*/
    }
}

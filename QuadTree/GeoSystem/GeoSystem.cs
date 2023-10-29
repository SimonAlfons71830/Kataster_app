using QuadTree.QTree;
using QuadTree.Structures;
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

        public void AddProperty(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates)
        {
            this._area.InsertUpdate(new Property(registerNumber, description, coordinates, new List<PlotOfLand>()));
        }

        public List<Property> FindPropertiesGPS(Coordinates position) 
        {
            var listOfProp = new List<Property>();



            return listOfProp;
        }

        public List<Property> FindPropertiesInterval((Coordinates startPos, Coordinates endPos) coordinates, bool interfere) 
        {
            var foundObjects = new List<ISpatialObject>();

            foundObjects = this._area.IntervalSearch(new Boundaries(coordinates.startPos.Longitude, coordinates.startPos.Latitude, coordinates.endPos.Longitude, coordinates.endPos.Latitude),interfere);
            // Filter and cast the found objects to Property type
            var listOfProperties = foundObjects.OfType<Property>().ToList();

            return listOfProperties;
        }

        /*public bool AddPlotOfLand(int registerNumber, string desc, Tuple<Coordinates, Coordinates> coordinates) 
        { 
            var plotOfLand = new PlotOfLand(registerNumber, desc, coordinates, new List<Property>());

            //Polygon?
            _area.InsertUpdate(plotOfLand);

        }*/
    }
}

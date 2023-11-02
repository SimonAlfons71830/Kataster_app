using QuadTree.QTree;
using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.GeoSystem
{
    public class GeoApp
    {
        public MyQuadTree _area;

        public GeoApp(MyQuadTree area) 
        {
            //ked sa vytvori system, vytvori sa area zo zadanych suradnic od pouzivatela
            //naseeduju sa tu nejake parcely a nehnutelnosti do nich
            _area = area;
            //seed
        }

        public void AddProperty(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates)
        {
            this._area.Insert(new Property(registerNumber, description, coordinates, new List<PlotOfLand>()));
        }

        public void AddPlot(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates) 
        {
            this._area.Insert(new PlotOfLand(registerNumber, description, coordinates, new List<Property>()));
        }

        public List<Polygon> FindInterval((Coordinates startPos, Coordinates endPos) coordinates) 
        {
            var listOfObj = _area.IntervalSearch(new Boundaries(coordinates.startPos.Longitude,coordinates.startPos.Latitude,
                coordinates.endPos.Longitude, coordinates.endPos.Latitude) , true);

            var returnList = listOfObj.OfType<Polygon>().ToList();

            return returnList;
        }

        /*public List<Property> FindPropertiesInterval((Coordinates startPos, Coordinates endPos) coordinates, bool interfere) 
        {
            var foundObjects = new List<ISpatialObject>();

            foundObjects = this._area.IntervalSearch(new Boundaries(coordinates.startPos.Longitude, coordinates.startPos.Latitude, coordinates.endPos.Longitude, coordinates.endPos.Latitude),interfere);
            // Filter and cast the found objects to Property type
            var listOfProperties = foundObjects.OfType<Property>().ToList();

            return listOfProperties;
        }

        public List<PlotOfLand> FindPlotsInterval((Coordinates startPos, Coordinates endPos) coordinates, bool interfere)
        {
            var foundObjects = new List<ISpatialObject>();

            foundObjects = this._area.IntervalSearch(new Boundaries(coordinates.startPos.Longitude, coordinates.startPos.Latitude, coordinates.endPos.Longitude, coordinates.endPos.Latitude), interfere);
            // Filter and cast the found objects to Property type
            var listOfPlots = foundObjects.OfType<PlotOfLand>().ToList();

            return listOfPlots;
        }*/

        public List<Polygon> FindOBJInterval((Coordinates startPos, Coordinates endPos) coordinates, bool interfere, bool properties)
        {
            var foundObjects = new List<ISpatialObject>();

            foundObjects = this._area.IntervalSearch(new Boundaries(coordinates.startPos.Longitude, coordinates.startPos.Latitude, coordinates.endPos.Longitude, coordinates.endPos.Latitude), interfere);

            if (properties)
            {
                var listOfProperties = foundObjects.OfType<Property>().Cast<Polygon>().ToList();
                return listOfProperties;
            }
            else
            {
                var listOfPlots = foundObjects.OfType<PlotOfLand>().Cast<Polygon>().ToList();
                return listOfPlots;
            }
        }

    }
}

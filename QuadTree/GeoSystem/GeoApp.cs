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
        private Random _random = new Random();

        public GeoApp(MyQuadTree area) 
        {
            //ked sa vytvori system, vytvori sa area zo zadanych suradnic od pouzivatela
            //naseeduju sa tu nejake parcely a nehnutelnosti do nich
            _area = area;
            //seed
        }

        public void AddProperty(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates)
        {
            var list = this.FindInterval((coordinates.startPos, coordinates.endPos));
            var listForProp = list.OfType<PlotOfLand>().ToList();

            var prop = new Property(registerNumber, description, coordinates, listForProp);

            foreach(var item in listForProp) 
            {
                ((PlotOfLand)item)._properties.Add(prop);
            }

            this._area.Insert(prop);
        }

        public void AddPlot(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates) 
        {
            var list = this.FindInterval((coordinates.startPos, coordinates.endPos));
            var listForProp = list.OfType<Property>().ToList();

            var plot = new PlotOfLand(registerNumber, description, coordinates, listForProp);

            foreach (var item in listForProp)
            {
                ((Property)item)._lands.Add(plot);
            }

            this._area.Insert(plot);
        }

        public bool RemoveObj(Polygon obj) 
        {
            return _area.RemoveObject(obj);

            //aj z listu ich referencii odstranich samu seba

        }

        public List<Polygon> FindInterval((Coordinates startPos, Coordinates endPos) coordinates) 
        {
            var listOfObj = _area.IntervalSearch(new Boundaries(coordinates.startPos.Longitude,coordinates.startPos.Latitude,
                coordinates.endPos.Longitude, coordinates.endPos.Latitude) , true);

            var returnList = listOfObj.OfType<Polygon>().ToList();

            return returnList;
        }

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

        public void seedApp(int widthOfTree, int lengthOfTree, int numberOfProp, int numberOfPlot, int max_quad_cap, int max_depth) 
        {
            _area.ResetTree(_area._root);

            _area._dimension.X0 = 0;
            _area._dimension.Y0 = 0;
            _area._dimension.Xk = _area._dimension.X0 + widthOfTree;
            _area._dimension.Yk = _area._dimension.Y0 + lengthOfTree;
            _area.MAX_QUAD_CAPACITY = max_quad_cap;
            _area._maxDepth = max_depth;

            for (int i = 0; i < numberOfProp; i++)
            {
                //nechat ako double ???
                double startPosX = _random.Next((int)_area._dimension.X0, (int)_area._dimension.Xk);
                double startPosY = _random.Next((int)_area._dimension.Y0, (int)_area._dimension.Yk);

                this.AddProperty(i, "", (new Coordinates(startPosX, startPosY, 0), new Coordinates(startPosX, startPosY, 0)));
            }

            for (int i = 0; i < numberOfPlot; i++)
            {
                var rozmer = 0;
                if ((this._area._dimension.Xk - this._area._dimension.X0) > (this._area._dimension.Yk - this._area._dimension.Y0))
                {
                    rozmer = _random.Next(1, (int)(this._area._dimension.Yk - this._area._dimension.Y0) / 5);
                }
                else
                {
                    rozmer = _random.Next(1, (int)(this._area._dimension.Xk - this._area._dimension.X0) / 5);
                }

                var startPosGen = new MyPoint(_random.Next((int)_area._dimension.X0, (int)_area._dimension.Xk - rozmer), _random.Next((int)_area._dimension.Y0, (int)_area._dimension.Yk - rozmer), _random.Next(1000000));
                var endPosGen = new MyPoint(startPosGen._x + rozmer, startPosGen._y + rozmer, _random.Next(100000));

               // var _object = new Polygon(_random.Next(10000), (new MyPoint(startPosGen._x, startPosGen._y, startPosGen._registerNumber), new MyPoint(endPosGen._x, endPosGen._y, endPosGen._registerNumber)));

                this.AddPlot(numberOfProp + i, "", (new Coordinates(startPosGen._x, startPosGen._y, 0), new Coordinates(endPosGen._x, endPosGen._y, 0)));

            }

        }

        public Polygon PickToEdit(Polygon obj) 
        {
            return (Polygon)this._area.ShowObject(obj);
        }

    }
}

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

                this.AddProperty(i, "This is Property description.", (new Coordinates(startPosX, startPosY, 0), new Coordinates(startPosX, startPosY, 0)));
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

                this.AddPlot(numberOfProp + i, "This is Plot description.", (new Coordinates(startPosGen._x, startPosGen._y, 0), new Coordinates(endPosGen._x, endPosGen._y, 0)));

            }

        }

        public Polygon PickToEdit(Polygon obj) 
        {
            return (Polygon)this._area.ShowObject(obj);
        }


        public void WriteToFiles()
        {
            StreamWriter writerProp = null;
            StreamWriter writerPlots = null;
            try
            {
                writerProp = new StreamWriter("Properties.txt");
                writerPlots = new StreamWriter("Plots.txt");


                var AllObj = this.FindInterval((new Coordinates(this._area._dimension.X0, this._area._dimension.Y0, 0), new Coordinates(this._area._dimension.Xk, this._area._dimension.Yk, 0)));
                
                foreach (var obj in AllObj)
                {
                    if (obj is Property)
                    {
                        writerProp.WriteLine( obj.GetType().Name + ";" +   ((Property)obj)._registerNumber + ";" +
                            ((Property)obj).Coordinates.x.Longitude + ((Property)obj).Coordinates.x.LongHem + ";" + 
                            ((Property)obj).Coordinates.x.Latitude + ((Property)obj).Coordinates.x.LatHem + ";" +
                            ((Property)obj)._description);
                    }
                    else
                    {
                        writerPlots.WriteLine(obj.GetType().Name + ";" + ((PlotOfLand)obj)._registerNumber + ";" + ((PlotOfLand)obj).Coordinates.startPos.Longitude + ((PlotOfLand)obj).Coordinates.startPos.LongHem + ";" + ((PlotOfLand)obj).Coordinates.startPos.Latitude + ((PlotOfLand)obj).Coordinates.startPos.LatHem + ";" +
                            ((PlotOfLand)obj).Coordinates.endPos.Longitude + ((PlotOfLand)obj).Coordinates.endPos.LongHem + ";" + ((PlotOfLand)obj).Coordinates.endPos.Latitude + ((PlotOfLand)obj).Coordinates.endPos.LatHem + ";" + ((PlotOfLand)obj)._description);
                    }
                }

                writerProp.Close();
                writerPlots.Close();
                
                //ask for all quads
                //Queue<Quad> AllQuads = this._area.GetQuadsAtDepth(0);
                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                writerProp.Close();
            }
        }

        public void ReadProperties(String pathProp)
        {

            StreamReader reader = new StreamReader(pathProp);
            StringBuilder builder = new StringBuilder();
            try
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("Property"))
                    {
                        // Split the line by semicolon to extract data
                        string[] parts = line.Split(';');

                        if (parts.Length >= 5)
                        {
                            //Property;3;79E;491N;description
                            // Parse the relevant data from the line
                            int id = int.Parse(parts[1]);
                            //double coordX = double.Parse(parts[2]);

                            string rawCoordX = parts[2];
                            string numericPartX = new string(rawCoordX.Reverse().SkipWhile(char.IsLetter).Reverse().ToArray());
                            double coordX = double.Parse(numericPartX);

                            //double coordY = double.Parse(parts[3]);
                            string rawCoordY = parts[2];
                            string numericPartY = new string(rawCoordX.Reverse().SkipWhile(char.IsLetter).Reverse().ToArray());
                            double coordY = double.Parse(numericPartY);

                            string description = parts[4];

                            AddProperty(id, description, ((new Coordinates(coordX,coordY,0)),new Coordinates(coordX,coordY,0)));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        public void ReadPlots(String pathPlot)
        {

            StreamReader reader = new StreamReader(pathPlot);
            StringBuilder builder = new StringBuilder();
            try
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("PlotOfLand"))
                    {
                        // Split the line by semicolon to extract data
                        string[] parts = line.Split(';');

                        if (parts.Length >= 7)
                        {
                            //PlotOfLand;25;113E;261N;202E;350N;description
                            int id = int.Parse(parts[1]);

                            string rawCoordStartX = parts[2];
                            string numericPartStartX = new string(rawCoordStartX.Reverse().SkipWhile(char.IsLetter).Reverse().ToArray());
                            double coordStartX = double.Parse(numericPartStartX);

                            string rawCoordStartY = parts[3];
                            string numericPartStartY = new string(rawCoordStartY.Reverse().SkipWhile(char.IsLetter).Reverse().ToArray());
                            double coordStartY = double.Parse(numericPartStartY);

                            string rawCoordEndX = parts[4];
                            string numericPartEndX = new string(rawCoordEndX.Reverse().SkipWhile(char.IsLetter).Reverse().ToArray());
                            double coordEndX = double.Parse(numericPartEndX);

                            string rawCoordEndY = parts[5];
                            string numericPartEndY = new string(rawCoordEndY.Reverse().SkipWhile(char.IsLetter).Reverse().ToArray());
                            double coordEndY = double.Parse(numericPartEndY);

                            string description = parts[6];

                            AddPlot(id, description, ((new Coordinates(coordStartX, coordStartY, 0)), new Coordinates(coordEndX, coordEndY, 0)));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                reader.Close();
            }
        }
    }
}

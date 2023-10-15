using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.Xml;
using QuadTree.Structures;

namespace QuadTree.QTree
{
    internal class QuadTreeStruct
    {
        private readonly int _maxDepth;
        public int _objectsCount;
        private int _level;
        const int MAX_QUAD_CAPACITY = 1;
        //rectangleF - stores 4 data in float that represents size and location of the rectangle
        private readonly Boundaries _dimension;
        private Quad _root;

        /// <summary>
        /// Constructor of a quad tree
        /// </summary>
        /// <param name="dimension">
        /// the size of the rectangle that represents quad tree (bounds)
        /// </param>
        /// <param name="maxDepth">
        /// number of levels of children from root node
        /// </param>
        public QuadTreeStruct(Boundaries dimension, int maxDepth)
        {

            _maxDepth = maxDepth;
            _dimension = dimension;
            //current level of the tree
            _level = 0;
            _root = new Quad(_dimension);
        }

        public Quad GetRoot()
        { return _root; }
  
        /// <summary>
        /// Method for inserting object that is implemented by interface. 
        /// Firstly it controls if there exists any childQuad of the root where the object 
        /// can be located if it fits within boundaries.
        /// Then its added to list of object in the quad.
        /// Condition is controlled for slitting the quad, if the conditions are met 
        /// the quad is splitted and the objects are inserted to correct quad.
        /// </summary>
        /// <param name="spatialObject"></param>
        public void Insert(ISpatialObject spatialObject)
        {
            Quad current = _root;

            while (true)
            {
                //if there is any child of the current quad, determine in which childQuad point belongs to
                if (current.getNW() != null)
                {
                    //find position of the point to the right quad
                    Quad subQuad = spatialObject.FindQuad(current);

                    if (subQuad != null)
                    {

                        current._objects.Remove(spatialObject);
                        current = subQuad;
                        continue;
                    }
                }

                //if current does not have childQuads or it does lie on the border 
                current._objects.Add(spatialObject);
                _objectsCount++;

                while (NeedToSplit(current))
                {
                    current.splitQuad();
                    _level++;
                    //if the quad is splitted every point from the quad must bee extracted and reinserted to the correct child quad
                    Queue<ISpatialObject> reinsertObjects = new Queue<ISpatialObject>();
                    foreach (var rObject in current._objects)
                    {
                        reinsertObjects.Enqueue(rObject);
                    }
                    current._objects.Clear();
                    var pomC = current;
                    //quad is edited now each point add to its designed quad
                    while (reinsertObjects.Count > 0)
                    {
                        //insert the points to the right childQuad

                        var rObject = reinsertObjects.Dequeue();
                        Quad? rQuad = rObject.FindQuad(current);

                        if (rQuad != null)
                        {
                            rQuad._objects.Add(rObject);
                        }
                        else
                        {
                            current._objects.Add(rObject);
                        }
                        pomC = rQuad;
                    }
                    //skontrolovat ci synovia splnaju podmienku splitu
                    current = pomC;
                }
                break;
            }
        }


        /*
         * Find (vyhľadanie) :
         * - bodové aj intervalové
         * - majme zadanú obdĺžniková oblasť S a chceme nájsť všetky záznamy z tejto oblasti
         * - prehľadávaním stromu od koreňa postupne sprístupňujeme tie oblasti quad stromu, 
         * ktoré sa s S prelínajú, neprehľadávame teda tie oblasti, kde S nezasahuje
         * - v každom vrchole prehľadáme zoznam tam uložených záznamov, či patria do S
         * - na listoch skontrolujeme, či záznamy patria do S
         * - výsledkom je zoznam všetkých záznamov z oblasti S
         */

        public ISpatialObject Find(ISpatialObject _object) {
            Queue<Quad> quads = new Queue<Quad>();
            quads.Enqueue(_root);

            while (quads.Count > 0) { 
                Quad quad = quads.Dequeue();
                if (_object.IsContainedInQuad(quad))
                {
                    foreach (var point in quad._objects) {
                        if (point is MyPoint)
                        {
                            if (_object == point)
                            {
                                return (MyPoint?)point;
                            }
                        }
                    }
                    if (quad.getNE() != null)
                    {
                        quads.Enqueue(quad.getNE());
                        quads.Enqueue(quad.getNW());
                        quads.Enqueue(quad.getSE());
                        quads.Enqueue(quad.getSW());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// interval search
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public List<ISpatialObject> Find(Boundaries _searchArea) {
            List<ISpatialObject> result = new List<ISpatialObject>();
            Queue<Quad> queue = new Queue<Quad>();

            // Start the search from the root
            queue.Enqueue(_root);

            while (queue.Count > 0)
            {
                Quad currentQuad = queue.Dequeue();

                // Check if the current quad intersects with the search area
                if (_searchArea.IsContainedInQuad(currentQuad))
                {
                    foreach (var obj in currentQuad._objects)
                    {
                        // Check if the object belongs to the search area
                        if (obj.IsContainedInArea(_searchArea))
                        {
                            result.Add(obj);
                        }
                    }

                    // Add child quads to the queue for further examination
                    if (currentQuad.getNW() != null)
                    {
                        queue.Enqueue(currentQuad.getNW());
                        queue.Enqueue(currentQuad.getNE());
                        queue.Enqueue(currentQuad.getSE());
                        queue.Enqueue(currentQuad.getSW());
                    }
                }
            }

            return result;
        }




        /*public void Insert(MyPoint point)
        {
            Quad current = _root;

            while (true)
            {
                //if there is any child of the current quad, determine in which childQuad point belongs to
                if (current.getNW() != null)
                {
                    //find position of the point to the right quad
                    Quad subQuad = FindQuad(current, point);

                    if (subQuad != null)
                    {
                        
                        current._objects.Remove(point);
                        current = subQuad;
                        continue;
                    }
                }

                //if current does not have childQuads or it does lie on the border 
                current._objects.Add(point);

                while (NeedToSplit(current))
                {
                    current.splitQuad();
                    _level++;
                    //if the quad is splitted every point from the quad must bee extracted and reinserted to the correct child quad
                    Queue<MyPoint> reinsertPointsQ = new Queue<MyPoint>();
                    foreach (var rPoint in current._objects)
                    {
                        reinsertPointsQ.Enqueue((MyPoint)rPoint);
                    }
                    current._objects.Clear();
                    var pomC = current;
                    //quad is edited now each point add to its designed quad
                    while (reinsertPointsQ.Count > 0)
                    {
                        //insert the points to the right childQuad

                        var rPoint = reinsertPointsQ.Dequeue();
                        Quad? rQuad = FindQuad(current, rPoint);

                        if (rQuad != null)
                        {
                            rQuad._objects.Add(rPoint);
                        }
                        else
                        {
                            current._objects.Add(rPoint);
                        }
                        pomC = rQuad;
                    }
                    //skontrolovat ci synovia splnaju podmienku splitu
                    current = pomC;
                }
                break;
            }
        }

        public void Insert(Polygon polygon)
        {
            //to iste ako insert bodu
            //musia mi sediet vsetky body z listu do Quadu
            //ak nie ulozim polygon do listu currenta a nemusil ho splitovat

            Quad current = _root;

            while (true)
            {
                //if there is any child of the current quad, determine in which childQuad polygon belongs to
                if (current.getNW() != null)
                {
                    Quad subQuad = polygon.FindQuad(current);
                    //if subQuad is null - one of the tops of polygon does not fit into quad;

                    if (subQuad != null)
                    {
                        current._objects.Remove(polygon);
                        current = subQuad;
                        continue;
                    }
                }

                current._objects.Add(polygon);

                while (NeedToSplit(current))
                {
                    current.splitQuad();
                    _level++;
                    //if the quad is splitted every point from the quad must bee extracted and reinserted to the correct child quad
                    Queue<Polygon> reinsertPolygonsQ = new Queue<Polygon>();
                    foreach (var rPoint in current._objects)
                    {
                        reinsertPolygonsQ.Enqueue((Polygon)rPoint);
                    }
                    current._objects.Clear();
                    var pomC = current;
                    //quad is edited now each point add to its designed quad
                    while (reinsertPolygonsQ.Count > 0)
                    {
                        //insert the points to the right childQuad

                        var rPoint = reinsertPolygonsQ.Dequeue();
                        //Quad? rQuad = FindQuad(current, rPoint);
                        Quad? rQuad = rPoint.FindQuad(current);

                        if (rQuad != null)
                        {
                            rQuad._objects.Add(rPoint);
                        }
                        else
                        {
                            current._objects.Add(rPoint);
                        }
                        pomC = rQuad;
                    }
                    //skontrolovat ci synovia splnaju podmienku splitu
                    current = pomC;
                }
                break;

            }
        }*/

        public static bool AreQuadsSame(Quad[] quads)
        {
            for (int i = 0; i < quads.Length; i++)
            {
                for (int j = i + 1; j < quads.Length; j++)
                {
                    if (!quads[i].Equals(quads[j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        

        /// <summary>
        /// checks if Quad meets the condition defined from the user
        /// if no -> it returns true so quad needs to be split if conditions are met -> true
        /// </summary>
        /// <param name="quad"></param>
        /// <returns></returns>
        public Boolean NeedToSplit(Quad quad)
        {
            return quad._objects.Count > MAX_QUAD_CAPACITY && _level < _maxDepth ? true : false;
        }

       

        public void Reinsert(Quad current, Queue<MyPoint> reinsertPointsQ) {
            

            while (reinsertPointsQ.Count > 0)
            {
                //insert the points to the right childQuad

                var rPoint = reinsertPointsQ.Dequeue();
                Quad rQuad = FindQuad(current, rPoint);

                if (rQuad != null)
                {
                    rQuad._objects.Add(rPoint);
                }
                else
                {
                    current._objects.Add(rPoint);
                }

            }
        }

        public Quad? FindQuad(Quad current, MyPoint point)
        {
            //find the center of quad -> from there the boundaries will be determined
            double centerX = (current._boundaries.X + (current._boundaries.X + current._boundaries.Width)) / 2;
            double centerY = (current._boundaries.Y + (current._boundaries.Y + current._boundaries.Height)) / 2;

            //SW || NW
            if (point._x < centerX)
            {
                //SW
                if (point._y < centerY)
                {
                    return current.getSW();
                }
                //NW
                else if (point._y > centerY)
                {
                    return current.getNW();
                }

            }
            else //NE || SE
            {
                //SE
                if (point._y < centerY)
                {
                    return current.getSE();
                }
                //NE
                else if (point._y > centerY)
                {
                    return current.getNE();
                }
            }

            //if its not in any childQuad then its set on boundaries
            return null;

            //rewritten with ternaty operators

            /*Quad? result = point._x < centerX ? (point._y < centerY ? current.getSW() : current.getNW()) : (point._y < centerY ? current.getSE() : current.getNE());

            // If result is null, the point is outside all child quadrants and should be placed in the current quad
            return result ?? null;*/

        }

    }
}

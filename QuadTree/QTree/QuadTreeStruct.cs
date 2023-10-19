using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.Xml;
using QuadTree.Structures;

namespace QuadTree.QTree
{
    public class QuadTreeStruct
    {
        private readonly int _maxDepth;
        public int _objectsCount;
        const int MAX_QUAD_CAPACITY = 1;
        //rectangleF - stores 4 data in float that represents size and location of the rectangle
        public Boundaries _dimension;
        private Quad _root;

        /// <summary>
        /// Constructor of a currentQuad tree
        /// </summary>
        /// <param name="dimension">
        /// the size of the rectangle that represents currentQuad tree (bounds)
        /// </param>
        /// <param name="maxDepth">
        /// number of levels of children from root node
        /// </param>
        public QuadTreeStruct(Boundaries dimension, int maxDepth)
        {

            _maxDepth = maxDepth;
            _dimension = dimension;
            //current level of the tree
            _root = new Quad(_dimension,0);
        }

        public Quad GetRoot()
        { return _root; }

        /// <summary>
        /// Method for inserting object that is implemented by interface. 
        /// Firstly it controls if there exists any childQuad of the root where the object 
        /// can be located if it fits within boundaries.
        /// Then its added to list of object in the currentQuad.
        /// Condition is controlled for slitting the currentQuad, if the conditions are met 
        /// the currentQuad is splitted and the objects are inserted to correct currentQuad.
        /// </summary>
        /// <param name="spatialObject"></param>
        public void Insert(ISpatialObject spatialObject)
        {
            Quad current = _root;

            while (true)
            {
                //if there is any child of the current currentQuad, determine in which childQuad point belongs to
                if (current.getNW() != null)
                {
                    //find position of the point to the right currentQuad
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

                //ked mam 2 objekty v koreni a uz je strom split - preco sa splituje znovu ?
                //v tom pripade ak sa nenajde ziadny subquad v predchadzajucom kode moze to znamenat ze je na hrane
                //ak je na hrane a current uz ma nejakych potomkov nepotrebuje sa splitovat


                while (NeedToSplit(current))
                {
                    current.splitQuad();
                    //if the currentQuad is splitted every point from the currentQuad must bee extracted and reinserted to the correct child currentQuad
                    Queue<ISpatialObject> reinsertObjects = new Queue<ISpatialObject>();
                    foreach (var rObject in current._objects)
                    {
                        reinsertObjects.Enqueue(rObject);
                    }
                    current._objects.Clear();
                    var pomC = current;
                    //currentQuad is edited now each point add to its designed currentQuad
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
                    //ak sa do current ulozi null -> objekty su na hrane 
                    if (current == null)
                    {
                        break;
                    }
                }
                break;
            }
        }


        /*
         * PointSearch (vyhľadanie) :
         * - bodové aj intervalové
         * - majme zadanú obdĺžniková oblasť S a chceme nájsť všetky záznamy z tejto oblasti
         * - prehľadávaním stromu od koreňa postupne sprístupňujeme tie oblasti currentQuad stromu, 
         * ktoré sa s S prelínajú, neprehľadávame teda tie oblasti, kde S nezasahuje
         * - v každom vrchole prehľadáme zoznam tam uložených záznamov, či patria do S
         * - na listoch skontrolujeme, či záznamy patria do S
         * - výsledkom je zoznam všetkých záznamov z oblasti S
         */

        public ISpatialObject PointSearch(ISpatialObject _object) {
            Queue<Quad> quads = new Queue<Quad>();
            quads.Enqueue(_root);

            while (quads.Count > 0) {
                Quad quad = quads.Dequeue();
                if (_object.IsContainedInArea(quad._boundaries))
                {
                    //when the object is located in one of the 4 added currentQuad i can erease all of them from there
                    //ill be adding just childQuads of the currentQuad that is object within
                    quads.Clear();

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
        /// 
        /// vstupny parameter su hranice z ktoryh vyberam vsetky objekty
        /// skontrolujem ci sa hranice nachadzaju v roote ak ano, prejdem jeho list objektov a ak sa nachadzaju v boundaries tak ich pridam ako return value
        /// pokracujem childQuadmi root-a, pridam ich do queue
        /// uz nekontrolujem currentQuad z queue lebo hranice obsahuje aspon jeden 
        /// prejdem vsetky a zapisem ich body ak sa zmestia do hranic
        /// 
        /// ASPON JEDEN BOD Z HRANIC SA MUSI NACHADZAT V CHILD QUAD (NIE LEN BOD ALE MOZE PRECHADZAT AJ NA KRIVO)
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public List<ISpatialObject> IntervalSearch(Boundaries boundaries)
        {
            List<ISpatialObject> foundObjects = new List<ISpatialObject>();
            Queue<Quad> quads = new Queue<Quad>();
            quads.Enqueue(_root);

            while (quads.Count > 0) {
                Quad currentQuad = quads.Dequeue();
                if (boundaries.IntersectWithQuad(currentQuad))
                {
                    foreach (var _object in currentQuad._objects)
                    {
                        if (_object.IsContainedInArea(boundaries))
                        {
                            foundObjects.Add(_object);
                        }
                    }

                    if (currentQuad.getNW() != null)
                    {
                        quads.Enqueue(currentQuad.getNW());
                        quads.Enqueue(currentQuad.getNE());
                        quads.Enqueue(currentQuad.getSW());
                        quads.Enqueue(currentQuad.getSE());
                    }
                }
            }

            return foundObjects;
        }



        /// <summary>
        /// interval search with n complexity
        /// 
        /// TODO: Find the smallest possible quad that fits whole boundaries into it and then search withing it
        /// </summary>
        /// <param name="boundaries"></param>
        /// <returns></returns>
        public List<ISpatialObject> IntervalSearchN(Boundaries boundaries)
        {
            List<ISpatialObject> foundObjects = new List<ISpatialObject>();
            Queue<Quad> quads = new Queue<Quad>();
            //quads.Enqueue(_root);

            if (boundaries.IntersectWithQuad(_root))
            {
                quads.Enqueue(_root);
                while (quads.Count > 0) {

                    Quad current = quads.Dequeue();
                    foreach (var _object in current._objects) {
                        if (_object.IsContainedInArea(boundaries))
                        {
                            foundObjects.Add(_object);
                        }
                    }

                    if (current.getNW() != null)
                    {
                        quads.Enqueue(current.getNW());
                        quads.Enqueue(current.getNE());
                        quads.Enqueue(current.getSW());
                        quads.Enqueue(current.getSE());
                    }
                }
                return foundObjects;
            }
            return null;
        }
    
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
        /// if no -> it returns true so currentQuad needs to be split if conditions are met -> true
        /// </summary>
        /// <param name="quad"></param>
        /// <returns></returns>
        public Boolean NeedToSplit(Quad quad)
        {
            return quad._objects.Count > MAX_QUAD_CAPACITY && quad.level < _maxDepth && quad.getNW() == null ? true : false;
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
            //find the center of currentQuad -> from there the boundaries will be determined
            double centerX = (current._boundaries.X0 + current._boundaries.Xk ) / 2;
            double centerY = (current._boundaries.Y0 + current._boundaries.Xk) / 2;

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

        }

        /*public bool RemoveObject(int objectId) 
        {
            Quad current = _root;
            //ak je zaznam v zozname vrcholu tak sa odstrani
            foreach (var _object in current._objects)
            {
                if (_object._id == objectId)
                {
                    current._objects.Remove(_object);
                }
            }

        }*/










    }
}

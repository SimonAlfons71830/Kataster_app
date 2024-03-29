﻿using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.Xml;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using QuadTree.Structures;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace QuadTree.QTree
{
    public class QuadTreeStruct: IQuadTree
    {
        public int _maxDepth { get; set; }
        public int _objectsCount { get; set; }
        public int _objectsSearched { get; set; }
        public int MAX_QUAD_CAPACITY { get; set; }
        //rectangleF - stores 4 data in float that represents size and location of the rectangle
        public Boundaries _dimension { get; set; }
        public Quad _root { get; set; }
        private int maxDepth;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuadTreeStruct"/> class with the specified boundaries and maximum depth.
        /// </summary>
        /// <param name="dimension">The boundaries of the quadtree.</param>
        /// <param name="maxDepth">The maximum depth to which the quadtree will be constructed.</param>
        public QuadTreeStruct(Boundaries dimension, int maxDepth, int maxQuadCapacity)
        {
            _maxDepth = maxDepth;
            MAX_QUAD_CAPACITY = maxQuadCapacity;
            _dimension = dimension;
            _root = new Quad(_dimension,0);
            maxDepth = 0;
        }

        /// <summary>
        /// Gets the root quad of the quadtree structure.
        /// </summary>
        /// <returns>The root <see cref="Quad"/> of the quadtree.</returns>
        public Quad GetRoot() => _root;

        /// <summary>
        /// Method for inserting object which is implemented by interface (Points and Polygons). 
        /// Firstly it controls if there exists any childQuad of the root where the object 
        /// can be located if it fits within boundaries.
        /// Then its added to list of object in the currentQuad.
        /// Condition is controlled for splitting the currentQuad, if the conditions are met 
        /// the currentQuad is split and the objects are inserted to correct Quad.
        /// </summary>
        /// <param name="spatialObject"></param>
        public void Insert(ISpatialObject spatialObject)
        {
            Quad current = _root;
            while (true)
            {
                //if there is any child of the current currentQuad, determine in which childQuad point belongs to
                //if it is on the _borders the subQuad will be null and object will be added to current
                if (current.getNW() != null)
                {
                    //find position of the point to the right currentQuad
                    Quad subQuad = spatialObject.FindQuad(current);
                    if (subQuad != null)
                    {
                        current._objects.Remove(spatialObject);
                        current = subQuad;
                        //continue the iterations until the leaf is found
                        continue;
                    }
                }
                current._objects.Add(spatialObject);
                //pom
                _objectsCount++;

                //check whether the currentQuad needs to be split - (maxQuadCapacity, level, already is split)
                while (NeedToSplit(current))
                {
                    current.splitQuad();
                    
                    //pom
                    if (current._northEast.level > maxDepth)
                    {
                        maxDepth = current._northEast.level;
                    }

                    //every point from the currentQuad must bee removed and reinserted to the correct child of currentQuad
                    Queue<ISpatialObject> reinsertObjects = new Queue<ISpatialObject>(current._objects);
                    current._objects.Clear();
                    //pom to remember the last point's quad
                    var pomC = current;

                    while (reinsertObjects.Count > 0)
                    {
                        //find the right quad for objects
                        var rObject = reinsertObjects.Dequeue();
                        Quad? rQuad = rObject.FindQuad(current);

                        (rQuad ?? current)._objects.Add(rObject);

                        pomC = rQuad;
                    }
                    //check if quad where was the last point inserted needs to be split again 
                    //last point's is checked bc of it the quad needed to be split in first place
                    current = pomC;
                    //if the current is null -> object is on the boundary
                    if (current == null) { break; }
                }
                break;
            }
        }


        public Boolean NeedToSplit(Quad quad)
        {
            return quad._objects.Count > MAX_QUAD_CAPACITY && quad.level < _maxDepth && quad.getNW() == null;
        }

        public MyPoint findCentroid(List<ISpatialObject> _objects) 
        {
            double sumX = 0.0;
            double sumY = 0.0;

            foreach (var point in _objects)
            {
                sumX += point._x; 
                sumY += point._y;
            }

            // Calculate the centroid (midpoint)
            double midX = sumX / _objects.Count;
            double midY = sumY / _objects.Count;

            return new MyPoint(midX, midY, -1);
        }

        /*public void InsertUpdate(ISpatialObject spatialObject)
        {
            Quad current = _root;
            while (true)
            {
                //if there is any child of the current currentQuad, determine in which childQuad point belongs to
                //if it is on the _borders the subQuad will be null and object will be added to current
                if (current.getNW() != null)
                {
                    //find position of the point to the right currentQuad
                    Quad subQuad = spatialObject.FindQuadUpdate(current);
                    if (subQuad != null)
                    {
                        current._objects.Remove(spatialObject);
                        current = subQuad;
                        //continue the iterations until the leaf is found
                        continue;
                    }
                }
                current._objects.Add(spatialObject);
                //pom
                _objectsCount++;

                //check whether the currentQuad needs to be split - (maxQuadCapacity, level, already is split)
                while (NeedToSplit(current))
                {
                    current.splitQuadUpdate(this.findCentroid(current._objects));
                    //every point from the currentQuad must bee removed and reinserted to the correct child of currentQuad
                    Queue<ISpatialObject> reinsertObjects = new Queue<ISpatialObject>(current._objects);
                    current._objects.Clear();
                    //pom to remember the last point's quad
                    var pomC = current;

                    while (reinsertObjects.Count > 0)
                    {
                        //find the right quad for objects
                        var rObject = reinsertObjects.Dequeue();
                        Quad? rQuad = rObject.FindQuadUpdate(current);

                        (rQuad ?? current)._objects.Add(rObject);

                        pomC = rQuad;
                    }
                    //check if quad where was the last point inserted needs to be split again 
                    //last point's is checked bc of it the quad needed to be split in first place
                    current = pomC;
                    //if the current is null -> object is on the boundary
                    if (current == null) { break; }
                }
                break;
            }
        }*/

        /// <summary>
        /// Searches for a specific object within the quadtree based on its location.
        /// </summary>
        /// <param name="_object">The spatial object to search for.</param>
        /// <returns>
        ///   The found <see cref="ISpatialObject"/> if it exists in the quadtree; otherwise, <c>null</c>.
        /// </returns>
        public ISpatialObject PointSearch(ISpatialObject _object) {
            Queue<Quad> quads = new Queue<Quad>();
            quads.Enqueue(_root);

            while (quads.Count > 0) {
                Quad quad = quads.Dequeue();
                if (_object.IsContainedInArea(quad._boundaries, false))
                {
                    //when the object is located in one of the 4 added currentQuad i can erease all of them from there
                    //ill be adding just childQuads of the currentQuad that is object within
                    quads.Clear();

                    foreach (var point in quad._objects) {
                        //if (point is MyPoint)
                        //{
                            if (_object == point)
                            {
                            //return (MyPoint?)point;
                            return point;    
                            }
                        //}
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
        /// Searches for spatial objects within the given boundaries.
        /// </summary>
        /// <param name="boundaries">The boundaries of the area to search for objects.</param>
        /// <returns>
        ///   A list of <see cref="ISpatialObject"/> instances that intersect with the specified boundaries.
        /// </returns>
        public List<ISpatialObject> IntervalSearch(Boundaries boundaries, bool interfere)
        {
            List<ISpatialObject> foundObjects = new List<ISpatialObject>();
            Queue<Quad> quadsQ = new Queue<Quad>();
            quadsQ.Enqueue(_root);


            while (quadsQ.Count > 0)
            {
                Quad current = quadsQ.Dequeue();

                if (boundaries.IntersectsWithArea(current._boundaries))
                {
                    foreach (var _object in current._objects)
                    {
                        this._objectsSearched++;
                        if (_object.IsContainedInArea(boundaries, interfere))
                        {
                            foundObjects.Add(_object);
                        }
                    }

                    if (current.getNE() != null)
                    {
                        quadsQ.Enqueue(current.getNE());
                        quadsQ.Enqueue(current.getNW());
                        quadsQ.Enqueue(current.getSW());
                        quadsQ.Enqueue(current.getSE());
                    }
                }
            }
            return foundObjects;
        }


        /// <summary>
        /// Searches for spatial objects within the given boundaries with N-complexity.
        /// </summary>
        /// <param name="boundaries">The boundaries of the area to search for objects.</param>
        /// <returns>
        ///   A list of <see cref="ISpatialObject"/> instances that intersect with the specified boundaries.
        /// </returns>
        /// <remarks> 
        /// Just for the testing purposes. 
        /// </remarks>
        public List<ISpatialObject> IntervalSearchN(Boundaries boundaries, bool intersects)
        {
            List<ISpatialObject> foundObjects = new List<ISpatialObject>();
            Queue<Quad> quads = new Queue<Quad>();
            //quads.Enqueue(_root);

            if (boundaries.IntersectsWithArea(_root._boundaries))
            {
                quads.Enqueue(_root);
                while (quads.Count > 0) {

                    Quad current = quads.Dequeue();
                    foreach (var _object in current._objects) {
                        if (_object.IsContainedInArea(boundaries, intersects))
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

        
       /* public bool RemoveObject(ISpatialObject _object)
        {
            //find object
            //if quad that would containd P is empty return null
            //else remove P change to empty
            //ak max jeden z childnodov ma nejaky bod tak zucim tieto childnody
            //ak dvaja surodenci obsahuju bod tak ich necham tak
            

            //vyhladanie objektu podla vstupneho parametra, ktorym je objekt s rovnakymi atributami
            var listOfObjectsOnThePositions = this.IntervalSearch(new Boundaries(_object._x, _object._y, _object._x, _object._y),true);
            ISpatialObject objectDelete = null;

            foreach (ISpatialObject _item in listOfObjectsOnThePositions) 
            {
                if (_object is Polygon && _item is Polygon)
                {
                    if (((Polygon)_object).Equals(((Polygon)_item)))
                    {
                        objectDelete = _item;
                        break;
                    }
                }
                else if (_object is MyPoint && _item is MyPoint)
                {
                    if (((MyPoint)_object).Equals((MyPoint)_item))
                    {
                        objectDelete = _item;
                        break;
                    }
                }


            }

            List<Quad> pathToObject = new List<Quad>();
            pathToObject.Add(_root);

            if (objectDelete != null)
            {
                Quad current = _root;
                Quad _parent = null;
                while (current != null)
                {
                    foreach (var _obj in current._objects)
                    {
                        if (_obj == _object)
                        {
                            current._objects.Remove(_obj);

                            if (_parent != null)
                            {
                                //this.Rejoin(current, _parent);
                                this.RejoinUpdate(pathToObject);
                            }
                            

                            this._objectsCount--;
                            return true;
                        }
                    }
                    //not in the current -> search childQuad
                    _parent = current;
                    current = objectDelete.FindQuad(current);
                    pathToObject.Add(current);
                }
                //notfound
                return false;
            }
            else
            {
                return false;
            }

        }*/

        /*public void Rejoin(Quad current, Quad _parent)
        {
            //joining of the split quad
            if (current.getNW() == null)
            {
                Queue<Quad> parentQuads = new Queue<Quad>();
                parentQuads.Enqueue(_parent);
                var count = 0;
                List<ISpatialObject> objectsToReinsert = new List<ISpatialObject>();

                while (parentQuads.Count > 0)
                {
                    Quad quad = parentQuads.Dequeue();
                    count += quad._objects.Count;
                    foreach (var item in quad._objects)
                    {
                        objectsToReinsert.Add(item);
                    }

                    if (quad._southEast != null)
                    {
                        parentQuads.Enqueue(quad.getNW());
                        parentQuads.Enqueue(quad.getSW());
                        parentQuads.Enqueue(quad.getNE());
                        parentQuads.Enqueue(quad.getSE());
                    }
                }
                if (count <= MAX_QUAD_CAPACITY)
                {
                    _parent._southWest = null;
                    _parent._northEast = null;
                    _parent._northWest = null;
                    _parent._southEast = null;

                    _parent._objects = objectsToReinsert;
                }

            }
        }
*/
        /// <summary>
        /// Removing the object from QuadTree.
        /// </summary>
        /// <param name="_object"></param>
        /// <returns></returns>
        public bool RemoveObject(ISpatialObject _object)
        {
            //find object
            //if quad that would containd P is empty return null
            //else remove P change to empty
            //ak max jeden z childnodov ma nejaky bod tak zucim tieto childnody
            //ak dvaja surodenci obsahuju bod tak ich necham tak


            //vyhladanie objektu podla vstupneho parametra, ktorym je objekt s rovnakymi atributami
            var listOfObjectsOnThePositions = this.IntervalSearch(new Boundaries(_object._x, _object._y, _object._x, _object._y), true);
            ISpatialObject objectDelete = null;

            foreach (ISpatialObject _item in listOfObjectsOnThePositions)
            {
                if (_object is Polygon && _item is Polygon)
                {
                    if (((Polygon)_object).Equals(((Polygon)_item)))
                    {
                        objectDelete = _item;
                        break;
                    }
                }
                else if (_object is MyPoint && _item is MyPoint)
                {
                    if (((MyPoint)_object).Equals((MyPoint)_item))
                    {
                        objectDelete = _item;
                        break;
                    }
                }


            }


            List<Quad> pathToObjectQ = new List<Quad>();
            pathToObjectQ.Add(_root);

            if (objectDelete != null)
            {
                Quad current = _root;
                while (current != null)
                {
                    foreach (var _obj in current._objects)
                    {
                        if (_obj == _object)
                        {
                            current._objects.Remove(_obj);
                            this._objectsCount--;
                            //this.Rejoin(current, _parent);
                            this.Rejoin(pathToObjectQ);
                            
                            
                            return true;
                        }
                    }
                    //not in the current -> search childQuad
                    current = objectDelete.FindQuad(current);
                    pathToObjectQ.Add(current);
                }
                //notfound
                return false;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Rejoining the split of the child quad if the conditions are met.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="parent"></param>
        public void Rejoin(List<Quad> pathToObject)
        {
            //objekt mazany z roota
            if (pathToObject.Count == 1)
            {
                if (_objectsCount < MAX_QUAD_CAPACITY)
                {
                    //root je posledny quad
                    Queue<Quad> q = new Queue<Quad>();
                    q.Enqueue(pathToObject.ElementAt(0));
                    List<ISpatialObject> objectsToReinsert = new List<ISpatialObject>();

                    while (q.Count > 0)
                    {
                        var quad = q.Dequeue();
                        objectsToReinsert.AddRange(quad._objects);
                        quad._objects.Clear();
                        if (quad._southEast != null)
                        {
                            q.Enqueue(quad._southEast);
                            q.Enqueue(quad._northEast);
                            q.Enqueue(quad._southWest);
                            q.Enqueue(quad._northWest);
                        }
                    }

                    if (objectsToReinsert.Count < MAX_QUAD_CAPACITY)
                    {
                        _root._objects.AddRange(objectsToReinsert);

                        _root._northEast = null;
                        _root._northWest = null;
                        _root._southEast = null;
                        _root._southWest = null;

                    }
                }
            }

            while (pathToObject.Count > 1)
            {
                //posledny quad kde bol mazany objekt
                var current = pathToObject.ElementAt(pathToObject.Count - 1);
                var parent = pathToObject.ElementAt(pathToObject.Count - 2);

                //joining of the split quad
                //if (current.getNW() == null)
                //{
                    //spocitam objekty parentovych potomkov
                    Queue<Quad> parentQuads = new Queue<Quad>();
                    parentQuads.Enqueue(parent);
                    var count = 0;
                    List<ISpatialObject> objectsToReinsert = new List<ISpatialObject>();

                    while (parentQuads.Count > 0)
                    {

                        Quad quad = parentQuads.Dequeue();
                        count += quad._objects.Count;

                        objectsToReinsert.AddRange(quad._objects);

                        if (quad._southEast != null)
                        {
                            parentQuads.Enqueue(quad.getNW());
                            parentQuads.Enqueue(quad.getSW());
                            parentQuads.Enqueue(quad.getNE());
                            parentQuads.Enqueue(quad.getSE());
                        }

                    }
                    if (count <= MAX_QUAD_CAPACITY)
                    {
                        parent._southWest = null;
                        parent._northEast = null;
                        parent._northWest = null;
                        parent._southEast = null;

                        parent._objects = objectsToReinsert;
                    }
                    else
                    {
                        return;
                    }

                    pathToObject.RemoveAt(pathToObject.Count - 1);

             }
        }



        /*public void RejoinUpdate(List<Quad> pathToObject)
        {
            while (pathToObject.Count >= 2)
            {
                var current = pathToObject.ElementAt(pathToObject.Count - 1);
                var _parent = pathToObject.ElementAt(pathToObject.Count - 2);

                //joining of the split quad
                if (current.getNW() == null)
                {
                    //spocitam objekty parentovych potomkov
                    Queue<Quad> parentQuads = new Queue<Quad>();
                    parentQuads.Enqueue(_parent);
                    var count = 0;
                    List<ISpatialObject> objectsToReinsert = new List<ISpatialObject>();

                    while (parentQuads.Count > 0)
                    {

                        Quad quad = parentQuads.Dequeue();
                        count += quad._objects.Count;
                        foreach (var item in quad._objects)
                        {
                            objectsToReinsert.Add(item);
                        }

                        if (quad._southEast != null)
                        {
                            parentQuads.Enqueue(quad.getNW());
                            parentQuads.Enqueue(quad.getSW());
                            parentQuads.Enqueue(quad.getNE());
                            parentQuads.Enqueue(quad.getSE());
                        }

                    }
                    if (count <= MAX_QUAD_CAPACITY)
                    {
                        _parent._southWest = null;
                        _parent._northEast = null;
                        _parent._northWest = null;
                        _parent._southEast = null;

                        _parent._objects = objectsToReinsert;
                    }

                    pathToObject.RemoveAt(pathToObject.Count - 1);

                }

                if (current.getNW() != null)
                {
                    break;
                }
            }
        }*/

        /// <summary>
        /// creates a new Root
        /// </summary>
        /// <param name="quad"></param>
        public void ResetTree(Quad quad)
        {
            var pom = _root;
            _root = new Quad(pom._boundaries,0);
            _objectsCount = 0;
            _objectsSearched = 0;
        }

        public void SetNewDepth(int newDepth)
        {
            //int maxDepth;
            //Queue<Quad> quadsAtMaxDepth = GetQuadsAtMaxDepth(out maxDepth);

            if (newDepth > maxDepth)
            {
                var list = GetQuadsAtDepth(maxDepth);
                //tree has to grow
                this.Grow(list, newDepth);
                this.maxDepth = newDepth;
                this._maxDepth = newDepth;
            }
            else
            {
                //var quadsToShrink = this.GetQuadsAtDepth(newDepth);
                //tree has to shrink
                this.Shrink(newDepth);
                this.maxDepth = newDepth;
                this._maxDepth = newDepth;
            }
            //else the depth stays the same
        }

        public Queue<Quad> GetQuadsAtDepth(int targetDepth)
        {
            Queue<Quad> allQuads = new Queue<Quad>();
            Queue<Quad> quadQueue = new Queue<Quad>();

            // Start from the root Quad
            quadQueue.Enqueue(_root);

            while (quadQueue.Count > 0)
            {
                // Dequeue the current Quad
                Quad currentQuad = quadQueue.Dequeue();

                // Check if the current Quad has the desired level
                if (currentQuad.level == targetDepth)
                {
                    allQuads.Enqueue(currentQuad);
                }

                // Enqueue child quads if they exist
                if (currentQuad.getNE() != null)
                {
                    quadQueue.Enqueue(currentQuad.getNE());
                    quadQueue.Enqueue(currentQuad.getNW());
                    quadQueue.Enqueue(currentQuad.getSW());
                    quadQueue.Enqueue(currentQuad.getSE());
                }
            }

            return allQuads;
        }

        public void Grow(Queue<Quad> growingQuads, int desiredDepth) 
        {
            while (growingQuads.Count > 0)
            {
                Quad current = growingQuads.Dequeue();
                if (current._objects.Count > 0)
                {
                    //if it has to grow i just need to take the quads from list that has the same level as the newDepth and split them until the level is reached
                    current.splitQuad();
                    //every point from the currentQuad must bee removed and reinserted to the correct child of currentQuad
                    Queue<ISpatialObject> reinsertObjects = new Queue<ISpatialObject>(current._objects);
                    current._objects.Clear();
                    //pom to remember the last point's quad
                    var pomC = current;

                    while (reinsertObjects.Count > 0)
                    {
                        //find the right quad for objects
                        var rObject = reinsertObjects.Dequeue();
                        Quad? rQuad = rObject.FindQuad(current);

                        (rQuad ?? current)._objects.Add(rObject);

                        pomC = rQuad;
                        if (rQuad != null)
                        {
                            if (rQuad._objects.Count != 0 && rQuad.level < desiredDepth)
                            {
                                growingQuads.Enqueue(rQuad);
                            }
                            //otherwise we do not have to split more
                        }
                    }
                }
            }
        }


        public void Shrink(int desiredDepth) 
        {
            // maxDepth - je aktualna level stromu
            //zoberiem vsetky objekty z quadov od <maxDept, newDepth-1> //nemusim ist uplne po spodok ak je maxDepth mensia
            //jednotlivym objektom najdem kvad na newdepth a vlozim ich do neho


            List<ISpatialObject> reinsertObj = new List<ISpatialObject>();
            Queue<Quad> quadsAtDesiredDepth = GetQuadsAtDepth(desiredDepth);
            bool isRoot = desiredDepth == 0 ? true : false;
            Queue<Quad> parentsOfDesiredDepthQuads;
            if (isRoot)
            {
                parentsOfDesiredDepthQuads = null;
            }
            else
            {
                parentsOfDesiredDepthQuads = GetQuadsAtDepth(desiredDepth - 1);
            }



            //prehladat vsetky quads z nizsich levelov ako je desired depth 
            //ulozit si ich objekty do listu
            for (int i = maxDepth; i > desiredDepth; i--)
            {
                Queue<Quad> quadsFromMaxD = this.GetQuadsAtDepth(i);

                foreach (Quad quad in quadsFromMaxD)
                {
                    reinsertObj.AddRange(quad._objects);
                }
            }


            //ak je root tak zoberiem vsetky objekty a vlozim ich do roota inak ->
            if (isRoot)
            {
                _root._objects.AddRange(reinsertObj);
            }
            else
            {
                //pre ulozzene objekty najst prislusny quad z desired depth
                //mam funkciu find quad - kde z parenta najde najvhodnejsieho childQuada - tymi budu desiredDepth quady
                foreach (var _object in reinsertObj)
                {
                    foreach (var quad in parentsOfDesiredDepthQuads)
                    {
                        Quad pomQ = _object.FindQuadUpdate(quad);

                        if (pomQ != null && quadsAtDesiredDepth.Contains(pomQ))
                        {
                            pomQ._objects.Add(_object);
                        }
                    }
                }
            }

            //pre vsetkych desired depth vymazat potomkov - GC
            foreach (var quad in quadsAtDesiredDepth)
            {
                if (quad.getNE() != null)
                {
                    quad._northEast = null;
                    quad._northWest = null;
                    quad._southEast = null;
                    quad._southWest = null;

                }
            }
        }
    }
}

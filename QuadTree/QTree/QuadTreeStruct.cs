﻿using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.Xml;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using QuadTree.Structures;

namespace QuadTree.QTree
{
    public class QuadTreeStruct
    {
        public int _maxDepth { get; set; }
        public int _objectsCount { get; set; }
        public int _objectsSearched = 0;
        public int MAX_QUAD_CAPACITY { get; set; }
        //rectangleF - stores 4 data in float that represents size and location of the rectangle
        public Boundaries _dimension;
        private Quad _root;

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
                //if it is on the borders the subQuad will be null and object will be added to current
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

        /// <summary>
        ///  A quad needs to be split if it exceeds the maximum capacity, has not reached the maximum depth, 
        ///  and has no childQuads.
        /// </summary>
        /// <param name="quad">The Quad to check for splitting.</param>
        /// <returns>
        ///   <c>true</c> if the quad needs to be split; otherwise, <c>false</c>.
        /// </returns>
        public Boolean NeedToSplit(Quad quad)
        {
            return quad._objects.Count > MAX_QUAD_CAPACITY && quad.level < _maxDepth && quad.getNW() == null;
        }

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
        /// Searches for spatial objects within the given boundaries.
        /// </summary>
        /// <param name="boundaries">The boundaries of the area to search for objects.</param>
        /// <returns>
        ///   A list of <see cref="ISpatialObject"/> instances that intersect with the specified boundaries.
        /// </returns>
        public List<ISpatialObject> IntervalSearch(Boundaries boundaries)
        {
            List<ISpatialObject> foundObjects = new List<ISpatialObject>();
            Queue<Quad> quadsQ = new Queue<Quad>();
            quadsQ.Enqueue(_root);


            while (quadsQ.Count > 0) { 
                Quad current = quadsQ.Dequeue();

                if (boundaries.IntersectsWithArea(current._boundaries))
                {
                    foreach (var _object in current._objects)
                    {
                        this._objectsSearched++;
                        if (_object.IsContainedInArea(boundaries))
                        {
                            foundObjects.Add(_object);
                        }
                    }

                    if (current.getNE()!= null)
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
        public List<ISpatialObject> IntervalSearchN(Boundaries boundaries)
        {
            List<ISpatialObject> foundObjects = new List<ISpatialObject>();
            Queue<Quad> quads = new Queue<Quad>();
            //quads.Enqueue(_root);

            if (boundaries.IntersectWith(_root))
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

        public bool RemoveObject(ISpatialObject _object)
        {
            //find object
            //if quad that would containd P is empty return null
            //else remove P change to empty
            //ak max jeden z childnodov ma nejaky bod tak zucim tieto childnody
            //ak dvaja surodenci obsahuju bod tak ich necham tak

            var objectDelete = this.PointSearch(_object);

            if (objectDelete != null)
            {
                Quad current = _root;
                Quad parent = null;
                while (current != null) 
                {
                    foreach (var _obj in current._objects)
                    {
                        if (_obj == _object)
                        {
                            current._objects.Remove(_obj);

                            //joining of the split quad
                            if (current.getNW() == null)
                            {
                                Queue<Quad> parentQuads = new Queue<Quad>();
                                parentQuads.Enqueue(parent);
                                var count = 0;
                                List<ISpatialObject> objectsToReinsert = new List<ISpatialObject>();

                                while (parentQuads.Count > 0) { 
                                
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
                                if (count < MAX_QUAD_CAPACITY)
                                {
                                    parent._southWest = null;
                                    parent._northEast = null;
                                    parent._northWest = null;
                                    parent._southEast = null;

                                    parent._objects = objectsToReinsert;
                                }

                            }
                            this._objectsCount--;
                            return true;
                        }
                    }
                    //not in the current -> search childQuad
                    parent = current;
                    current = objectDelete.FindQuad(current);
                }
                //notfound
                return false;
            }
            else
            {
                return false;
            }

        }
    }
}

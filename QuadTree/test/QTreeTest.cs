﻿using QuadTree.QTree;
using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuadTree.test
{
    public class QTreeTest
    {
        public int pocetOperacii;
        public int podielInsert;
        public int podielRemove;
        public int podielFind;
        //public int seed;

        public int passedInsert;
        public int passedRemove;
        public int passedFind;

        public int failedInsert;
        public int failedRemove;
        public int failedFind;

        public int pocetVykonanychOperacii;
        public int pocetInsert;
        public int pocetRemove;
        public int pocetFind;

        public int generateOptions = 1; //1 - points 2 - polygons 3 - both

        public IQuadTree quadTree = new MyQuadTree(new Boundaries(0.0,0.0, 100.0,100.0),10,1);
        List<ISpatialObject> availableObjects = new List<ISpatialObject>();
        List<ISpatialObject> usedKeys = new List<ISpatialObject>();
        HashSet<int> uniqueNumbers = new HashSet<int>();

        public List<ISpatialObject> failedObj = new List<ISpatialObject>();

        public int passed = 0;
        public int failed = 0;

        private static Random random = new Random(0);

        public void changeStruct(IQuadTree selected_struct)
        {
            quadTree = selected_struct;
        }


        public void TestInsertRemoveFind()
        {
            this.GenerateID();

            //seed the QTree
            this.SeedQT();

            for (int i = 0; i < pocetOperacii; i++)
            {
                int cislo = random.Next(pocetOperacii - i);

                if (cislo < podielInsert) //INSERT
                {
                    this.TestInsert();
                }
                else if(podielInsert <= cislo && cislo < (podielInsert + podielFind)) //FIND
                {
                    this.TestFind();
                }
                else //remove
                {
                    this.TestRemove();

                }
            }
        }

        public void TestInsert() {
            int oldSize = this.quadTree._objectsCount;

            int insertIndex = random.Next(availableObjects.Count);
            //MyPoint insertPoint = (MyPoint)availableObjects[insertIndex];
            //Polygon insertPoint = (Polygon)availableObjects[insertIndex];
            ISpatialObject insertPoint = availableObjects[insertIndex];
            
            //quadTree.Insert(insertPoint);
            quadTree.Insert(insertPoint);
            if (oldSize + 1 == this.quadTree._objectsCount)
            {
                passedInsert++;
                passed++;
            }
            else
            {
                failedInsert++;
                failed++;
            }
            usedKeys.Add(insertPoint);
            availableObjects.RemoveAt(insertIndex);
            podielInsert--;
            pocetInsert++;
            pocetVykonanychOperacii++;
        }

        public void TestFind()
        {
            int tryFindIndex = random.Next(usedKeys.Count);
            //MyPoint tryFindKey = (MyPoint)usedKeys[tryFindIndex];
            //Polygon tryFindKey = (Polygon)usedKeys[tryFindIndex];
            ISpatialObject tryFindKey = usedKeys[tryFindIndex];

            var pomNodeKey = tryFindKey;

            var point = quadTree.PointSearch(tryFindKey);
            if (point == null)
            {
                //failed++; 
                //return; 
            }
            if (pomNodeKey == point)
            {
                passedFind++;
                passed++;
            }
            else
            {

                failedFind++;
                failed++;
                failedObj.Add(tryFindKey);
            }
            podielFind--;
            pocetVykonanychOperacii++;
            pocetFind++;
        }


        public void TestRemove() {
            int removeIndex = random.Next(usedKeys.Count);
            var removeObj = usedKeys[removeIndex];

            var exists = quadTree.PointSearch(removeObj);
            if (exists != null)
            {
                var boolDel = quadTree.RemoveObject(removeObj);
                if (boolDel)
                {
                    passedRemove++;
                    passed++;
                }
                else
                {
                    failedRemove++;
                    failed++;
                }
            }
            else
            {
                failedRemove++;
                //debug
            }
            availableObjects.Add(removeObj);
            usedKeys.RemoveAt(removeIndex);
            podielRemove--;
            pocetVykonanychOperacii++;
            pocetRemove++;
        }

        public void GenerateID()
        {
            //TODO: negenerovat to staitcky

            //generate a set of unique ID for objects inserted to the Qtree
            /*while (uniqueNumbers.Count < 100000) 
            {
                int number = random.Next(1, 10000000); // Generate random numbers between 1 and 10,000

                if (!uniqueNumbers.Contains(number))
                {
                    uniqueNumbers.Add(number);
                }
            }
            List<int> uniqueNumbersList = uniqueNumbers.ToList();
            */
            for (int i = 0; i < (podielFind + podielRemove + podielInsert); i++)
            {
                /* var rozmer = 0;
                 if ((this.quadTree._dimension.Xk - this.quadTree._dimension.X0) > (this.quadTree._dimension.Yk - this.quadTree._dimension.Y0))
                 {
                     rozmer = random.Next(1, (int)(this.quadTree._dimension.Yk - this.quadTree._dimension.Y0) / 5);
                 }
                 else
                 {
                     rozmer = random.Next(1, (int)(this.quadTree._dimension.Xk - this.quadTree._dimension.X0) / 5);
                 }

                 if (this.generateOptions == 1)
                 {
                     availableObjects.Add(new MyPoint(random.Next(0, (int)(quadTree._dimension.Xk - quadTree._dimension.X0)), random.Next(0, (int)(quadTree._dimension.Yk - quadTree._dimension.Y0)), random.Next(10000)));// 0uniqueNumbersList.ElementAt(i)));
                 }
                 else if (this.generateOptions == 2)
                 {
                     var startPosGen = new MyPoint(random.Next((int)quadTree._dimension.X0, (int)quadTree._dimension.Xk - rozmer), random.Next((int)quadTree._dimension.Y0, (int)quadTree._dimension.Yk - rozmer), random.Next(1000000));
                     var endPosGen = new MyPoint(startPosGen._x + rozmer, startPosGen._y + rozmer, random.Next(100000));

                     var _object = new Polygon(random.Next(10000), (new MyPoint(startPosGen._x,startPosGen._y,startPosGen._registerNumber),new MyPoint(endPosGen._x,endPosGen._y,endPosGen._registerNumber)));

                     availableObjects.Add(_object);
                 }
                 else
                 {
                     var chance = random.NextDouble();
                     if (chance > 0.5)
                     {
                         //point
                         availableObjects.Add(new MyPoint(random.Next(0, (int)(quadTree._dimension.Xk - quadTree._dimension.X0)), random.Next(0, (int)(quadTree._dimension.Yk - quadTree._dimension.Y0)), random.Next(10000)));
                     }
                     else
                     {
                         var startPosGen = new MyPoint(random.Next((int)quadTree._dimension.X0, (int)quadTree._dimension.Xk - rozmer), random.Next((int)quadTree._dimension.Y0, (int)quadTree._dimension.Yk - rozmer), random.Next(1000000));
                         var endPosGen = new MyPoint(startPosGen._x + rozmer, startPosGen._y + rozmer, random.Next(100000));

                         var _object = new Polygon(random.Next(10000), (new MyPoint(startPosGen._x, startPosGen._y, startPosGen._registerNumber), new MyPoint(endPosGen._x, endPosGen._y, endPosGen._registerNumber)));

                         availableObjects.Add(_object);
                     }
                 }*/

                //generovanie rozmeru plotu
                var rozmer = 0.0; // Change the data type to double
                if ((this.quadTree._dimension.Xk - this.quadTree._dimension.X0) > (this.quadTree._dimension.Yk - this.quadTree._dimension.Y0))
                {
                    rozmer = random.NextDouble() * ((this.quadTree._dimension.Yk - this.quadTree._dimension.Y0) / 5.0);
                }
                else
                {
                    rozmer = random.NextDouble() * ((this.quadTree._dimension.Xk - this.quadTree._dimension.X0) / 5.0);
                }

                if (this.generateOptions == 1)
                {
                    double startX = random.NextDouble() * (quadTree._dimension.Xk - quadTree._dimension.X0) + quadTree._dimension.X0;
                    double startY = random.NextDouble() * (quadTree._dimension.Yk - quadTree._dimension.Y0) + quadTree._dimension.Y0;
                    availableObjects.Add(new MyPoint(startX, startY, random.Next(10000)));
                }
                else if (this.generateOptions == 2)
                {
                    double startX = random.NextDouble() * (quadTree._dimension.Xk - quadTree._dimension.X0 - rozmer) + quadTree._dimension.X0;
                    double startY = random.NextDouble() * (quadTree._dimension.Yk - quadTree._dimension.Y0 - rozmer) + quadTree._dimension.Y0;
                    double endX = startX + rozmer;
                    double endY = startY + rozmer;

                    var _object = new Polygon(random.Next(10000), (new MyPoint(startX, startY, random.Next(1000000)), new MyPoint(endX, endY, random.Next(100000))));

                    availableObjects.Add(_object);
                }
                else
                {
                    var chance = random.NextDouble();
                    if (chance > 0.5)
                    {
                        double startX = random.NextDouble() * (quadTree._dimension.Xk - quadTree._dimension.X0) + quadTree._dimension.X0;
                        double startY = random.NextDouble() * (quadTree._dimension.Yk - quadTree._dimension.Y0) + quadTree._dimension.Y0;
                        availableObjects.Add(new MyPoint(startX, startY, random.Next(10000)));
                    }
                    else
                    {
                        double startX = random.NextDouble() * (quadTree._dimension.Xk - quadTree._dimension.X0 - rozmer) + quadTree._dimension.X0;
                        double startY = random.NextDouble() * (quadTree._dimension.Yk - quadTree._dimension.Y0 - rozmer) + quadTree._dimension.Y0;
                        double endX = startX + rozmer;
                        double endY = startY + rozmer;

                        var _object = new Polygon(random.Next(10000), (new MyPoint(startX, startY, random.Next(1000000)), new MyPoint(endX, endY, random.Next(100000))));

                        availableObjects.Add(_object);
                    }
                }



            }

        }

        public void SeedQT()
        {
            for (int i = 0; i < podielFind + podielRemove; i++)
            {
                int index = random.Next(availableObjects.Count);
                ISpatialObject _object;
                if (this.generateOptions == 1)
                {
                    _object = availableObjects[index];
                }
                else if (this.generateOptions == 2)
                {
                    _object = availableObjects[index];
                }
                else
                {
                    var chance = random.NextDouble();
                    if (chance > 0.5)
                    {
                        //point
                        _object = availableObjects[index];
                    }
                    else
                    {
                        //polygon
                        _object = availableObjects[index];
                    }
                }
                
                quadTree.Insert(_object);

                usedKeys.Add(_object);
                availableObjects.RemoveAt(index);
            }
        }
        public List<ISpatialObject> IntervalSearchTest(Boundaries boundaries, bool interfere) {

            return quadTree.IntervalSearch(boundaries, interfere);
        }

        public bool TestIntervalSearch(List<ISpatialObject> list1, List<ISpatialObject> list2)
        { 
            return list1.Count == list2.Count;
        }

        public void TestRemoveSeparatelly()
        {
            for (int i = 0; i < podielRemove; i++)
            {
                int removeIndex = random.Next(usedKeys.Count);
                var removeObj = usedKeys[removeIndex];

                var exists = quadTree.PointSearch(removeObj);
                if (exists != null)
                {
                    var boolDel = quadTree.RemoveObject(removeObj);
                    if (boolDel)
                    {
                        passedRemove++;
                        passed++;
                    }
                    else
                    {
                        failedRemove++;
                        failed++;
                    }
                }
                else
                {
                    failedRemove++;
                    var pom = 0;
                    //debug
                }

                availableObjects.Add(removeObj);
                usedKeys.RemoveAt(removeIndex);
                podielRemove--;
                pocetVykonanychOperacii++;
                pocetRemove++;
            }
            
        }

        public List<ISpatialObject> IntervalSearchNcomplex(Boundaries boundaries, bool intersects) {
            return quadTree.IntervalSearchN(boundaries, intersects);
        }

        public void SetPocetOperacii(int pocetOperacii) {
            this.pocetOperacii = pocetOperacii;
        }

        public void SetPocetInsert(int pocetInsert) { 
        this.podielInsert = pocetInsert;
        }

        public void SetPocetRemove(int pocetRemove) { 
            this.podielRemove = pocetRemove;
        }
        public void SetPocetFind(int pocetFind) { 
            this.podielFind = pocetFind;
        }

        /*public void SetSeed(int seedQ) { 
        this.seed = seedQ;
        }*/

        public void setSizeOfTree(double w, double h) {
            quadTree._dimension.X0 = 0;
            quadTree._dimension.Y0 = 0;
            quadTree._dimension.Xk = 0+w;
            quadTree._dimension.Yk = 0+h;
        }

        public void setMaxDepth(int maxDepth) { 
            quadTree._maxDepth = maxDepth;
        }

        public void setMaxObjects(int maxObjCount) { 
            quadTree.MAX_QUAD_CAPACITY = maxObjCount;
        }

        public void setGeneratingObjects(int option)
        {
            this.generateOptions = option;
        }

        public void ResetTest()
        {
            pocetOperacii = 0;
            podielInsert = 0;
            podielRemove = 0;
            podielFind = 0;
            //seed = 0;

            passedInsert = 0;
            passedRemove = 0;
            passedFind = 0;

            failedInsert = 0;
            failedRemove = 0;
            failedFind = 0;

            pocetVykonanychOperacii = 0;
            pocetInsert = 0;
            pocetRemove = 0;
            pocetFind = 0;

            this.availableObjects.Clear();
            this.usedKeys.Clear();
            this.uniqueNumbers.Clear();

            this.passed = 0;
            this.failed = 0;

            quadTree.ResetTree(quadTree._root);
            failedObj = new List<ISpatialObject>();


        }
    }
}

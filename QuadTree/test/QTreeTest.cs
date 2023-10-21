using QuadTree.QTree;
using QuadTree.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int seed;

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

        public QuadTreeStruct quadTree = new QuadTreeStruct(new Boundaries(0.0,0.0, 100.0,100.0),10,1);
        List<ISpatialObject> availableObjects = new List<ISpatialObject>();
        List<ISpatialObject> usedKeys = new List<ISpatialObject>();
        HashSet<int> uniqueNumbers = new HashSet<int>();

        public int passed = 0;
        public int failed = 0;

        private static Random random = new Random();

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
            MyPoint insertPoint = (MyPoint)availableObjects[insertIndex];

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
            MyPoint tryFindKey = (MyPoint)usedKeys[tryFindIndex];
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
            //generate a set of unique ID for objects inserted to the Qtree
            while (uniqueNumbers.Count < 1000)
            {
                int number = random.Next(1, 10000); // Generate random numbers between 1 and 10,000

                if (!uniqueNumbers.Contains(number))
                {
                    uniqueNumbers.Add(number);
                }
            }
            List<int> uniqueNumbersList = uniqueNumbers.ToList();

            for (int i = 0; i < seed; i++)
            {
                availableObjects.Add(new MyPoint(random.Next(0, (int)(quadTree._dimension.Xk - quadTree._dimension.X0)), random.Next(0, (int)(quadTree._dimension.Yk - quadTree._dimension.Y0)), uniqueNumbersList.ElementAt(i)));
            }
        
        }

        public void SeedQT()
        {
            for (int i = 0; i < podielRemove; i++)
            {
                int index = random.Next(availableObjects.Count);
                MyPoint point = (MyPoint)availableObjects[index];
                quadTree.Insert(point);

                usedKeys.Add(point);
                availableObjects.RemoveAt(index);
            }
        }
        public List<ISpatialObject> IntervalSearchTest(Boundaries boundaries) {

            return quadTree.IntervalSearch(boundaries);
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

        public List<ISpatialObject> IntervalSearchNcomplex(Boundaries boundaries) {
            return quadTree.IntervalSearchN(boundaries);
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

        public void SetSeed(int seedQ) { 
        this.seed = seedQ;
        }

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

        public void ResetTest()
        {
            pocetOperacii = 0;
            podielInsert = 0;
            podielRemove = 0;
            podielFind = 0;
            seed = 0;

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

            quadTree.ResetTree(quadTree.GetRoot());



        }
    }
}

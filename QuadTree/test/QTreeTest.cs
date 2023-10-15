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
    internal class QTreeTest
    {
        public int pocetOperacii;
        public int podielInsert;
        //int podielRemove = 60000;
        public int podielFind;
        public int seed;

        public int pocetVykonanychOperacii;
        public int pocetInsert;
        //int pocetRemove;
        public int pocetFind;
        QuadTreeStruct quadTree =new QuadTreeStruct(new Boundaries(0.0,0.0, 1000,1000),10);
        List<ISpatialObject> availableObjects = new List<ISpatialObject>();
        List<ISpatialObject> usedKeys = new List<ISpatialObject>();
        public int passed = 0;
        public int failed = 0;
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void TestujInsertRemoveFind()
        {
            for (int i = 0; i < seed; i++)
            {
                availableObjects.Add(new MyPoint(random.Next(0,1000), random.Next(0,1000), RandomString(1)));
            }

            for (int i = 0; i < podielFind; i++)
            {
                String data = RandomString(1);
                int index = random.Next(availableObjects.Count);
                MyPoint point = (MyPoint)availableObjects[index];
                quadTree.Insert(point);
                //if (binary.Insert(key,data) == null) { failed++; }
                usedKeys.Add(point);
                availableObjects.RemoveAt(index);
                //BTreePrinter.Print(binary.Root);
            }


            for (int i = 0; i < pocetOperacii; i++)
            {
                int cislo = random.Next(pocetOperacii - i);
                //BTreePrinter.Print(binary.Root);
                if (cislo < podielInsert) //0 - 20k
                {
                    int oldSize = this.quadTree._objectsCount;
                    int insertIndex = random.Next(availableObjects.Count);
                    MyPoint insertPoint = (MyPoint)availableObjects[insertIndex];

                    quadTree.Insert(insertPoint);
                    if (oldSize + 1 == this.quadTree._objectsCount)
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                    usedKeys.Add(insertPoint);
                    availableObjects.RemoveAt(insertIndex);
                    podielInsert--;
                    pocetInsert++;
                    pocetVykonanychOperacii++;
                }
                else  // 40 - 100k
                {
                    int tryFindIndex = random.Next(usedKeys.Count);
                    MyPoint tryFindKey = (MyPoint)usedKeys[tryFindIndex];
                    var pomNodeKey = tryFindKey;

                    var point = quadTree.Find(tryFindKey);
                    if (point == null) { 
                        failed++; 
                        return; }
                    if (pomNodeKey == point)
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                    podielFind--;
                    pocetVykonanychOperacii++;
                    pocetFind++;
                }
            }

            Console.WriteLine("POCET VYKONANYCH OPERACII : " + pocetVykonanychOperacii + "\n     " +
                                "pocet operacii insert : " + pocetInsert + "\n       " +
                                "pocet operacii find : " + pocetFind + "\n");
            Console.WriteLine("SUMAR TESTOV : \n\tpassed : " + passed + "\n\tfailed : " + failed);

        }

        public void SetPocetOperacii(int pocetOperacii) {
            this.pocetOperacii = pocetOperacii;
        }

        public void SetPocetInsert(int pocetInsert) { 
        this.podielInsert = pocetInsert;
        }

        public void SetPocetFind(int pocetFind) { 
            this.podielFind = pocetFind;
        }

        public void SetSeed(int seedQ) { 
        this.seed = seedQ;
        }

    }
}

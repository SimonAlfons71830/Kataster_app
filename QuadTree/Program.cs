using QuadTree.GeoSystem;
using QuadTree.Hashing;
using QuadTree.QTree;
using QuadTree.Structures;
using QuadTree.test;
using QuadTree.UI;
using StackExchange.Profiling;
using System.Diagnostics;

namespace QuadTree
{

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var list_of_lands = new List<int>();
            list_of_lands.Add(12000);
            list_of_lands.Add(10000);
            var prop = new Property(52000, "NewProperty", (new Coordinates(12.3, 15.9, -1), new Coordinates(95.3, 28.6, -1)), list_of_lands);

            var size = prop.getSize();
            var hash = prop.getHash();
            var bytearr = prop.toByteArray();
            var prop2 = new Property();
            prop2.fromByteArray(bytearr);

            var hashing = new DynamicHashing<Property>("hashFile", 2);
            hashing.Insert(prop);

            hashing.Insert(new Property(11000, "Property1", (new Coordinates(0, 0, -1), new Coordinates(100, 100, -1)), list_of_lands));
            hashing.Insert(new Property(11000, "Property2", (new Coordinates(20, 20, -1), new Coordinates(100, 100, -1)), list_of_lands));
            hashing.Insert(new Property(11000, "Property3", (new Coordinates(50, 0, -1), new Coordinates(100, 100, -1)), list_of_lands));
            
            hashing.Find(prop);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var test = new QTreeTest();
            var app = new GeoApp(new MyQuadTree(new Boundaries(0,0,0,0),0,0));
            Application.Run(new Test(test,app));
            //Application.Run(new GUI());

           
        }
        

    }
}
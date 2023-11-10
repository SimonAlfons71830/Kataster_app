using QuadTree.GeoSystem;
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
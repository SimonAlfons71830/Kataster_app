using QuadTree.QTree;
using QuadTree.Structures;
using QuadTree.test;
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

            // Create a Quadtree instance
            var quadTree = new QuadTreeStruct(new Boundaries(0,0,100,100),7);

            // Create a list to store points
            List<MyPoint> points = new List<MyPoint>();

            // Create and add points to the list
            MyPoint A = new MyPoint(56.0, 60.0, "A");
            MyPoint C = new MyPoint(70.0, 20.0, "C");
            MyPoint D = new MyPoint(60.0, 28.0, "D");
            MyPoint E = new MyPoint(60.0, 21.0, "E");
            MyPoint E2 = new MyPoint(60.0, 21.0, "E2");
            MyPoint H = new MyPoint(50.0, 50.0, "H");

            Polygon P1 = new Polygon();
            P1.AddTop(new MyPoint(11.0,89.0,"A"));
            P1.AddTop(new MyPoint(20.0, 89.0,"B"));
            P1.AddTop(new MyPoint(11.0, 59.0, "C"));
            P1.AddTop(new MyPoint(20.0, 59.0, "D"));

            Polygon P2 = new Polygon();
            P2.AddTop(new MyPoint(78.0, 15.0, "M"));
            P2.AddTop(new MyPoint(89.0, 15.0, "N"));
            P2.AddTop(new MyPoint(78.0, 7.0, "O"));
            P2.AddTop(new MyPoint(89.0, 7.0, "P"));




            /* quadTree.Insert(A);
             quadTree.Insert(C);
             quadTree.Insert(D);
             quadTree.Insert(E);
             quadTree.Insert(E2);
             quadTree.Insert(H);*/
            var newPoint = new MyPoint(50.0, 50.0, "center");
            var newPoint2 = new MyPoint(100.0, 100.0, "end");
           
            quadTree.Insert(newPoint);
            quadTree.Insert(newPoint2);
            quadTree.Insert(A);
            quadTree.Insert(C);
            quadTree.Insert(D);
            quadTree.Insert(E);

            //Process.Start("cmd.exe");

            var point =  quadTree.PointSearch(newPoint);
            var point2 = quadTree.PointSearch(newPoint2);

            List<ISpatialObject> spatialObjects = quadTree.IntervalSearchN(new Boundaries(0.0,0.0,100.0,100.0));

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());

            

        }
        

    }
}
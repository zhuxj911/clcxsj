using System;

namespace Ch03Ex01
{
    class Program
    {
        static void Main(string[] args)
        {
            Circle c1 = new Circle(100, 100, 80);
            Circle c2 = new Circle(200, 200, 110);

            Console.WriteLine("Circle1的面积={0}", c1.Area );
            Console.WriteLine("Circle2的面积={0}", c2.Area);

            bool yes = c1.IsIntersectWithCircle(c2);
            Console.WriteLine("Circle1与Circle2是否相交:{0}", 
				yes ? "是" : "否" );

            //Polyline pl = new Polyline();
            //pl.Add(-1,  0);
            //pl.Add(2, 3);
            //pl.Add(4, 2);
            //pl.Add(4, 4);
            //pl.Add(6, 8);
            //pl.Add(-2, 5);
            //Console.WriteLine("Polyline's Area={0}, Length={1}", pl.Area, pl.Length );

            //double[,] pts = new double[,] {
            //    { -1,0},
            //    { 2, 3 }, 
            //    { 4, 2 }, 
            //    { 4, 4 }, 
            //    { 6, 8 }, 
            //    { -2, 5 }};
            //Polyline pl = new Polyline(pts);
            //Console.WriteLine("Polyline's Area={0}, Length={1}", pl.Area, pl.Length);

          Point[] pts = new Point[]{
                new Point(-1,0),
                new Point(2, 3 ),
                new Point(4, 2 ),
                new Point(4, 4 ),
                new Point(6, 8 ),
                new Point(-2, 5 )};
            Polyline pl = new Polyline(pts);
            Console.WriteLine("Polyline's Area={0}, Length={1}", pl.Area, pl.Length);
        }
    }
}

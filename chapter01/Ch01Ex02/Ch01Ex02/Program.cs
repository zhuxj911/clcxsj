using System;

namespace Ch01Ex02
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p1 = new Point();
            p1.name = "p1";
            p1.x = 100;
            p1.y = 100;
            p1.z = 425.324;


            Circle c1 = new Circle();
            c1.center = p1;
            c1.r = 80;

            Point p2 = new Point();
            p2.name = "p2";
            p2.x = 200;
            p2.y = 200;
            p2.z = 417.626;

            Circle c2 = new Circle();
            c2.center = p2;
            c2.r = 110;

            c1.Area(); //计算圆c1的面积
            Console.WriteLine("Circle1的面积={0}", c1.area );

            c2.Area(); //计算圆c2的面积
            Console.WriteLine("Circle2的面积={0}", c2.area);

            bool yes = c1.IsIntersectWithCircle(c2);
            Console.WriteLine("Circle1与Circle2是否相交:{0}", 
				yes ? "是" : "否" );
        }
    }
}

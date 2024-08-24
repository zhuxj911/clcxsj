using System;

namespace Ch01Ex03
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
        }
    }
}

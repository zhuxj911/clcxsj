using System;

namespace Ch01Ex02
{
    class Point
    {
        public string name;
        public double x;
        public double y;
        public double z;

        public double Distance(Point p2)
        {
            double dx = x - p2.x;
            double dy = y - p2.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    class Circle
    {
        public Point center;
        public double r;
        public double area;
        public double length;

        public void Area()
        {
            area = Math.PI * r * r;
        }

        public bool IsIntersectWithCircle(Circle c2)
        {
            double d = this.center.Distance(c2.center);
            return d <= (r + c2.r);
        }
    }
}

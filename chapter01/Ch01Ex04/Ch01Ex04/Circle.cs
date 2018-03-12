using System;

namespace Ch01Ex
{
    public class Point
    {
        public string Name { get;  set;}
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point(double x, double y)
        {
            Name = "";
            X = x; Y = y;  Z = 0;
        }

        public Point(string name, double x, double y, double z)
        {
            Name = name;
            X = x; Y = y; Z = z;
        }

        public double Distance(Point p2)
        {
            double dx = X - p2.X;
            double dy = Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    public class Circle
    {
        public Point Center{ get; set; }

        private double r;
        public double R
        {
            get { return r; }
            set
            {
                if (r != value) { r = value; CalArea(); }
            }
        }

        private double area;
        public double Area
        {
            get { return area; }
        }

        private double length;

        public Circle(Point center, double r)
        {
            this.Center = center;
            this.r = r;

            CalArea();
        }

        public Circle(double x, double y, double r)
        {
            Center = new Point(x, y);
            this.r = r;

            CalArea();
        }

        private void CalArea()
        {
            area = Math.PI * r * r;
        }

        //判断两圆是否相交
        public bool IsIntersectWithCircle(Circle c2)
        {
            double d = this.Center.Distance(c2.Center);
            return d <= (r + c2.r);
        }
    }
}

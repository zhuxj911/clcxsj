using System;

namespace Ch01Ex03
{
    class Point
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double x;
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        private double y;
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        private double z;
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public Point(double x, double y)
        {
            this.name = "";
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        public Point(string name, double x, double y, double z)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Distance(Point p2)
        {
            double dx = X - p2.X;
            double dy = Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    class Circle
    {
        public Point Center { get; set; }

        private double r;
        public double R
        {
            get { return r; }
            set
            {
                if(value != r && value >= 0)
                {
                    r = value;
                    CalArea(); //r的值发生改变，重新计算圆的面积
                }
            }
        }

        private double area;
        public double Area
        {
            get { return area; }
        }

        private double length;

        public Circle(double x, double y, double r)
        {
            Center = new Point(x, y);

            this.R = r;  //赋值给R而不是this.r，确保计算圆的面积
        }

        //计算圆的面积
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

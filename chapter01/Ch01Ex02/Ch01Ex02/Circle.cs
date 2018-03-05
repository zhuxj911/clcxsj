using System;

namespace Ch01Ex02
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

        public Point()
        {
            Name = "";
            x = y = z = 0;
        }
        //函数重载：函数名称一样，但参数（个数或类型）不一样
        public Point(string name, double x, double y, double z)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
        }


        /// <summary>
        /// 计算当前点至p2点的距离
        /// </summary>
        /// <param name="p2">目标点</param>
        /// <returns>两点的距离</returns>
        public double Distance(Point p2)
        {
            double dx = X - p2.X;
            double dy = Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }

    class Circle
    {
        private Point center;
        private double r;

        private double area;
        public double Area
        {
            get { return area; }
        }

        private double length;

        public Circle()
        {
           
        }

        public Circle(string centerName, double x, double y, double z, double r)
        {
            center = new Point(centerName, x, y, z);
            this.r = r;

            CalArea();
        }


        /// <summary>
        /// 计算圆的面积
        /// </summary>
        private void CalArea()
        {
            area = Math.PI * r * r;
        }

        //判断两圆是否相交
        public bool IsIntersectWithCircle(Circle c2)
        {
            double d = this.center.Distance(c2.center);
            return d <= (r + c2.r);
        }
    }
}

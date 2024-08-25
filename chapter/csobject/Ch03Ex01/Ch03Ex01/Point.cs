using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03Ex01
{
    public class Point : Shape
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
}

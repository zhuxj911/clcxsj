using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03Ex01
{
    public class Polygon : Shape
    {
        private List<Point> points = new List<Point>();

        public double CalLength()
        {
            if (points.Count < 2) return 0;
            double sum = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int j = (i + 1) % points.Count;
                double dx = points[j].X - points[i].X;
                double dy = points[j].Y - points[i].Y;
                sum += Math.Sqrt(dx * dx + dy * dy);
            }
            return sum;
        }

        public double CalArea()
        {
            if (points.Count < 3) return 0;
            double s = 0;
            for (int i = 0; i < points.Count; i++)
            {
                int j = i + 1;
                if (j == points.Count) j = 0;
                s += points[i].X * points[j].Y - points[j].X * points[i].Y;
            }
            return 0.5 * Math.Abs(s);
        }

        public void Add(double x, double y)
        {
            points.Add(new Point(x, y));
        }
        
        public override void Calculate()
        {
            this.length = CalLength();
            this.area = CalArea();
        }

        public override void Draw()
        {
            Console.WriteLine("Polygon Draw ......");
        }

        public override string ToString()
        {
            return $"Polygon's Area ={Area}, Length ={Length}";
        }
    }
}

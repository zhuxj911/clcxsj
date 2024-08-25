using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03Ex01
{
    public class Polyline : Shape
    {
        private List<Point> points = new List<Point>();

        private double CalLength()
        {
            if (points.Count < 2) return 0;
            double sum = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                double dx = points[i + 1].X - points[i].X;
                double dy = points[i + 1].Y - points[i].Y;
                sum += Math.Sqrt(dx * dx + dy * dy);
            }
            return sum;
        }       

        public void Add(double x, double y)
        {
            points.Add(new Point(x, y));
        }

        public Polyline()
        {
        }       

        public override void Calculate()
        {
            this.length = CalLength();
            this.area = 0.0;

        }

        public override void Draw()
        {
            Console.WriteLine("Polyline Draw ......");
        }

        public override string ToString()
        {
            return $"Polyline's Area ={Area}, Length ={Length}";
        }
    }
}

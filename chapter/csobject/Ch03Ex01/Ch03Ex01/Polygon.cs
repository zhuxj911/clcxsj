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

        public double Length
        {
            get
            {
                if (points.Count < 2) return 0;
                double sum = 0;
                for (int i = 0; i < points.Count-1; i++)
                {
                    double dx = points[i+1].X - points[i].X;
                    double dy = points[i + 1].Y - points[i].Y;
                    sum += Math.Sqrt(dx*dx + dy*dy);
                }
                return sum;
            }
        }
        public double Area
        {
            get
            {
                if(points.Count < 3) return 0;
                double s = 0;
                for(int i=0; i< points.Count; i++)
                {
                    int j = i + 1;
                    if (j == points.Count) j = 0;
                    s += points[i].X * points[j].Y - points[j].X * points[i].Y;
                }
                return 0.5 * Math.Abs(s);
            }
        }

        public void Add(double x, double y)
        {
            points.Add(new Point(x, y));
        }

        public void Insert(int index, double x, double y)
        {
            points.Insert(index, new Point(x, y));
        }


        public Polyline()
        {
        }

        public Polyline(Point[] pts)
        {
            foreach (var pt in pts)
            {
                points.Add(pt);
            }
        }

        public Polyline(double[ ,] xy)
        {
            for (var i=0; i<xy.GetLength(0); i++)
            {
                points.Add(new Point(xy[i, 0], xy[i,1]) );
            }
        }
    }
}

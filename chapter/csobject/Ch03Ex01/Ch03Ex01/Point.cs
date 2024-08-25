using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03Ex01
{
    public sealed class Point : Shape
    {      
        public double X { get; set; }
        public double Y { get; set; }        

        public Point(double x, double y)
        {          
            this.X = x;
            this.Y = y;          
        }

        public override void Calculate()
        {
            this.area = 0.0;
            this.length = 0.0;
        }

        public override void Draw()
        {
            Console.WriteLine("Point Draw ......");
        }

        public override string ToString()
        {
            return $"Point's Area ={Area}, Length ={Length}";
        }
    }
}

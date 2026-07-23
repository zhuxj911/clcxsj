using System;

namespace Ch03Ex01
{
    public class Circle : Shape
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
                    Calculate(); //r的值发生改变，重新计算圆的面积
                }
            }
        }
        

        public Circle(double x, double y, double r)
        {
            Center = new Point(x, y);
            this.R = r;  //赋值给R而不是this.r，确保计算圆的面积
        }

        public override void Calculate()
        {
            this.length = Math.PI * r * 2;
            this.area = Math.PI * r * r;

        }

        public override void Draw()
        {
            Console.WriteLine("Circle Draw ......");
        }

        public override string ToString()
        {
            return $"Circle's Area ={Area}, Length ={Length}";
        }
    }
}

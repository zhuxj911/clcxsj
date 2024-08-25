using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03Ex01
{
    public abstract class Shape : ICalculate, IDraw
    {
        protected double length = 0.0;
        public double Length { get => length; }

        protected double area = 0.0;
        public double Area { get =>area; }

        public abstract void Calculate();
        public abstract void Draw();
    }
}

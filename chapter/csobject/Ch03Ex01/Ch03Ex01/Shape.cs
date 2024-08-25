using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch03Ex01
{
    public abstract class Shape : IShape, IDraw
    {
        public abstract double Length { get;  }

        public abstract double Area { get; }

        public abstract void Draw();
    }
}

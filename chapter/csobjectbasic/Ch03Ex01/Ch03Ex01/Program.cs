using System;
using System.Collections.Generic;

namespace Ch03Ex01
{
    class Program
    {
        static void Main(string[] args)
        {
            //Shape s = new Shape();
            //s.Calculate();
            //Console.WriteLine("Shape's Area={0}, Length={1}", s.Area, s.Length);
            //s.Draw();

            //Point p = new Point(100, 100);
            //p.Calculate();
            //Console.WriteLine("Point's Area={0}, Length={1}", p.Area, p.Length);
            //p.Draw();

            //Circle c = new Circle(100, 100, 80);
            //c.Calculate();
            //Console.WriteLine("Circle1's Area={0}, Length={1}", c.Area, c.Length);
            //c.Draw();

            //Polyline pl = new Polyline();
            //pl.Add(-1, 0);
            //pl.Add(2, 3);
            //pl.Add(4, 2);
            //pl.Add(4, 4);
            //pl.Add(6, 8);
            //pl.Add(-2, 5);
            //pl.Calculate();
            //Console.WriteLine("Polyline's Area={0}, Length={1}", pl.Area, pl.Length);
            //pl.Draw();

            //Polygon pg = new Polygon();
            //pg.Add(-1, 0);
            //pg.Add(2, 3);
            //pg.Add(4, 2);
            //pg.Add(4, 4);
            //pg.Add(6, 8);
            //pg.Add(-2, 5);
            //pg.Calculate();
            //Console.WriteLine("Polygon's Area={0}, Length={1}", pg.Area, pg.Length);
            //pg.Draw();

            List<Shape> shapeList = new List<Shape>();
            shapeList.Add(new Point(100, 100));
            shapeList.Add(new Circle(100, 100, 80));

            Polyline pl = new Polyline();
            pl.Add(-1, 0);
            pl.Add(2, 3);
            pl.Add(4, 2);
            pl.Add(4, 4);
            pl.Add(6, 8);
            pl.Add(-2, 5);
            shapeList.Add(pl);

            Polygon pg = new Polygon();
            pg.Add(-1, 0);
            pg.Add(2, 3);
            pg.Add(4, 2);
            pg.Add(4, 4);
            pg.Add(6, 8);
            pg.Add(-2, 5);
            shapeList.Add(pg);

            List<IDraw> drawList = new List<IDraw>();
            foreach (var item in shapeList)
            {
                IDraw idraw = item;
                drawList.Add(idraw);
            }
            DrawAll(drawList);

            List<ICalculate> calculateList = new List<ICalculate>();
            foreach (var item in calculateList)
            {
                ICalculate icalculate = item as ICalculate;
                calculateList.Add(icalculate);
            }
            CalculateAll(calculateList);

            foreach (var item in shapeList)
            {                
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }

        static void CalculateAll(List<ICalculate> calculateList)
        {
            foreach (var item in calculateList)
            {
                item.Calculate();           
            }
        }

        static void DrawAll(List<IDraw> shapeList)
        {
            foreach (var item in shapeList)
            {
                item.Draw();
            }
        }
    }
}

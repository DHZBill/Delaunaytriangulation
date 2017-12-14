using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace DivideAndConquer
{
    public class DivideAndConquer
    {

        private static List<Point> Points = new List<Point>();
        private static List<Triangulation> StoredList = new List<Triangulation>();
        private static List<Circle> Circles = new List<Circle>();

        private static List<Point> sortPoints(List<Point> inpList)
        {
            var sorted = new List<Point>();
            sorted = inpList.OrderBy(t => t.X()).ThenBy(t => t.Y()).ToList();
            return sorted;
        }

        private static List<List<Point>> divide(List<Point> pts)
        {
            var split = pts.Select((s, i) => new {s, i}).GroupBy(x => x.i % 4).Select(g => g.Select(x => x.s).ToList())
                .ToList();
            return split;
        }
        
        
        
        public static void Main(string[] args)
        {
            var pointA = new Point(174, 274);
            var pointB = new Point(94, 232);
            var pointC = new Point(87, 166);
            var pointD = new Point(144, 181);
            var pointE = new Point(225, 182);
            var pointF = new Point(150, 113);
            var pointG = new Point(220, 88);

            
            Points.Add(pointA);
            Points.Add(pointB);
            Points.Add(pointC);
            Points.Add(pointD);
            Points.Add(pointE);
            Points.Add(pointF);
            Points.Add(pointG);
            
            Circle c1 = Circle.getCircle(pointA, pointB, pointD);
            Circle c2 = Circle.getCircle(pointB, pointC, pointD);
            Circle c3 = Circle.getCircle(pointA, pointD, pointE);
            Circle c4 = Circle.getCircle(pointC, pointD, pointF);
            Circle c5 = Circle.getCircle(pointD, pointE, pointF);
            Circle c6 = Circle.getCircle(pointE, pointF, pointG);
            
            Circles.Add(c1);
            Circles.Add(c2);
            Circles.Add(c3);
            Circles.Add(c4);
            Circles.Add(c5);
            Circles.Add(c6);

            foreach (var c in Circles)
            {
                Console.WriteLine("Center: " + c.GetCenter() + " Radius: " + c.GetRadius());
            }
        }
    }
}

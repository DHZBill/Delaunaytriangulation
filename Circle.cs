using System;

namespace DivideAndConquer
{
    public class Circle
    {
        private Point center;
        private double radius;
        public Circle(Point center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public Point GetCenter()
        {
            return center;
        }

        public double GetRadius()
        {
            return radius;
        }
        
        // Check if the point is inside of the given circle
        public static bool pointInCircle(Point p, Circle c)
        {
            var distance = Math.Sqrt((p.X() - c.GetCenter().X()) * (p.X() - c.GetCenter().X()) +
                                     (p.Y() - c.GetCenter().Y()) * (p.Y() - c.GetCenter().Y()));
            return distance <= c.GetRadius();
        }
        
        // Returns a circle defined by center and radius given 3 points on the circle
        public static Circle getCircle(Point p1, Point p2, Point p3)
        {
            var x1 = p1.X();
            var x2 = p2.X();
            var x3 = p3.X();
            var y1 = p1.Y();
            var y2 = p2.Y();
            var y3 = p3.Y();
            
            var midPt1 = new Point((x1+x2)/2, (y1+y2)/2);
            var midPt2 = new Point((x1+x3)/2, (y1+y3)/2);

            var k1 = -(x2 - x1) / (y2 - y1);
            var k2 = -(x3 - x1) / (y3 - y1);

            var centerX = (midPt2.Y() - midPt1.Y() - k2 * midPt2.X() + k1 * midPt1.X()) / (k1 - k2);
            var centerY = midPt1.Y() +
                          k1 * (midPt2.Y() - midPt1.Y() - k2 * midPt2.X() + k2 * midPt1.X()) / (k1 - k2);

            var center = new Point(centerX, centerY);
            var radius = Math.Sqrt((centerX - x1) * (centerX - x1) + (centerY - y1) * (centerY - y1));
            
            var circle = new Circle(center, radius);

            return circle;
        }

    }
}
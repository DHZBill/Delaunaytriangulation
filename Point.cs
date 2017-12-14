using System.Dynamic;

namespace DivideAndConquer
{
    public class Point
    {
        private double x;
        private double y;
        private double angle;
        public Point()
        {
            
        }
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X()
        {
            return x;
        }

        public double Y()
        {
            return y;
        }

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public void Set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetAngle(double x)
        {
            this.angle = x;
        }

        public bool Equals(Point other)
        {
            return (this.x == other.x && this.y == other.y);
        }

        public override string ToString()
        {
            return "(" + this.x + "," + this.y + ")";
        }
    }
}
using System;

namespace DivideAndConquer
{
    public class Edge
    {
        private Point pl;
        private Point pr;
        public Edge(Point pl, Point pr)
        {
            this.pl = pl;
            this.pr = pr;
        }

        public Point Pl()
        {
            return this.pl;
        }
        

        public Point Pr()
        {
            return this.pr;
        }

        public void Set(Point p1, Point p2)
        {
            this.pl = p1;
            this.pr = p2;
        }
        
    }
}
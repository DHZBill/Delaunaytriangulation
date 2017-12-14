using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace DivideAndConquer
{
    public class Triangulation
    {
        private Edge baseEdge;
        private Point leftCandidate;
        private Point rightCandidate;
        private Point nextLeftCandidate;
        private Point nextRightCandidate;
        private Point finalLeftCandidate;
        private Point finalRightCandidate;
        private List<Point> Vertices = new List<Point>();
        private List<Edge> Edges = new List<Edge>();

        public Triangulation()
        {

        }

        public List<Point> GetVertices()
        {
            return Vertices;
        }

        public List<Edge> GetEdges()
        {
            return Edges;
        }
        public void AddVertex(Point pt)
        {
            this.Vertices.Add(pt);
        }

        public void AddEdge(Edge e)
        {
            this.Edges.Add(e);
        }

        

        public void MergeTriangulation(Triangulation tl, Triangulation tr)
        {
            foreach (var vertex in tl.Vertices) this.AddVertex(vertex);
            foreach (var vertex in tr.Vertices) this.AddVertex(vertex);

            foreach (var edge in tr.Edges) this.AddEdge(edge);
            foreach (var edge in tr.Edges) this.AddEdge(edge);

        }

        public void AddLREdge(Triangulation tl, Triangulation tr, Edge b)
        {

            baseEdge = b;
            foreach (var pt in tr.Vertices)
            {
                pt.SetAngle(FindAngle(baseEdge, pt, false));
            }
            foreach (var pt in tl.Vertices)
            {
                pt.SetAngle(FindAngle(baseEdge, pt, true));
            }

            var sortedR = tr.Vertices.OrderBy(t => t.Angle).ToList();
            var sortedL = tl.Vertices.OrderBy(t => t.Angle).ToList();

            sortedR.RemoveAll(t => t.Angle <= 0);
            sortedL.RemoveAll(t => t.Angle <= 0);

            finalRightCandidate = findPotentialCandidate(tr,sortedR, baseEdge, false);
            finalLeftCandidate = findPotentialCandidate(tl,sortedL, baseEdge, true);

            if (finalRightCandidate != null && finalLeftCandidate != null)
            {
                Circle circle = Circle.getCircle(finalLeftCandidate, baseEdge.Pl(), baseEdge.Pr());
                if (!Circle.pointInCircle(finalRightCandidate, circle))
                {
                    AddEdge(new Edge(finalLeftCandidate, baseEdge.Pr()));
                    AddLREdge(tl, tr, baseEdge);

                }
                else
                {
                    AddEdge(new Edge(finalRightCandidate, baseEdge.Pl()));
                    AddLREdge(tl, tr, baseEdge);

                }
            }
            else if (finalRightCandidate != null && finalLeftCandidate == null)
            {
                AddEdge(new Edge(finalRightCandidate, baseEdge.Pl()));
                baseEdge.Set(finalRightCandidate, baseEdge.Pl());
                AddLREdge(tl, tr, baseEdge);

            }
            else if (finalRightCandidate == null && finalLeftCandidate != null)
            {
                AddEdge(new Edge(finalLeftCandidate, baseEdge.Pr()));
                baseEdge.Set(finalLeftCandidate, baseEdge.Pr());
                AddLREdge(tl, tr, baseEdge);
            }
        }

        public static Point findPotentialCandidate(Triangulation tri, List<Point> ptList, Edge e, bool left)

        {
            var pts = ptList;   
            if (pts.Count != 0)
            {
                var candidate = pts[0];
                if (pts.Count == 1) return candidate;
                else
                {
                    var nextCandidate = pts[1];
                    Circle circle = Circle.getCircle(e.Pl(), e.Pr(), candidate);
                    if (!Circle.pointInCircle(nextCandidate, circle))
                    {
                        return candidate;
                    }
                    else
                    {
                        pts.RemoveAt(0);
                        tri.Edges.RemoveAll(x => x.Equals(left ? e.Pl():e.Pr()));
                        findPotentialCandidate(tri, pts, e, left);
                    }
                }
            }
            return null;
        }
        
        public static double FindAngle(Edge baseEdge, Point candidate, bool left)
        {
            var p1 = new Point();
            var p0 = new Point();
            if (!left)
            {
                 p0.Set(baseEdge.Pr().X(), baseEdge.Pr().Y());
                p1.Set(baseEdge.Pl().X(), baseEdge.Pl().Y());
            }
            else
            {
                p0.Set(baseEdge.Pl().X(), baseEdge.Pl().Y());
                p1.Set(baseEdge.Pr().X(), baseEdge.Pr().Y());
            }
            var p2 = new Point(candidate.X(), candidate.Y());
            
            var v1 = new Point(p1.X() - p0.X(), p1.Y() - p0.Y());
            var v2 = new Point(p2.X() - p0.X(), p2.Y() - p0.Y());

            var dot = v1.X() * v2.X() + v1.Y() * v2.Y();
            var det = v1.X() * v2.Y() + v1.Y() * v2.X();
            var radian = Math.Atan2(det, dot);
            return radian;
        }

        public Point BottomMostPoint()
        {
            var sorted = new List<Point>();
            sorted = this.Vertices.OrderBy(t => t.Y()).ThenByDescending(t => t.X()).ToList();
            return sorted[0];
        }

        public Edge findBaseEdge(Triangulation tr1, Triangulation tr2)
        {
            return new Edge(tr1.BottomMostPoint(), tr2.BottomMostPoint());
        }

        public void Triangulate(Triangulation tl, Triangulation tr)
        {
            baseEdge = findBaseEdge(tl, tr);
            this.AddEdge(baseEdge);
            this.AddLREdge(tl, tr, baseEdge);
            this.MergeTriangulation(tl, tr);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGUtilities
{
   public class PointWithAngle
    {
        public Point myPoint;
        public double myAngle;
        public PointWithAngle(Point p,double angl)
        {
            myPoint = p;
            myAngle = angl;
        }
    }
    public class HelperMethods
    {
        public static Enums.PointInPolygon PointInTriangle(Point p, Point a, Point b, Point c)
        {
            if (a.Equals(b) && b.Equals(c))
            {
                if (p.Equals(a) || p.Equals(b) || p.Equals(c))
                    return Enums.PointInPolygon.OnEdge;
                else
                    return Enums.PointInPolygon.Outside;
            }

            Line ab = new Line(a, b);
            Line bc = new Line(b, c);
            Line ca = new Line(c, a);

            if (GetVector(ab).Equals(Point.Identity)) return (PointOnSegment(p, ca.Start, ca.End)) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (GetVector(bc).Equals(Point.Identity)) return (PointOnSegment(p, ca.Start, ca.End)) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (GetVector(ca).Equals(Point.Identity)) return (PointOnSegment(p, ab.Start, ab.End)) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;

            if (CheckTurn(ab, p) == Enums.TurnType.Colinear)
                return PointOnSegment(p, a, b)? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (CheckTurn(bc, p) == Enums.TurnType.Colinear && PointOnSegment(p, b, c))
                return PointOnSegment(p, b, c) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;
            if (CheckTurn(ca, p) == Enums.TurnType.Colinear && PointOnSegment(p, c, a))
                return PointOnSegment(p, a, c) ? Enums.PointInPolygon.OnEdge : Enums.PointInPolygon.Outside;

            if (CheckTurn(ab, p) == CheckTurn(bc, p) && CheckTurn(bc, p) == CheckTurn(ca, p))
                return Enums.PointInPolygon.Inside;
            return Enums.PointInPolygon.Outside;
        }
        public static Enums.TurnType CheckTurn(Point vector1, Point vector2)
        {
            double result = CrossProduct(vector1, vector2);
            if (result < 0) return Enums.TurnType.Right;
            else if (result > 0) return Enums.TurnType.Left;
            else return Enums.TurnType.Colinear;
        }
        public static Point FindnextPointInStack(Stack<Point> myStack)
        {
            Point top = myStack.Pop();
            Point result = myStack.Peek();
            myStack.Push(top);
            return result;
        }
        public static double SquareDistance(Point p1, Point p2)
        {
            var squared = (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
            return squared;
        }
        /// Get turn type from cross product between two vectors (l.start -> l.end) and (l.end -> p)
        public static Enums.TurnType CheckTurn(Line l, Point p)
        {
            Point a = l.Start.Vector(l.End);
            Point b = l.End.Vector(p);
            return HelperMethods.CheckTurn(a, b);
        }
        public static Point GetVector(Line l)
        {
            return l.Start.Vector(l.End);
        }
        public static bool PointOnSegment(Point p, Point a, Point b)
        {
            if (a.Equals(b))
                return p.Equals(a);
            if (b.X == a.X)
                return p.X == a.X && (p.Y >= Math.Min(a.Y, b.Y) && p.Y <= Math.Max(a.Y, b.Y));
            if (b.Y == a.Y)
                return p.Y == a.Y && (p.X >= Math.Min(a.X, b.X) && p.X <= Math.Max(a.X, b.X));
            double tx = (p.X - a.X) / (b.X - a.X);
            double ty = (p.Y - a.Y) / (b.Y - a.Y);
            return (Math.Abs(tx - ty) <= Constants.Epsilon && tx <= 1 && tx >= 0);
        }
        // More Helper Functions 
        public static double DotProduct(Point a , Point b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }
        public static double CalculateAngle(Point a , Point b, Point c)
        {
            Point vecBA = b.Vector(a);
            Point vecBC = b.Vector(c);
            double crossProduct = CrossProduct(vecBA, vecBC);
            double dotProduct = DotProduct(vecBA, vecBC);
            double radianAngle = Math.Atan2(crossProduct, dotProduct);
            if(radianAngle<0)
            {
                radianAngle += Math.PI * 2;
            }
            double degreeAngle = radianAngle * 180 / Math.PI;
            return degreeAngle;
        }
        public static int compareTwoPointsByAngle(PointWithAngle p1,PointWithAngle p2)
        {
            if (p1.myAngle < p2.myAngle)
                return -1;
            else if (p1.myAngle > p2.myAngle)
                return 1;
            else
                return 0;
        }
        public static int getPrevIndex(int index,int n)
        {
            return (index - 1 + n) % n;
        }
        public static int getNextIndex(int index,int n)
        {
            return (index + 1) % n;
        }
        public static Point getCenterOfTriangle(Point a,Point b,Point c)
        {
            Point tmp=new Point((a.X+b.X)/2,(a.Y+b.Y)/2);
            Point Result=new Point((tmp.X+c.X)/2,(tmp.Y+c.Y)/2);
            return Result;
        }
        public static int Orientation(Point p,Point q,Point r)
        {
            var val = (q.Y - p.Y) * (r.X - q.X) -
             (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0;  // colinear
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }
        public static double CrossProduct(Point a, Point b)
        {
            return a.X * b.Y - a.Y * b.X;
        }
        public static bool PointOnRay(Point p, Point a, Point b)
        {
            if (a.Equals(b)) return true;
            if (a.Equals(p)) return true;
            var q = a.Vector(p).Normalize();
            var w = a.Vector(b).Normalize();
            return q.Equals(w);
        }
        public static double distOfPointFromSegment(Point a,Point b,Point p)
        {
            Point VectorAB = a.Vector(b);
            Point VectorAP = a.Vector(p);
            double crossProduct = CrossProduct(VectorAB, VectorAP);
            double DistanceAB = Math.Sqrt(((b.Y - a.Y) * (b.Y - a.Y)) + ((b.X - a.X) * (b.X - a.X)));
            double height = crossProduct / DistanceAB;
            return Math.Abs(height);
        }
    }
}

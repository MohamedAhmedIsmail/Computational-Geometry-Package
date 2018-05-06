using CGUtilities;
using CGUtilities.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class Incremental : Algorithm
    {
        Line baseLine;
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count < 3)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    outPoints.Add(points[i]);
                }
                return;
            }
            Point myCenterPoint = HelperMethods.getCenterOfTriangle(points[0], points[1], points[2]);
            baseLine = new Line(myCenterPoint, new Point(myCenterPoint.X + 50, myCenterPoint.Y));
            OrderedSet<Point> ConvexHull = new OrderedSet<Point>(new Comparison<Point>(compareByAngle));
            ConvexHull.Add(points[0]);
            ConvexHull.Add(points[1]);
            ConvexHull.Add(points[2]);
            for (int i = 3; i < points.Count; i++)
            {
                Point p = points[i];
                Point previousPoint = ConvexHull.DirectUpperAndLower(p).Key;
                Point nextPoint = ConvexHull.DirectUpperAndLower(p).Value;
                if(HelperMethods.CheckTurn(new Line(previousPoint,nextPoint),p)==Enums.TurnType.Right)
                {
                    Point newPrev = ConvexHull.DirectUpperAndLower(previousPoint).Key;
                    while(HelperMethods.CheckTurn(new Line(p,previousPoint),newPrev)==Enums.TurnType.Left)
                    {
                        ConvexHull.Remove(previousPoint);
                        previousPoint = newPrev;
                        newPrev = ConvexHull.DirectUpperAndLower(previousPoint).Key;
                    }
                    Point newNext = ConvexHull.DirectUpperAndLower(nextPoint).Value;
                    while(HelperMethods.CheckTurn(new Line(p,nextPoint),newNext)==Enums.TurnType.Right)
                    {
                        ConvexHull.Remove(nextPoint);
                        nextPoint = newNext;
                        newNext = ConvexHull.DirectUpperAndLower(nextPoint).Value;
                    }
                    ConvexHull.Add(p);
                }
            }
            outPoints.AddRange(ConvexHull);
        }
        public override string ToString()
        {
            return "Convex Hull - Incremental";
        }
        public int compareByAngle(Point p1,Point p2)
        {
            double angleP1 = HelperMethods.CalculateAngle(baseLine.End, baseLine.Start, p1);
            double angleP2 = HelperMethods.CalculateAngle(baseLine.End, baseLine.Start, p2);
            if (angleP1 < angleP2)
                return -1;
            else if (angleP1 > angleP2)
                return 1;
            else
                return 0;
        }
    }
}

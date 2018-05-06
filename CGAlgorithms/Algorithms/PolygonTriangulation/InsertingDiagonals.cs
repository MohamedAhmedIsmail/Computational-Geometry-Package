using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
using CGUtilities.DataStructures;
namespace CGAlgorithms.Algorithms.PolygonTriangulation
{
    class InsertingDiagonals : Algorithm
    {
        public bool isConvex(LinkedListNode<Point> pointIndex, LinkedList<Point> _polygon)
        {
            LinkedListNode<Point> prev = pointIndex.Previous;
            LinkedListNode<Point> next = pointIndex.Next;
            if (prev == null)
                prev = _polygon.Last;
            if (next == null)
                next = _polygon.First;
            Line line = new Line(prev.Value, pointIndex.Value);
            Enums.TurnType type = HelperMethods.CheckTurn(line, next.Value);
            if (type == Enums.TurnType.Left)
                return true;
            return false;
        }
        public bool polygonInCCW(List<Point> polygon)
        {
            // 1. Get Point with Min X
            double extMin = 1e9;
            int minPointIndex = -1;
            for (int i = 0; i < polygon.Count; ++i)
            {
                if ((polygon[i].X < extMin) || (polygon[i].X == extMin && polygon[i].Y < polygon[minPointIndex].Y))
                {
                    extMin = polygon[i].X;
                    minPointIndex = i;
                }
            }
            int prev, next;
            prev = HelperMethods.getPrevIndex(minPointIndex, polygon.Count);
            next = HelperMethods.getNextIndex(minPointIndex, polygon.Count);
            Line line = new Line(polygon[prev], polygon[next]);
            Enums.TurnType type = HelperMethods.CheckTurn(line, polygon[minPointIndex]);
            if (type == Enums.TurnType.Left)
                return false;
            else
                return true;
        }
        List<List<Point>> polygonPoints = new List<List<Point>>();
        List<Line> outputDiagonals = new List<Line>();   
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            for (int i = 0; i < polygons.Count; ++i)
            {
                List<Point> polygon = new List<Point>();
                for (int j = 0; j < polygons[i].lines.Count; ++j)
                {
                    Point start = polygons[i].lines[j].Start;
                    polygon.Add(start);
                }
                // If polygon drawn in clockwise direction, reverse the points
                bool isCCW = polygonInCCW(polygon);
                if (polygonInCCW(polygon) == false)
                {
                    polygon.Reverse();
                }
                polygonPoints.Add(polygon);
            }
            for (int p = 0; p < polygonPoints.Count; ++p)
                Inserting_Diagonals(polygonPoints[p]);

            outLines = outputDiagonals;
        }
        public override string ToString()
        {
            return "Inserting Diagonals";
        }
        public void Inserting_Diagonals(List<Point> polygon)
        {
            // 1) Get any convex point;
            // 2) Get farthest point from line (c.prev, c.next) inside the triangle (cprev, c, cnext)
            // 3) Get 2 subpolygons
            if (polygon.Count > 3)
            {
                LinkedList<Point> tempPolygon = new LinkedList<Point>();
                for (int i = 0; i < polygon.Count; ++i)
                    tempPolygon.AddLast(polygon[i]);
                LinkedListNode<Point> convexPoint = new LinkedListNode<Point>(tempPolygon.First.Value);
                for (LinkedListNode<Point> current = tempPolygon.First; current != tempPolygon.Last; current = current.Next)
                {
                    if (isConvex(current, tempPolygon))
                    {
                        convexPoint = current;
                        break;
                    }
                }
                LinkedListNode<Point> prev = convexPoint.Previous;
                LinkedListNode<Point> next = convexPoint.Next;
                if (prev == null)
                    prev = tempPolygon.Last;
                if (next == null)
                    next = tempPolygon.First;
                bool maxDistPointExist = false;
                double maxDist = 0;
                LinkedListNode<Point> maxDistPoint = new LinkedListNode<Point>(new Point(0, 0));
                for (LinkedListNode<Point> i = tempPolygon.First; i != null; i = i.Next)
                {
                    if (i != prev && i != next && i != convexPoint)
                    {
                        Enums.PointInPolygon isInTriangle = HelperMethods.PointInTriangle(i.Value, prev.Value, convexPoint.Value, next.Value);
                        if (isInTriangle.Equals(Enums.PointInPolygon.Inside) || isInTriangle.Equals(Enums.PointInPolygon.OnEdge))
                        {
                            maxDistPointExist = true;
                            if (HelperMethods.distOfPointFromSegment(prev.Value, next.Value, i.Value) > maxDist)
                            {
                                maxDist = HelperMethods.distOfPointFromSegment(prev.Value, next.Value, i.Value);
                                maxDistPoint = i;
                            }
                        }
                    }
                }
                if (maxDistPointExist == false)
                    outputDiagonals.Add(new Line(prev.Value, next.Value));
                else
                    outputDiagonals.Add(new Line(convexPoint.Value, maxDistPoint.Value));       
                List<Point> polygon1 = new List<Point>(), polygon2 = new List<Point>();
                if (maxDistPointExist == true)
                {
                    LinkedListNode<Point> index = convexPoint;
                    while (index != maxDistPoint)
                    {
                        LinkedListNode<Point> p = index;
                        polygon1.Add(p.Value);
                        index = index.Next;
                        if (index == null)
                            index = tempPolygon.First;
                    }
                    polygon1.Add(maxDistPoint.Value);

                    index = maxDistPoint;
                    while (index != convexPoint)
                    {
                        LinkedListNode<Point> p = index;
                        polygon2.Add(p.Value);
                        index = index.Next;
                        if (index == null)
                            index = tempPolygon.First;
                    }
                    polygon2.Add(convexPoint.Value);
                }
                else if (maxDistPointExist == false)
                {
                    tempPolygon.Remove(convexPoint);
                    for (LinkedListNode<Point> i = tempPolygon.First; i != null; i = i.Next)
                    {
                        polygon1.Add(i.Value);
                    }
                }
                Inserting_Diagonals(polygon1);
                Inserting_Diagonals(polygon2);
            }
        }
    }
}

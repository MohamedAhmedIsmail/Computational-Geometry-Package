using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms.PolygonTriangulation
{
    class SubtractingEars : Algorithm
    {
        public Line subtractingEars(Point p, List<Point> earPoints, List<Point> myPolygon)
        {
            earPoints.Remove(p);
            int earIndex = myPolygon.IndexOf(p);
            int earPrev, earNext;
            earPrev = HelperMethods.getPrevIndex(earIndex, myPolygon.Count);
            earNext = HelperMethods.getNextIndex(earIndex, myPolygon.Count);
            if (isEar(earPrev, myPolygon))
            {
                if (!earPoints.Contains(myPolygon[earPrev]))
                    earPoints.Add(myPolygon[earPrev]);
            }
            else
            {
                if (earPoints.Contains(myPolygon[earPrev]))
                    earPoints.Remove(myPolygon[earPrev]);
            }
            if (isEar(earNext, myPolygon))
            {
                if (!earPoints.Contains(myPolygon[earNext]))
                    earPoints.Add(myPolygon[earNext]);
            }
            else
            {
                if (earPoints.Contains(myPolygon[earNext]))
                    earPoints.Remove(myPolygon[earNext]);
            }
            return new Line(myPolygon[earPrev], myPolygon[earNext]);
        }
        public List<Point> FindAllEarPoints(List<Point> myPolygon)
        {
            List<Point> myEar = new List<Point>();
            for (int i = 0; i < myPolygon.Count; i++)
            {
                if (isEar(i, myPolygon))
                {
                    myEar.Add(myPolygon[i]);
                }
            }
            return myEar;
        }
        public bool polygonCCW(List<Point> myPolygon)
        {
            double mini = 1e9;
            int minPointIndex = -1;
            for (int i = 0; i < myPolygon.Count; i++)
            {
                if (myPolygon[i].X < mini || myPolygon[i].X == mini && myPolygon[i].Y < myPolygon[minPointIndex].Y)
                {
                    mini = myPolygon[i].X;
                    minPointIndex = i;
                }
            }
            int prev, next;
            prev = HelperMethods.getPrevIndex(minPointIndex, myPolygon.Count);
            next = HelperMethods.getNextIndex(minPointIndex, myPolygon.Count);
            Line line = new Line(myPolygon[prev], myPolygon[next]);
            Enums.TurnType type = HelperMethods.CheckTurn(line, myPolygon[minPointIndex]);
            if (type == Enums.TurnType.Left)
                return false;
            else
                return true;
        }
        public bool isConvex(int pointIndex, List<Point> myPolygon)
        {
            int prev, next;
            prev = HelperMethods.getPrevIndex(pointIndex, myPolygon.Count);
            next = HelperMethods.getNextIndex(pointIndex, myPolygon.Count);
            Line line = new Line(myPolygon[prev], myPolygon[pointIndex]);
            Enums.TurnType type = HelperMethods.CheckTurn(line, myPolygon[next]);
            if (type == Enums.TurnType.Left)
            {
                return true;
            }
            return false;
        }
        public bool isEar(int IndexPoint, List<Point> myPolygon)
        {
            if (!isConvex(IndexPoint, myPolygon))
            {
                return false;
            }
            int prev, next;
            prev = HelperMethods.getPrevIndex(IndexPoint, myPolygon.Count);
            next = HelperMethods.getNextIndex(IndexPoint, myPolygon.Count);
            for (int i = 0; i < myPolygon.Count; i++)
            {
                if (i != prev && i != IndexPoint && i != next)
                {
                    Enums.PointInPolygon checkTriangle = HelperMethods.PointInTriangle(myPolygon[i], myPolygon[prev], myPolygon[IndexPoint], myPolygon[next]);
                    if (checkTriangle == Enums.PointInPolygon.Inside || checkTriangle == Enums.PointInPolygon.OnEdge)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        List<List<Point>> polygonPoints = new List<List<Point>>();
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            for(int i=0;i<polygons.Count;i++)
            {
                List<Point> polygon = new List<Point>();
                for(int j=0;j<polygons[i].lines.Count;j++)
                {
                    Point start = polygons[i].lines[j].Start;
                    polygon.Add(start);
                }
                bool isCCW = polygonCCW(polygon);
                if(polygonCCW(polygon)==false)
                {
                    polygon.Reverse();
                }
                polygonPoints.Add(polygon);
            }
           for(int i=0;i<polygonPoints.Count;i++)
           {
               List<Point> Ears = FindAllEarPoints(polygonPoints[i]);
               while(Ears.Count!=0)
               {
                   Point ear = Ears[0];
                   Line internalDiagonal = subtractingEars(ear, Ears, polygonPoints[i]);
                   outLines.Add(internalDiagonal);
                   polygonPoints[i].Remove(ear);
                   if (polygonPoints[i].Count == 3)
                       break;
               }
           }
        }
        public override string ToString()
        {
            return "Subtracting Ears";
        }
    }
}

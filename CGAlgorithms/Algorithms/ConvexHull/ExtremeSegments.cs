using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Enums.TurnType checkTurns;
            int[] checkArr = new int[points.Count];
            int count = 0;
            Line myLine = new Line(new Point(0, 0), new Point(0, 0));
            if (points.Count == 1)
            {
                 outPoints.Add(points[0]);
                return;
            }
            for(int k=0;k<points.Count;k++)
            {
                for(int i=0;i<points.Count;i++)
                {
                    count = 0;
                    if (i == k)
                        continue;
                    for(int j=0;j<points.Count;j++)
                    {
                        if (i == j || k == j)
                            continue;
                        myLine.Start = points[k];
                        myLine.End = points[i];
                        checkTurns = HelperMethods.CheckTurn(myLine, points[j]);
                        if(checkTurns==Enums.TurnType.Left || checkTurns==Enums.TurnType.Colinear)
                        {
                            count++;
                        }
                    }
                    if(count==points.Count-2)
                    {
                        checkArr[k] = 1;
                        checkArr[i] = 1;
                    }
                }
            }
            for(int i=0;i<points.Count;i++)
            {
                if (checkArr[i] == 1)
                    outPoints.Add(points[i]);
            }   
         }
        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}

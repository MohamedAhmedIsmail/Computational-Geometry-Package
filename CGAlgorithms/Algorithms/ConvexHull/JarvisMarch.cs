using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if(points.Count<3)
            {
               for(int i=0;i<points.Count;i++)
               {
                   outPoints.Add(points[i]);
               }
               return;
            }
            List < Point > myPoints= new List<Point>();
            int leftMost = 0;
            for(int i=1;i<points.Count;i++)
            {
                if (points[i].X < points[leftMost].X)
                    leftMost = i;
            }
            int myLeftMostpoint = leftMost;
            int next;
            do
            {
                myPoints.Add(points[myLeftMostpoint]);
                next = (myLeftMostpoint + 1) % points.Count;
                for (int i = 0; i < points.Count; i++)
                {
                    if (i == myLeftMostpoint || i == next)
                        continue;
                    if (HelperMethods.Orientation(points[myLeftMostpoint], points[i], points[next]) == 2)
                        next = i;
                }
                myLeftMostpoint = next;
            } while (myLeftMostpoint != leftMost);
            for(int i =0;i<myPoints.Count;i++)
            {
                outPoints.Add(myPoints[i]);
            }
        }
        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}

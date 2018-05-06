using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremePoints : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            bool check = true;
            Enums.PointInPolygon myPoints;   
            for(int p=0;p<points.Count;p++)
            {
                check=true;
                Point myPoint1 = points[p];
                for(int i=0;i<points.Count;i++)
                {
                    if (i == p)
                        continue;
                    Point myPoint2=points[i];
                    for(int j=0;j<points.Count;j++)
                    {
                        if (i == j || j == p)
                            continue;
                        Point myPoint3 = points[j];
                        for(int k=0;k<points.Count;k++)
                        {
                            if (p == i || j == i || p == k)
                                continue;
                            Point myPoint4 = points[k];
                            myPoints=HelperMethods.PointInTriangle(myPoint1, myPoint2, myPoint3, myPoint4);
                            if(myPoints==Enums.PointInPolygon.Inside||myPoints==Enums.PointInPolygon.OnEdge)
                            {
                                check =false;
                            }               
                        }
                    }
                }
                if (check == true)
                    outPoints.Add(myPoint1);
            }
        }
        public override string ToString()
        {
            return "Convex Hull - Extreme Points";
        }
    }
}

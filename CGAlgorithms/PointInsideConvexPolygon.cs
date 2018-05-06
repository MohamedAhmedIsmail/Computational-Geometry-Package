using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class PointInsideConvexPolygon:Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Point myPoint = points[0];
            Polygon myPolygon = polygons[0];
            Enums.TurnType Check;
            int count = 0;
            for(int i=0;i<myPolygon.lines.Count;i++)
            {
                Check = HelperMethods.CheckTurn(myPolygon.lines[i], myPoint);
                if(Check==Enums.TurnType.Right)
                {
                    count++;    
                }
            }
            
            if(count==myPolygon.lines.Count)
            {
                outPoints.Add(myPoint);
            }
            else
            {
                for(int i=0;i<myPolygon.lines.Count;i++)
                {
                    outLines.Add(myPolygon.lines[i]);
                }
            }
        }
        public override string ToString()
        {
            return "PointInsideConvexPolygon";
        }
    }
}

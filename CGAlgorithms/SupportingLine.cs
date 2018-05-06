using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class SupportingLine:Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            int count = 0;
            Line l = lines[0];
            Enums.TurnType result;
            int count2 = 0;
            for (int i=0;i<points.Count;i++)
            {
                result = HelperMethods.CheckTurn(l, points[i]);
                if (result==Enums.TurnType.Left)
                {
                    count++;
                }
                else if (result == Enums.TurnType.Right)
                {
                    count2++;
                }
            }
            if(count==points.Count)
            {
                for(int i=0;i<points.Count;i++)
                {
                    outPoints.Add(points[i]);
                }
            }
            else if(count2==points.Count)
            {
                for(int i=0;i<points.Count;i++)
                {
                    outPoints.Add(points[i]);
                }
            }
            else
            {
                outLines.Add(l);
            }
        }
        public override string ToString()
        {
            return "Supporting Line";
        }
    }
}

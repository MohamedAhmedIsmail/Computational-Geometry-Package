using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class orientationTest:Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons,ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Point p=points[0];
            Line l =lines[0];
            Enums.TurnType result;
            result = HelperMethods.CheckTurn(l, p);
            if (result==Enums.TurnType.Left)
            {
                outPoints.Add(p);
            }
            else if(result==Enums.TurnType.Right)
            {
                outLines.Add(l);
            }
            else if(result==Enums.TurnType.Colinear)
            {
                outPoints.Add(p);
                outLines.Add(l);
            }
            
        }
        public override string ToString()
        {
            return "Orientation Test";
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class TangentLineToConvexPolygon:Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Polygon myPolygon = polygons[0];
            Line myLine = lines[0];
            Enums.TurnType checkStartPoint;
            Enums.TurnType checkEndPoint;
            int count = 0;
            for(int i=0;i<myPolygon.lines.Count;i++)
            {
                checkStartPoint = HelperMethods.CheckTurn(myLine, myPolygon.lines[i].Start);
                checkEndPoint = HelperMethods.CheckTurn(myLine, myPolygon.lines[i].End);
                if(checkStartPoint==checkEndPoint)
                {
                    count++;
                }
            }
            if(count!=myPolygon.lines.Count)
            {
                outLines.Add(myLine);
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
            return "TangentLineConvexPolygon";
        }
    }
}

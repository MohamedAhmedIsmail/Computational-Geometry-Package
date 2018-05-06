using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class SegmentSegmentIntersection:Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Line myLine1 = lines[0];
            Line myLine2 = lines[1];
            Enums.TurnType myIntersectedSegmentsFirstLine1;
            Enums.TurnType myIntersectedSegmentsFirstLine2;
            Enums.TurnType myIntersectedSegmentsSecondLine1;
            Enums.TurnType myIntersectedSegmentsSecondLine2;
            myIntersectedSegmentsFirstLine1 = HelperMethods.CheckTurn(myLine2, myLine1.Start);
            myIntersectedSegmentsFirstLine2 = HelperMethods.CheckTurn(myLine2, myLine1.End);
            myIntersectedSegmentsSecondLine1 = HelperMethods.CheckTurn(myLine1, myLine2.Start);
            myIntersectedSegmentsSecondLine2 = HelperMethods.CheckTurn(myLine1, myLine2.End);
            if(myIntersectedSegmentsFirstLine1!=myIntersectedSegmentsSecondLine1&&myIntersectedSegmentsFirstLine2!=myIntersectedSegmentsSecondLine2)
            {
                outLines.Add(myLine1);
                outLines.Add(myLine2);
            }
            else
            {
                outLines.Add(myLine1);
            }
        }
        public override string ToString()
        {
            return "SegmentSegmentIntersection";
        }
    }
}

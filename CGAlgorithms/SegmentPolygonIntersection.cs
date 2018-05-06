using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class SegmentPolygonIntersection:Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {

            Polygon myPolygon = polygons[0];
            Line myLine = lines[0];
            Enums.TurnType intersectStartLine;
            Enums.TurnType intersectEndLine;
            Enums.TurnType intersectStartLinePolygon;
            Enums.TurnType intersectEndLinePolygon;
            for (int i = 0; i < myPolygon.lines.Count;i++ )
            {
                intersectStartLine = HelperMethods.CheckTurn(myPolygon.lines[i], myLine.Start);
                intersectEndLine = HelperMethods.CheckTurn(myPolygon.lines[i], myLine.End);
                intersectStartLinePolygon = HelperMethods.CheckTurn(myLine, myPolygon.lines[i].Start);
                intersectEndLinePolygon = HelperMethods.CheckTurn(myLine, myPolygon.lines[i].End);
                if((intersectStartLine!=intersectStartLinePolygon)&&(intersectEndLine!=intersectEndLinePolygon))
                {
                    outLines.Add(myLine);
                    outLines.Add(myPolygon.lines[i]);
                }
                else
                {
                    outLines.Add(myLine);
                }
            }
        }
        public override string ToString()
        {
            return "SegmentPolygonIntersection";
        }
    }
}

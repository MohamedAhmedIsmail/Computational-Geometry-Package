using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class PolygonPolygonIntersection:Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            Polygon myFirstPolygon = polygons[0];
            Polygon mySecondPolygon = polygons[1];
            Enums.TurnType myFirstPolygonStartLine;
            Enums.TurnType myFirstPolygonEndLine;
            Enums.TurnType mySecondPolygonStartLine;
            Enums.TurnType mySecondPolygonEndLine;
            bool accepted = false;
            for(int i=0;i<myFirstPolygon.lines.Count;i++)
            {
                for(int j=0;j<mySecondPolygon.lines.Count;j++)
                {
                    myFirstPolygonStartLine = HelperMethods.CheckTurn(mySecondPolygon.lines[j], myFirstPolygon.lines[i].Start);
                    myFirstPolygonEndLine = HelperMethods.CheckTurn(mySecondPolygon.lines[j], myFirstPolygon.lines[i].End);
                    mySecondPolygonStartLine = HelperMethods.CheckTurn(myFirstPolygon.lines[i], mySecondPolygon.lines[j].Start);
                    mySecondPolygonEndLine = HelperMethods.CheckTurn(myFirstPolygon.lines[i], mySecondPolygon.lines[j].End);
                    if(myFirstPolygonStartLine!=myFirstPolygonEndLine&&mySecondPolygonStartLine!=mySecondPolygonEndLine)
                    {
                        outLines.Add(myFirstPolygon.lines[i]);
                        outLines.Add(mySecondPolygon.lines[j]);
                        accepted = true;
                        break;
                    }
                }
            }
            if (accepted == false)
            {
                for (int i = 0; i < myFirstPolygon.lines.Count; i++)
                {
                    outLines.Add(myFirstPolygon.lines[i]);
                }
            }
        }
        public override string ToString()
        {
            return "PolygonPolygonIntersection";
        }
    }
}

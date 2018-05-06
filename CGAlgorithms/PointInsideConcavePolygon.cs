using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms
{
    class PointInsideConcavePolygon:Algorithm
    {
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            Polygon myPolygon = polygons[0];
            Point myPoint = points[0];
            Line myLine = new Line(new Point(0,0),new Point(0,0));
            myLine.Start.X = myPoint.X;
            myLine.Start.Y = myPoint.Y;
            double myMinX = 0.0;
            Enums.TurnType intersectStartLine;
            Enums.TurnType intersectEndLine;
            Enums.TurnType intersectStartLinePolygon;
            Enums.TurnType intersectEndLinePolygon;
            long  myCounter = 0;
            for(int i=0;i<myPolygon.lines.Count;i++)
            {
                myMinX = Math.Min(myPolygon.lines[i].Start.X, myPoint.X);
            }
            myLine.End.X = myMinX;
            myLine.End.Y = myPoint.Y;
            for(int i=0;i<myPolygon.lines.Count;i++)
            {
                intersectStartLine = HelperMethods.CheckTurn(myPolygon.lines[i], myLine.Start);
                intersectEndLine = HelperMethods.CheckTurn(myPolygon.lines[i], myLine.End);
                intersectStartLinePolygon = HelperMethods.CheckTurn(myLine, myPolygon.lines[i].Start);
                intersectEndLinePolygon = HelperMethods.CheckTurn(myLine, myPolygon.lines[i].End);
                if ((intersectStartLine != intersectEndLine) && (intersectStartLinePolygon != intersectEndLinePolygon))
                {
                    myCounter++;
                }
            }
            if(myCounter%2==1)
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
 	        return "PointInsideConcavePolygon";
        }
    }
}

using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities.DataStructures;
namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            //1- get minimum the point Y
            //2- get angles between Horizontal Line and all points put it in List<pair> with their index
            //3- sort points put points in list then sort 
            //4- create stack and push points p0,p1
            //5- loop i:2->n top,preTop
            // if pretop.top turn left with pi then push pi in stack then i++
            // else pop from stack
            // complexity O(nlogn)
            List<Point> myPoints = new List<Point>();
            for (int i = 0; i < points.Count;i++ )
            {
                if(!myPoints.Contains(points[i]))
                {
                    myPoints.Add(points[i]);
                }
            }
                if (myPoints.Count < 3)
                {
                    for (int i = 0; i < myPoints.Count; i++)
                    {
                        outPoints.Add(myPoints[i]);
                    }
                    return;
                }
            Point myMinimumPoint = new Point(0,0);
            var yminimumIndex =-1;
            double minimum = 1e9;
            for(int i=0;i<myPoints.Count;i++)
            {
                var y = myPoints[i].Y;
                if((y<minimum) || (y==minimum && myPoints[i].X <myMinimumPoint.X))
                {
                    minimum = y;
                    yminimumIndex = i;
                    myMinimumPoint = myPoints[i];
                }
            }
            myPoints[yminimumIndex] = myPoints[0];
            myPoints[0] = myMinimumPoint;
            OrderedSet<PointWithAngle> pointsSorted = new OrderedSet<PointWithAngle>(new Comparison<PointWithAngle>(HelperMethods.compareTwoPointsByAngle));
            pointsSorted.Add(new PointWithAngle(myPoints[0], 0));
            for(int i=1;i<myPoints.Count;i++)
            {
                Point temp = new Point(myPoints[0].X + 10, myPoints[0].Y);
                double myAngle = HelperMethods.CalculateAngle(temp, myPoints[0], myPoints[i]);
                pointsSorted.Add(new PointWithAngle(myPoints[i], myAngle));
            }
            Stack<Point> myStack = new Stack<Point>();
            for (int i = 0; i < 3;i++ )
            {
                myStack.Push(pointsSorted[i].myPoint);
            }
            for(int i=3;i<pointsSorted.Count;i++)
            {
                Point top = myStack.Peek();
                Point nextToTop = HelperMethods.FindnextPointInStack(myStack);
                while(HelperMethods.CheckTurn(new Line(nextToTop,top),pointsSorted[i].myPoint)!=Enums.TurnType.Left)
                {
                    myStack.Pop();
                    top = myStack.Peek();
                    nextToTop = HelperMethods.FindnextPointInStack(myStack);
                }
                myStack.Push(pointsSorted[i].myPoint);
            }
            while(myStack.Count!=0)
            {
                outPoints.Add(myStack.Pop());
            }
        }
        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}

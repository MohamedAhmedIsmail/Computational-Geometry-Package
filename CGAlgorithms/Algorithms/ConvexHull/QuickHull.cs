using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class QuickHull : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            //1-Recursive Algorithm
            //2- minimum X and maximum X then create line of them
            //3- creat a list contains all left points
            //4- create a list contains all right points
            //5-make a QuickHullRecursive function, stopping condition if ps.count=0 return empty List
            //5.1- get max point=furthest point from L
            //5-1 get first equation of first line:y=m1x+c   m2=-1/m1
            //   y=m2x+c solve the two equations the we get c the substitue to get the x
            // 5-1-1 get the cross Product AxB=2 delta abx
            //       AxB = 2*0.5 hab
            //      h=AxB/ab
            //6- L1=(list.start , max Distance)
            //   L2=(max Distance , line.End)
            //7- Lp1= points to the left of L1
            //8- Lp2= points to the left of L2
            //9- R1=QHRec(L1,Lp1)
            //   R2=QHRec(L2,Lp2)
            //  R=R1+R2 + MaxD
            // 10- in my Run
            //    R1=QHRec(L,LeftPpoints)
            //    R2=QHRec(L.reverse,RightPoints)
            //    R= R1+R2+minX+maxX
        }

        public override string ToString()
        {
            return "Convex Hull - Quick Hull";
        }
    }
}

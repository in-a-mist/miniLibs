using Rhino.Collections;
using Rhino.Geometry;
using System;

namespace miniLibs
{
    public class Utils
    {
        //此方法过于复杂，暂且如是
        public static Brep CreateGY(Point3d ptStart, Point3d ptEnd, double offset,double unitValue)
        {

            Point3d ptA = new Point3d(ptStart.X + 2 * unitValue, ptStart.Y, ptStart.Z + 4 * unitValue);
            Point3d ptB = new Point3d(ptStart.X + 2 * unitValue, ptStart.Y, ptStart.Z + 6 * unitValue);
            Point3d ptC = new Point3d(ptEnd.X - 2 * unitValue, ptEnd.Y, ptEnd.Z + 6 * unitValue);
            Point3d ptD = new Point3d(ptEnd.X - 2 * unitValue, ptEnd.Y, ptEnd.Z + 4 * unitValue);

            Point3d ptE = new Point3d(ptStart.X, ptStart.Y, ptStart.Z - 3 * unitValue);
            Point3d ptF = new Point3d(ptStart.X + 3 * unitValue, ptStart.Y, ptStart.Z - 3 * unitValue);
            Point3d ptG = new Point3d(ptEnd.X - 3 * unitValue, ptEnd.Y, ptEnd.Z - 3 * unitValue);

            //
            Point3dList topPts = new Point3dList(ptA, ptB, ptC, ptD);
            Curve topLine = new PolylineCurve(topPts);

            //
            PolylineCurve crvToA = new PolylineCurve(new Point3dList(ptStart, ptA));
            Curve offsetCrvToA = crvToA.Offset(Plane.WorldZX, 0.5 * unitValue, 0.1, CurveOffsetCornerStyle.None)[0];
            Point3d midPoint2 = offsetCrvToA.PointAt(0.5);
            Arc arcLeft = new Arc(ptStart, midPoint2, ptA);

            Arc arcRight = new Arc(ptStart, midPoint2, ptA);
            Point3d rotatePt = new Point3d((ptStart.X + ptEnd.X) / 2.0, (ptStart.Y + ptEnd.Y) / 2.0, (ptStart.Z + ptEnd.Z) / 2.0);
            Plane roPlane = new Plane(rotatePt, Vector3d.ZAxis);
            arcRight.Transform(Transform.Rotation(Math.PI, roPlane.ZAxis, rotatePt));

            //
            Point3dList downPts = new Point3dList(ptStart, ptE, ptF);
            Curve downLeftArc = Curve.CreateControlPointCurve(downPts, 3);

            Curve downRightArc = Curve.CreateControlPointCurve(downPts, 3);
            downRightArc.Transform(Transform.Rotation(Math.PI, roPlane.ZAxis, rotatePt));

            //
            Curve crvDwon = new LineCurve(ptF, ptG);

            //
            CurveList allCrvs = new CurveList() { topLine, arcLeft, arcRight, downLeftArc, downRightArc, crvDwon };
            Curve sectionCurve = Curve.JoinCurves(allCrvs, 0.1)[0];
            Brep singleSrf = Brep.CreatePlanarBreps(sectionCurve, 0.1)[0];
            Brep solid = Brep.CreateFromOffsetFace(singleSrf.Faces[0], offset, 0.1, true, true);

            return solid;


        }
        public static Brep CreateCutSolid(double cutFullLength,double cutHeight, double cutAmount )
        {
            //两端卷杀,
            CurveList jsCurves = new CurveList();
            double oneH = cutHeight / cutAmount;
            double cutEachLength = cutFullLength / cutAmount;

            for (int i = 1; i <= cutAmount; i++)
            {

                Point3d ptA = Point3d.Origin;
                Point3d ptB = new Point3d(cutEachLength * i, 0, 0);
                Point3d ptC = new Point3d(0, 0, (cutAmount + 1 - i) * oneH);
                Point3dList points = new Point3dList() { ptA, ptB, ptC, ptA };

                PolylineCurve polyLineCrv = new PolylineCurve(points);
                jsCurves.Add(polyLineCrv);

            }

            //Vector3d vec01 = new Vector3d(0.5 * outDouLength, 0, 0);
            //Vector3d vec02 = new Vector3d(insideLength + 0.5 * insDouLength, 0, 0);

            Curve edge = Curve.CreateBooleanUnion(jsCurves, 0.1)[0];
            Brep srfs = Brep.CreatePlanarBreps(edge,Utils.GetTolerance)[0];
            //jsEdge01.Translate(vec01);

            //Curve jsEdge02 = Curve.CreateBooleanUnion(jsCurves, 0.1)[0];
            //jsEdge02.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);
            //jsEdge02.Translate(vec02);

            //Curve step01 = Curve.CreateBooleanDifference(recCrv, jsEdge01, 0.1)[0];
            //Curve step02 = Curve.CreateBooleanDifference(step01, jsEdge02, 0.1)[0];

            return srfs;
        }
        public static Rectangle3d CreateRec(double recLenght, double recWidth, Point3d planePositon)
        {
            Plane plane = Plane.WorldXY;
            plane.Translate(new Vector3d(planePositon));
            Interval len = new Interval(-0.5 * recLenght, 0.5 * recLenght);
            Interval wide = new Interval(-0.5 * recWidth, 0.5 * recWidth);
            Rectangle3d rec = new Rectangle3d(plane, len, wide);
            return rec;
        }
        public static Brep CreateBox(double boxLenght, double boxWidth, double boxHeight, Point3d planePositon)
        {
            Curve rec = Utils.CreateRec(boxLenght, boxWidth, planePositon).ToNurbsCurve();
            Vector3d vec = new Vector3d(0, 0,boxHeight);
            Brep srfs = Surface.CreateExtrusion(rec, vec).ToBrep();

            Brep solid = srfs.CapPlanarHoles(Utils.GetTolerance);
            solid.Faces.SplitKinkyFaces();
            return solid;
        }
        public static double GetTolerance => Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
    }
}

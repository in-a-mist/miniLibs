using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Fangs
{
    /// <summary>
    /// 箍头枋
    /// </summary>
    class FangLockOnHead : FangOuter
    {
        public FangLockOnHead(double length) : base(length)
        {
        }

        public Brep Create()
        {


            Brep step01 = PrimitiveSolid(Length);
            Brep endStyle = EndStyle();
            Brep[] breps1 = { step01, endStyle };
            Brep halfBrep = Brep.CreateBooleanUnion(breps1, DocTolerance.ModelToler)[0];

            //镜像合并
            Brep step02 = halfBrep.DuplicateBrep();
            step02.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);
            step02.Translate(0, Length, 0);

            Brep[] breps = { halfBrep, step02 };
            Brep step03 = Brep.CreateBooleanUnion(breps, DocTolerance.ModelToler)[0];
            step03.Rotate(-0.5*Math.PI, Vector3d.ZAxis, Point3d.Origin);

            //替换端头
            Brep endbrep = DecorateEnd();
            //Endbrep02.Translate(0, 0, -Height);

            Brep[] breps03 = { step03, endbrep };
            Brep step04 = Brep.CreateBooleanUnion(breps03, DocTolerance.ModelToler)[0];

            //
            Brep result = step04;
            result.MergeCoplanarFaces(DocTolerance.ModelToler);

            if (null != Position)
            {
                result.Translate(new Vector3d(Position));
                result.Translate(0, 0, -settings.ColumnDiameter);
            }

            return result;
        }

        protected Brep DecorateEnd()
        {

            //燕尾榫
            Brep sub01 = CommonModel.SwallowtailTenon(0.25 * settings.ColumnDiameter, 0.25 * settings.ColumnDiameter, settings.ColumnDiameter, Height);
            sub01.Rotate(Math.PI * 0.5, Vector3d.ZAxis, Point3d.Origin);
            sub01.Translate(0, 0, Height);

            //
            Brep brep = CommonModel.BoxBrep(1.5 * settings.ColumnDiameter, 0.8 * Width, Height);
            brep.Translate(-1 * settings.ColumnDiameter, 0, 0);

            Brep subSrf = CommonModel.RollingEnd(0.8 * Width,settings.ColumnDiameter, settings.ColumnHeight);
            Brep sub02 = Brep.CreateFromOffsetFace(subSrf.Faces[0], Height, DocTolerance.ModelToler, true, true);
            Brep step01 = Brep.CreateBooleanDifference(brep, sub02, DocTolerance.ModelToler)[0];

            //三叉头折线
            double D = settings.ColumnDiameter;
            Point3d pA = new Point3d(-D, 0, 0);
            Point3d pB = new Point3d(-D, 0, Height);
            Point3d pC = new Point3d(0.25*D, 0, Height);
            Point3d pD = new Point3d(0, 0, 0.66*Height);
            Point3d pE = new Point3d(0.15 * D, 0, 0.33*Height);
            Point3d pF = new Point3d(0.65 * D, 0, 0);
            Point3d[] points = { pA, pB, pC, pD, pE, pF, pA };
            Curve polycurve = Curve.CreateInterpolatedCurve(points, 1);
            Brep threeSrf = Brep.CreatePlanarBreps(polycurve,DocTolerance.ModelToler)[0];
            Brep threeSub = Brep.CreateFromOffsetFace(threeSrf.Faces[0], Width, DocTolerance.ModelToler, true, true);
            threeSub.Translate(-1.75 * settings.ColumnDiameter, 0, 0);

            Brep step02 = Brep.CreateBooleanDifference(step01, threeSub, DocTolerance.ModelToler)[0];

            //
            Brep[] breps02 = { sub01, step02 };
            Brep step03 = Brep.CreateBooleanUnion(breps02, DocTolerance.ModelToler)[0];


            return step03;
        }
    }
}

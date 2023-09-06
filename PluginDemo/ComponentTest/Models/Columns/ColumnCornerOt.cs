using System;
using ComponentTest.Models.Columns;
using ComponentTest.Models.Utils;
using Rhino.Geometry;

namespace ComponentTest.Models.Columns
{
    public class ColumnCornerOt : ColumnBase
    {
        public ColumnCornerOt(double diameter, double height, double shrinkPercent = 0.01) : base(diameter, height, shrinkPercent)
        {
        }

        public Brep Create()
        {
            Brep step01 = PrimitiveSolid();

            //透榫
            Brep box01 = CommonModel.BoxBrep(0.25 * Diameter, 2.0 * Diameter, 0.5 * Diameter);
            box01.Translate(0, 0, Height - 2 * Diameter);

            //半榫
            Brep box02 = CommonModel.BoxBrep(0.25 * Diameter, 0.8 * Diameter, Diameter);
            box02.Translate(0, 0.5 * Diameter, Height - 2 * Diameter);

            Brep step02 = Brep.CreateBooleanDifference(step01, box01, DocTolerance.ModelToler)[0];
            Brep step03 = Brep.CreateBooleanDifference(step02, box02, DocTolerance.ModelToler)[0];

            //
            Brep bunTenon = CommonModel.BunTenon(0.33 * Diameter, 0.33 * Diameter);
            bunTenon.Translate(0, 0, Height);

            Brep step04 = Brep.CreateBooleanDifference(step03, bunTenon, DocTolerance.ModelToler)[0];


            //一字型开口
            Brep sub01 = CommonModel.SwallowtailTenon(0.25 * Diameter, 0.25 * Diameter, Diameter, Diameter);
            sub01.Translate(0, 0, Height);
            sub01.Rotate(Math.PI * 0.5, Vector3d.ZAxis, Point3d.Origin);

            Brep step05 = Brep.CreateBooleanDifference(step04, sub01, DocTolerance.ModelToler)[0];


            //




            //
            Brep result = step05;
            result.MergeCoplanarFaces(DocTolerance.ModelToler);

            if (null != Position)
            {
                result.Translate(new Vector3d(Position));
            }

            return result;
        }
    }
}

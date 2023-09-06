using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;

namespace ComponentTest.Models.Columns
{
    public class ColumnInner:ColumnBase
    {
        
        public ColumnInner(double diameter, double height)
            : base(diameter, height)
        {
            var instance = GlobalSettings.GetInstance();
            Height = instance.ColumnHeight + 0.5 * instance.DistanceOuterSpan;
            Diameter = 1.1*instance.ColumnDiameter;
        }

        public Brep Create()
        {
            double outerHeight = GlobalSettings.GetInstance().ColumnHeight;

            
            Brep step01 = PrimitiveSolid();

            //透榫
            Brep box01 = CommonModel.BoxBrep(0.25 * Diameter, 2.0 * Diameter, 0.5 * GlobalSettings.GetInstance().ColumnDiameter);
            box01.Translate(0, 0, outerHeight - 2 * GlobalSettings.GetInstance().ColumnDiameter);

            //半榫
            Brep box02 = CommonModel.BoxBrep(0.25 * Diameter, 0.8 * Diameter, GlobalSettings.GetInstance().ColumnDiameter);
            box02.Translate(0, -0.5 * Diameter, outerHeight - 2 * GlobalSettings.GetInstance().ColumnDiameter);

            Brep step02 = Brep.CreateBooleanDifference(step01, box01, DocTolerance.ModelToler)[0];
            Brep step03 = Brep.CreateBooleanDifference(step02, box02, DocTolerance.ModelToler)[0];

            //抱头梁尾端半榫入柱
            Beams.BeamsData data = new Beams.BeamsData(Beams.BeamsType.OneSpan);
            Brep box03 = CommonModel.BoxBrep(0.25 * data.Width, 0.6 * Diameter, data.Height);
            box03.Translate(0, -0.5 * Diameter, outerHeight);

            Brep step04 = Brep.CreateBooleanDifference(step03, box03, DocTolerance.ModelToler)[0];

            //燕尾榫
            Brep sub01 = CommonModel.SwallowtailTenon(0.2 * Diameter, 0.25 * Diameter, 0.3 * Diameter, Diameter);
            sub01.Translate(0, 0.35 * Diameter, Height);
            //sub01.Rotate(Math.PI * 0.5, Vector3d.ZAxis, Point3d.Origin);
            Brep step05 = Brep.CreateBooleanDifference(step04, sub01, DocTolerance.ModelToler)[0];




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

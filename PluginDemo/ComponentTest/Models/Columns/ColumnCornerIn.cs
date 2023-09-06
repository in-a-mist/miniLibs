using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Columns
{
    public class ColumnCornerIn : ColumnBase
    {
        public ColumnCornerIn(double diameter, double height, double shrinkPercent = 0.01) : base(diameter, height, shrinkPercent)
        {
            var settings = GlobalSettings.GetInstance();
            Height = settings.ColumnHeight + 0.5 * settings.DistanceOuterSpan;
            Diameter = settings.ColumnDiameter + 0.1 * settings.ScaleRule;
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
            Brep bunTenon = CommonModel.BunTenon(0.33 * Diameter, 0.33 * Diameter);
            bunTenon.Translate(0, 0, Height);

            Brep step06 = Brep.CreateBooleanDifference(step05, bunTenon, DocTolerance.ModelToler)[0];


            //一字型开口
            Brep sub001 = CommonModel.SwallowtailTenon(0.25 * Diameter, 0.25 * Diameter, Diameter, Diameter);
            sub001.Translate(0, 0, Height);
            sub001.Rotate(Math.PI * 0.5, Vector3d.ZAxis, Point3d.Origin);

            Brep step07 = Brep.CreateBooleanDifference(step06, sub001, DocTolerance.ModelToler)[0];

            //
            Brep result = step07;
            result.MergeCoplanarFaces(DocTolerance.ModelToler);

            if (null != Position)
            {
                result.Translate(new Vector3d(Position));
            }

            return result;
        }
    }
}

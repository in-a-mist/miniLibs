using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;

namespace ComponentTest.Models.Columns
{
    public class ColumnOuter:ColumnBase
    {
        public ColumnOuter(double diameter,double height)
            :base(diameter, height)
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
            Brep result = step03;
            result.MergeCoplanarFaces(DocTolerance.ModelToler);

            if (null != Position)
            {
                result.Translate(new Vector3d(Position));
            }

            return result;
        }
    }
}

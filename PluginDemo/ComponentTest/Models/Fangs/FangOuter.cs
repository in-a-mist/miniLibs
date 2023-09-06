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
    /// 檐枋
    /// </summary>
    public class FangOuter:FangInsertIn
    {
        public FangOuter(double length)
        {
            data = new FangsData(FangsType.OutterInner);
            Height = data.Height;
            Width = data.Width;
            Length =length;
        }

        public override Brep Create(double length)
        {
            Brep step01 = PrimitiveSolid(Length);
            Brep endStyle = EndStyle();
            Brep[] breps1 = { step01, endStyle };
            Brep halfBrep = Brep.CreateBooleanUnion(breps1, DocTolerance.ModelToler)[0];

            //镜像合并
            Brep step02 = halfBrep.DuplicateBrep();
            step02.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);
            step02.Translate(0, Length, 0);

            //
            Brep[] breps = { halfBrep, step02 };
            Brep step03 = Brep.CreateBooleanUnion(breps, DocTolerance.ModelToler)[0];
            step03.Rotate(-0.5*Math.PI, Vector3d.ZAxis, Point3d.Origin);

            //
            Brep result = step03;
            result.MergeCoplanarFaces(DocTolerance.ModelToler);

            if (null != Position)
            {
                result.Translate(new Vector3d(Position));
                result.Translate(0, 0, -settings.ColumnDiameter);
            }

            return result;
        }

        protected override Brep EndStyle()
        { 
            //燕尾榫
            Brep sub01 = CommonModel.SwallowtailTenon(0.2 * settings.ColumnDiameter, 0.25 * settings.ColumnDiameter, 0.3 * settings.ColumnDiameter, Height);
            sub01.Translate(0, 0.35 * settings.ColumnDiameter, Height);
            //sub01.Rotate(Math.PI * 0.5, Vector3d.ZAxis, Point3d.Origin); CommonModel.SwallowtailTenon

            return sub01;
        }
    }
}

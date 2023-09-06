using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Beams
{
    /// <summary>
    /// 抱头梁
    /// </summary>
    public class BeamOnHead : BeamBase
    {
        public BeamOnHead()
        {
            BeamsData beamsData = new BeamsData(BeamsType.OneSpan);
            Height = beamsData.Height;
            Length = beamsData.Length;
            Width = beamsData.Width;
        }

        public Brep Create()
        {
            Brep oringin = PrimitiveSolid(Length, Width, Height);
            Brep bunTenon = CommonModel.BunTenon(0.33 * settings.ColumnDiameter, 0.33 * settings.ColumnDiameter);

            Brep step01 = Brep.CreateBooleanDifference(oringin, bunTenon, DocTolerance.ModelToler)[0];

            //撞一回二
            Brep subSrf = CommonModel.RollingEnd(Width, settings.ColumnDiameter,settings.ColumnHeight);
            Brep sub01 = Brep.CreateFromOffsetFace(subSrf.Faces[0], Height, DocTolerance.ModelToler, true, true);
            sub01.Rotate(0.5*Math.PI, Vector3d.ZAxis, Point3d.Origin);
            sub01.Translate(0, Length - settings.ColumnDiameter, 0);

            Brep step02 = Brep.CreateBooleanDifference(step01, sub01, DocTolerance.ModelToler)[0];

            //半榫
            Brep box02 = CommonModel.BoxBrep(0.25 * Width, 0.6 * settings.ColumnDiameter, Height);
            box02.Translate(0, Length - 1.5* settings.ColumnDiameter, 0);

            Brep[] solids = { step02, box02 };
            Brep step03 = Brep.CreateBooleanUnion(solids, DocTolerance.ModelToler)[0];


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

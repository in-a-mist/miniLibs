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
    /// 三架梁
    /// </summary>
    public class BeamThreeSpan:BeamBase
    {
        public BeamThreeSpan()
        {
            BeamsData beamsData = new BeamsData(BeamsType.ThreeSpan);
            Height = beamsData.Height;
            Length = beamsData.Length;
            Width = beamsData.Width;
        }

        public Brep Create()
        {
            Brep oringin = PrimitiveSolid(0.5 * Length, Width, Height);
            Brep bunTenon = CommonModel.BunTenon(0.33 * 1.1 * settings.ColumnDiameter, 0.33 * 1.1 * settings.ColumnDiameter);

            Brep step01 = Brep.CreateBooleanDifference(oringin, bunTenon, DocTolerance.ModelToler)[0];

            //添加瓜柱卯眼、且略


            //
            Brep step02 = step01.DuplicateBrep();
            step02.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);
            step02.Translate(0, Length - 2 * settings.ColumnDiameter, 0);

            //
            Brep[] breps = { step01, step02 };
            Brep step03 = Brep.CreateBooleanUnion(breps, DocTolerance.ModelToler)[0];

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

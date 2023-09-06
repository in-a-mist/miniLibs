using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Fangs
{

    public class FangInsertIn
    {
        protected FangsData data;
        protected GlobalSettings settings = GlobalSettings.GetInstance();
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Point3d Position { get; set; }
        /// <summary>
        /// 顶部定位点
        /// </summary>
        public Point3d GetVertex()
        {
            Point3d point = new Point3d(Position.X, Position.Y, Position.Z + Height);
            return point;
        }
        public FangInsertIn()
        {
            data = new FangsData(FangsType.InsertIn);
            Height = data.Height;
            Width = data.Width;
            Length = data.Length;
        }

        protected Brep PrimitiveSolid(double length)
        {
            double hailfLength = 0.5 * length;

            Brep box00 = CommonModel.BoxBrep(Width, hailfLength, Height);
            box00.Translate(0, 0.5 * hailfLength, 0);

            Brep subSrf = CommonModel.RollingEnd(Width, settings.ColumnDiameter, settings.ColumnHeight - this.Height);
            Brep sub01 = Brep.CreateFromOffsetFace(subSrf.Faces[0], Height, DocTolerance.ModelToler, true, true);
            sub01.Rotate(-0.5 * Math.PI, Vector3d.ZAxis, Point3d.Origin);

            Brep step01 = Brep.CreateBooleanDifference(box00, sub01, DocTolerance.ModelToler)[0];

            return step01;
        }

        protected virtual Brep EndStyle()
        {
            //端头样式*****************************
            //透榫
            double Diameter = settings.ColumnDiameter;
            Brep box01 = CommonModel.BoxBrep(0.25 * Diameter, 2.0 * Diameter, 0.5 * Diameter);
            //box01.Translate(0, 0, Height - 2 * Diameter);

            //半榫
            Brep box02 = CommonModel.BoxBrep(0.25 * Diameter, Diameter, Diameter);
            box02.Translate(0, 0.6 * Diameter, 0);
            Brep[] breps1 = { box01, box02};
            Brep halfBrep = Brep.CreateBooleanUnion(breps1, DocTolerance.ModelToler)[0];

            return halfBrep;

            //*************************************
        }

        public virtual Brep Create(double length)
        {
            Brep step01 = PrimitiveSolid(length);
            Brep endStyle = EndStyle();
            Brep[] breps1 = { step01, endStyle };
            Brep halfBrep = Brep.CreateBooleanUnion(breps1, DocTolerance.ModelToler)[0];

            //镜像合并
            Brep step02 = halfBrep.DuplicateBrep();
            step02.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);
            step02.Translate(0, Length - 2 * settings.ColumnDiameter-0.05*settings.ScaleRule, 0);

            //
            Brep[] breps = { halfBrep, step02 };
            Brep step03 = Brep.CreateBooleanUnion(breps, DocTolerance.ModelToler)[0];

            //
            Brep result = step03;
            result.MergeCoplanarFaces(DocTolerance.ModelToler);

            if (null != Position)
            {
                result.Translate(new Vector3d(Position));
                result.Translate(0,0,-2* settings.ColumnDiameter);
            }

            return result;
        }
    }
}

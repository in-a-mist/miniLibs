using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;

namespace ComponentTest.Models.Columns
{
    public class ColumnBase
    {
        /// <summary>
        /// 直径
        /// </summary>
        public double Diameter { get; set; }
        /// <summary>
        /// 柱高
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 收分
        /// </summary>
        public double ShrinkPercent { get; set; }
        /// <summary>
        /// 底部位置点
        /// </summary>
        public Point3d Position { get; set; }
        /// <summary>
        /// 顶部定位点
        /// </summary>
        public Point3d GetVertex()
        {
            Point3d point = new Point3d(Position.X, Position.Y, Position.Z + Height);
            return point;
        }

        /// <summary>
        /// 柱子
        /// </summary>
        /// <param name="diameter">直径</param>
        /// <param name="height">高</param>
        /// <param name="shrinkPercent">收分比(_如收1/100柱高则设为：0.01)</param>
        public ColumnBase(double diameter, double height, double shrinkPercent=0.01)
        {
            Diameter = diameter;
            Height = height;
            ShrinkPercent = shrinkPercent;
        }


        protected Brep PrimitiveSolid()
        {
            Brep step01 = ShrinkCylinder();

            //燕尾榫
            Brep sub01 = CommonModel.SwallowtailTenon(0.2 * Diameter, 0.25 * Diameter, 0.3 * Diameter, Diameter);
            sub01.Translate(0, 0.35 * Diameter, Height);
            sub01.Rotate(Math.PI * 0.5, Vector3d.ZAxis, Point3d.Origin);

            Brep sub02 = sub01.DuplicateBrep();
            sub02.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);

            Brep step02 = Brep.CreateBooleanDifference(step01, sub01, DocTolerance.ModelToler)[0];
            Brep step03 = Brep.CreateBooleanDifference(step02, sub02, DocTolerance.ModelToler)[0];

            //馒头榫
            Brep bunTenon = CommonModel.BunTenon(0.33 * Diameter, 0.33 * Diameter);
            bunTenon.Translate(0, 0, Height);

            Brep[] breps = { step03, bunTenon };
            Brep step04 = Brep.CreateBooleanUnion(breps, DocTolerance.ModelToler)[0];

            ////透榫
            //Brep box01 = CommonModel.BoxBrep(0.25 * Diameter, 2.0 * Diameter, 0.5 * Diameter);
            //box01.Translate(0, 0, Height - 2 * Diameter);

            ////半榫
            //Brep box02 = CommonModel.BoxBrep(0.25 * Diameter, 0.8 * Diameter, Diameter);
            //box02.Translate(0, 0.5 * Diameter, Height - 2 * Diameter);

            //Brep step05 = Brep.CreateBooleanDifference(step04, box01, DocTolerance.ModelToler)[0];
            //Brep step06 = Brep.CreateBooleanDifference(step05, box02, DocTolerance.ModelToler)[0];


            ////
            //Brep result = step06;
            //result.MergeCoplanarFaces(DocTolerance.ModelToler);

            //if (null != Position)
            //{
            //    result.Translate(new Vector3d(Position));
            //}

            return step04;
        }

        protected Brep ShrinkCylinder()
        {
            double radius = Diameter * 0.5;
            double shrinkRadius = (Diameter - ShrinkPercent * Height) * 0.5;

            LineCurve edge = new LineCurve(new Point3d(radius, 0, 0), new Point3d(shrinkRadius, 0, Height));
            RevSurface revSrf = RevSurface.Create(edge, new Line(new Point3d(0, 0, 0), new Point3d(0, 0, Height)));
            Brep result = Brep.CreateFromRevSurface(revSrf, true, true);

            return result;
        }
    }
}

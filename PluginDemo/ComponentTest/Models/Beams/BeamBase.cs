using ComponentTest.Models.Utils;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Beams
{

    public class BeamBase
    {
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


        public BeamBase()
        {

        }

        protected Brep PrimitiveSolid(double lenght, double width, double height)
        {
            Brep box01 = CommonModel.BoxBrep(width, lenght, height);
            box01.Translate(0, 0.5 * lenght - settings.ColumnDiameter, 0);

            //
            Circle circle = new Circle(Plane.WorldYZ, 0.5 * settings.RoofCylinderDiameter);
            circle.Translate(new Vector3d(0.25*width, 0, 1.3 * settings.RoofCylinderDiameter));
            Cylinder cylinder = new Cylinder(circle, width);

            //檩椀
            Brep sub01 = cylinder.ToBrep(true, true);
            Brep sub02 = sub01.DuplicateBrep();
            sub02.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);

            Brep step02 = Brep.CreateBooleanDifference(box01, sub01, DocTolerance.ModelToler)[0];
            Brep step03 = Brep.CreateBooleanDifference(step02, sub02, DocTolerance.ModelToler)[0];

            //背
            Brep box02 = CommonModel.BoxBrep(width, 2*settings.ColumnDiameter, height);
            box02.Translate(0, -settings.ColumnDiameter, 1.3 * settings.RoofCylinderDiameter);
            box02.Translate(0, 0.8 * settings.RoofCylinderDiameter, 0);

            double radius = height - 1.3 * settings.RoofCylinderDiameter;
            Circle circle02 = new Circle(Plane.WorldYZ, radius);
            circle02.Translate(new Vector3d(-width, 0.8*settings.RoofCylinderDiameter, 1.3 * settings.RoofCylinderDiameter));
            Cylinder cylinder02 = new Cylinder(circle02, 2*width);
            Brep sub03 = cylinder02.ToBrep(true, true);

            Brep step04 = Brep.CreateBooleanDifference(box02, sub03, DocTolerance.ModelToler)[0];
            Brep step05 = Brep.CreateBooleanDifference(step03, step04, DocTolerance.ModelToler)[0];

            //檩板枋三件套：板卯口，燕尾榫：板厚0.25D
            double tileThickness = 0.25 * settings.ColumnDiameter;
            double shrinkThickness = 0.8 * tileThickness;
            Brep sub04 = CommonModel.SwallowtailTenon2(shrinkThickness, tileThickness, tileThickness, 1.3 * settings.RoofCylinderDiameter);
            sub04.Rotate(0.5*Math.PI, Vector3d.ZAxis, Point3d.Origin);
            sub04.Translate(-0.5*width+0.5* tileThickness, 0, 1.3 * settings.RoofCylinderDiameter);

            Brep sub05 = sub04.DuplicateBrep();
            sub05.Rotate(Math.PI, Vector3d.ZAxis, Point3d.Origin);

            Brep step06 = Brep.CreateBooleanDifference(step05, sub04, DocTolerance.ModelToler)[0];
            Brep step07 = Brep.CreateBooleanDifference(step06, sub05, DocTolerance.ModelToler)[0];

            return step07;
        }
    }
}

using Rhino.Geometry;

namespace ComponentTest.Models.Utils
{
    public class CommonModel
    {
        private static GlobalSettings settings = GlobalSettings.GetInstance();

        /// <summary>
        /// 在XY平面上，以中心点创建四边形
        /// (按梯形定义：可创建梯形，矩形，方形)
        /// </summary>
        /// <param name="topLength">上底</param>
        /// <param name="bottomLength">下底</param>
        /// <param name="height">高</param>
        /// <returns>四边形</returns>
        public static Curve Quadrilateral(double topLength, double bottomLength, double height)
        {
            Point3d cornerA = new Point3d(-0.5 * topLength, 0.5 * height, 0);
            Point3d cornerB = new Point3d(0.5 * topLength, 0.5 * height, 0);
            Point3d cornerC = new Point3d(0.5 * bottomLength, -0.5 * height, 0);
            Point3d cornerD = new Point3d(-0.5 * bottomLength, -0.5 * height, 0);

            Point3d[] cornerPoints = { cornerA, cornerB, cornerC, cornerD, cornerA };
            Curve quadCrv = Curve.CreateInterpolatedCurve(cornerPoints, 1);

            return quadCrv;
        }


        /// <summary>
        /// 燕尾榫
        /// (截面呈梯形)
        /// </summary>
        /// <param name="topLength">上底</param>
        /// <param name="bottomLength">下底</param>
        /// <param name="extendLength">榫长</param>
        /// <param name="height">榫高</param>
        /// <returns>燕尾榫</returns>
        public static Brep SwallowtailTenon(double topLength, double bottomLength,double extendLength, double height)
        {
            //收溜(且按收去下底面的十分之二)
            double shrinkPercent = 0.8;
            Curve topCrv = Quadrilateral(topLength, bottomLength, extendLength);
            Curve bottomCrv = Quadrilateral(topLength * shrinkPercent, bottomLength * shrinkPercent, extendLength);
            bottomCrv.Translate(0, 0, -height);

            Curve[] crvs = { bottomCrv, topCrv };
            Brep srf = Brep.CreateFromLoft(crvs, Point3d.Unset, Point3d.Unset, LoftType.Normal, false)[0];
            srf = srf.CapPlanarHoles(DocTolerance.ModelToler);
            srf.Faces.SplitKinkyFaces(DocTolerance.ModelToler);

            return srf;
        }

        /// <summary>
        /// 燕尾榫
        /// (截面呈梯形：无收溜)
        /// </summary>
        /// <param name="topLength">上底</param>
        /// <param name="bottomLength">下底</param>
        /// <param name="extendLength">榫长</param>
        /// <param name="height">榫高</param>
        /// <returns>燕尾榫</returns>
        public static Brep SwallowtailTenon2(double topLength, double bottomLength, double extendLength, double height)
        {
            //收溜(且按收去下底面的十分之二)
            double shrinkPercent = 1.0;
            Curve topCrv = Quadrilateral(topLength, bottomLength, extendLength);
            Curve bottomCrv = Quadrilateral(topLength * shrinkPercent, bottomLength * shrinkPercent, extendLength);
            bottomCrv.Translate(0, 0, -height);

            Curve[] crvs = { bottomCrv, topCrv };
            Brep srf = Brep.CreateFromLoft(crvs, Point3d.Unset, Point3d.Unset, LoftType.Normal, false)[0];
            srf = srf.CapPlanarHoles(DocTolerance.ModelToler);
            srf.Faces.SplitKinkyFaces(DocTolerance.ModelToler);

            return srf;
        }

        /// <summary>
        /// 馒头榫
        /// (截面呈方形)
        /// </summary>
        /// <param name="squareLength">方形边长</param>
        /// <param name="height">榫高</param>
        /// <returns>馒头榫</returns>
        public static Brep BunTenon(double squareLength,double height)
        {
            double shrinkSunDiameter = 0.8 * squareLength;
            double filletSunDiameter = 0.6 * squareLength;

            Curve bottomCrv = Quadrilateral(squareLength, squareLength, squareLength);
            Curve middleCrv = Quadrilateral(shrinkSunDiameter, shrinkSunDiameter, shrinkSunDiameter);
            middleCrv.Translate(0, 0, 0.9 * height);
            Curve topCrv = Quadrilateral(filletSunDiameter, filletSunDiameter, filletSunDiameter);
            topCrv.Translate(0, 0, height);

            Curve[] crvs = { bottomCrv, middleCrv, topCrv };
            Brep srf = Brep.CreateFromLoft(crvs, Point3d.Unset, Point3d.Unset, LoftType.Straight, false)[0];
            srf = srf.CapPlanarHoles(DocTolerance.ModelToler);
            srf.Faces.SplitKinkyFaces(DocTolerance.ModelToler);

            return srf;
        }

        /// <summary>
        /// 方体
        /// </summary>
        /// <param name="xLength">长</param>
        /// <param name="yLength">宽</param>
        /// <param name="zLength">高</param>
        /// <returns>长方体</returns>
        public static Brep BoxBrep(double xLength, double yLength, double zLength)
        {
            Interval xLen = new Interval(-0.5 * xLength, 0.5 * xLength);
            Interval yLen = new Interval(-0.5 * yLength, 0.5 * yLength);
            Interval zLen = new Interval(0, zLength);
            Box box = new Box(Plane.WorldXY, xLen, yLen, zLen);

            return box.ToBrep();
        }

        /// <summary>
        /// 撞一回二
        /// </summary>
        /// <param name="thickness">檩枋厚</param>
        /// <param name="columnDiameer">柱径</param>
        /// <returns>近似曲面</returns>
        public static Brep RollingEnd(double thickness, double columnDiameer,double columnHeight)
        {

            double radius01 = thickness / 8.0;
            double radius02 = (columnDiameer - 0.01 * columnHeight) * 0.5;

            Circle circle01 = new Circle(new Point3d(0, radius01, 0),3.0*radius01);
            Circle circle02 = new Circle(new Point3d(0, -radius01, 0),3.0*radius01);

            Circle circle03 = new Circle(new Point3d(radius02 + 2.5 * radius01, 0, 0), radius02);

            Rectangle3d rectangle = new Rectangle3d(Plane.WorldXY ,thickness, 2*thickness);
            Curve rectCrv = rectangle.ToNurbsCurve();
            rectCrv.Translate(0, -0.5 * thickness, 0);

            //
            Curve step01 = Curve.CreateBooleanDifference(rectCrv.ToNurbsCurve(), circle01.ToNurbsCurve(),DocTolerance.ModelToler)[0];
            Curve step02 = Curve.CreateBooleanDifference(step01, circle02.ToNurbsCurve(),DocTolerance.ModelToler)[0];

            //
            Curve[] crvs = { step02, circle03.ToNurbsCurve() };
            Curve step03 = Curve.CreateBooleanUnion(crvs, DocTolerance.ModelToler)[0];

            Brep result = Brep.CreatePlanarBreps(step03,DocTolerance.ModelToler)[0];
            result.Translate(-radius02 - 2.5 * radius01, 0, 0);

            return result;
        }
    }
}

using Rhino.Geometry;
using System.Collections.Generic;

namespace miniLibs
{
    public class Column
    {
        public ICalculatorRule _rule;
        private double _diameter;
        private double _height;

        public Column(ICalculatorRule rule, double diameter,double height)
        {
            _rule = rule;
            _height = height;
            _diameter = diameter;
        }
        public Brep GetShape()
        {
            double D = _diameter;
            double H = _height;
            double F = _rule.UnitValue;
            double t = 0.7 * D / 2;


            List<Point3d> arcCotralPoints = new List<Point3d>() {
                    new Point3d(t , 0, H ),
                    new Point3d(t +4*F , 0, H ),
                    new Point3d(t +4*F , 0, H-4*F )};

            Curve arcCrvs = Curve.CreateControlPointCurve(arcCotralPoints);


            List<Point3d> interPoints = new List<Point3d>() {

                    new Point3d(t +4*F , 0, H-4*F ),
                    new Point3d(D /2-4*F, 0, 8 * H / 9),
                    new Point3d(D /2-2*F, 0, 7 * H / 9),
                    new Point3d(D /2, 0, 2 * H / 3),
                    new Point3d(D /2, 0,  H / 3),
                    new Point3d(D /2, 0, 0) };

            Curve crvPolyLine = Curve.CreateInterpolatedCurve(interPoints, 3);

            List<Curve> crvsList = new List<Curve>() { arcCrvs, crvPolyLine };

            Curve crvToRo = Curve.JoinCurves(crvsList)[0];
            RevSurface revSrf = RevSurface.Create(crvToRo, new Line(Point3d.Origin, new Point3d(0, 0, 1)));

            Brep zhuBrep = Brep.CreateFromRevSurface(revSrf, true, true);

            return zhuBrep;
        }
    }
}

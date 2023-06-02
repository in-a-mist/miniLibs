using System;
using Rhino.Geometry;
using System.Collections.Generic;

namespace miniLibs
{ 
    public abstract class TouShape
    {
        protected TouData _dataTou;
        public TouShape(TouData dataTou)
        {
            _dataTou = dataTou;
        }
        protected Brep CreateOriginSolid()
        {
            //矩形放样成实体
            Rectangle3d downRec = Utils.CreateRec(_dataTou.Length - 0.4 * _dataTou.Height, _dataTou.Width - 0.4 * _dataTou.Height, Point3d.Origin);
            Rectangle3d midRec1 = Utils.CreateRec(_dataTou.Length - 0.4 * _dataTou.Height + 2 * _dataTou.InnerOffset, _dataTou.Width - 0.4 * _dataTou.Height + 2 * _dataTou.InnerOffset, new Point3d(0, 0, _dataTou.Lower * 0.5));
            Rectangle3d midRec2 = Utils.CreateRec(_dataTou.Length, _dataTou.Width, new Point3d(0, 0, _dataTou.Lower));
            Rectangle3d upperRec = Utils.CreateRec(_dataTou.Length, _dataTou.Width, new Point3d(0, 0, _dataTou.Lower + _dataTou.Middle));
            Rectangle3d topRec = Utils.CreateRec(_dataTou.Length, _dataTou.Width, new Point3d(0, 0, _dataTou.Height));

            Curve[] crv1 = { downRec.ToNurbsCurve(), midRec1.ToNurbsCurve(), midRec2.ToNurbsCurve() };
            Brep srfs1 = Brep.CreateFromLoft(crv1, Point3d.Unset, Point3d.Unset, LoftType.Normal, false)[0];

            Curve[] crv2 = { midRec2.ToNurbsCurve(), upperRec.ToNurbsCurve(), topRec.ToNurbsCurve() };
            Brep srfs2 = Brep.CreateFromLoft(crv2, Point3d.Unset, Point3d.Unset, LoftType.Normal, false)[0];

            //两部分合并
            Brep[] srfs = { srfs1, srfs2 };
            Brep solid = Brep.JoinBreps(srfs, Utils.GetTolerance)[0];
            solid = solid.CapPlanarHoles(Utils.GetTolerance);
            solid.Faces.SplitKinkyFaces();

            return solid;
        }
        public abstract Brep GetShape();
    }

    public class ShanTou : TouShape
    {
        public ShanTou(TouData dataTou) : base(dataTou)
        {
        }
        public override Brep GetShape()
        {
            Brep box1 = Utils.CreateBox(_dataTou.Length, _dataTou._rule.WidthSize, _dataTou.Height, new Point3d(0, 0, _dataTou.Lower + _dataTou.Middle));

            Brep origin = CreateOriginSolid();
            Brep resultSolid = Brep.CreateBooleanDifference(box1, origin, Utils.GetTolerance)[0];


            return resultSolid;
        }

    }

    public class ChiSinTou : TouShape
    {
        public ChiSinTou(TouData dataTou) : base(dataTou)
        {
        }

        public override Brep GetShape()
        {
            Brep result = new ShanTou(_dataTou).GetShape();
            return result;
        }
    }

    public class LuTou : TouShape
    {
        public LuTou(TouData dataTou) : base(dataTou)
        {
        }

        public override Brep GetShape()
        {
            Brep origin = new ShanTou(_dataTou).GetShape();
            Brep box2 = Utils.CreateBox(_dataTou._rule.WidthSize, _dataTou.Width, _dataTou.Height, new Point3d(0, 0, _dataTou.Lower + _dataTou.Upper));
            box2.Flip();

            Brep box3 = Utils.CreateBox(_dataTou._rule.WidthSize, _dataTou.Width, _dataTou.Height, new Point3d(0, 0, _dataTou.Lower + _dataTou.Middle));
            Brep box4 = Utils.CreateBox(_dataTou.Length, _dataTou._rule.WidthSize + (6.0 / 20.0) * _dataTou.Height, _dataTou.Height, new Point3d(0, 0, _dataTou.Lower + _dataTou.Middle));
            Brep[] box5 = Brep.CreateBooleanDifference(box4, box3, Utils.GetTolerance);

            Brep[] solid = Brep.CreateBooleanDifference(origin, box2, Utils.GetTolerance);
            Brep result = Brep.CreateBooleanDifference(solid, box5, Utils.GetTolerance)[0];
            result.MergeCoplanarFaces(Utils.GetTolerance);
            return result;
        }
    }

    public class ChiaoHuTou : TouShape
    {
        public ChiaoHuTou(TouData dataTou) : base(dataTou)
        {
        }

        public override Brep GetShape()
        {
            Brep result = new LuTou(_dataTou).GetShape();
            return result;
        }
    }


    //**************************************************
    public abstract class KungShape
    {
        protected KungData _dataKung;
        public KungShape(KungData dataKong)
        {
            _dataKung = dataKong;
        }

        protected Brep CreateHalfSolid(double lenghth, double rotateAngle)
        {
            //长方体
            Brep priSolid = Utils.CreateBox(lenghth, _dataKung.Width, _dataKung.Height, Point3d.Origin);
            priSolid.Translate(0.5 * lenghth, 0, 0);
            priSolid.Flip();

            //斗
            Brep subSolid = new ShanTou(new ShanTouData(_dataKung._rule)).GetShape();
            subSolid.Translate(5 * _dataKung._rule.UnitValue, 0, _dataKung._rule.SingleSize);

            //卷杀
            Brep srfs = Utils.CreateCutSolid(_dataKung.CutFullLength, _dataKung.CutHeight, _dataKung.CutAmount);
            Brep cutSolid = Brep.CreateFromOffsetFace(srfs.Faces[0], 0.5 * _dataKung.Width, Utils.GetTolerance, true, true);

            //栱眼
            Brep gy1 = Utils.CreateGY(new Point3d(10 * _dataKung._rule.UnitValue, 0.5 * _dataKung.Width, _dataKung._rule.SingleSize), new Point3d(lenghth - 6 * _dataKung._rule.UnitValue, 0.5 * _dataKung.Width, _dataKung._rule.SingleSize), _dataKung._rule.UnitValue, _dataKung._rule.UnitValue);
            Brep gy2 = Utils.CreateGY(new Point3d(10 * _dataKung._rule.UnitValue, -0.5 * _dataKung.Width, _dataKung._rule.SingleSize), new Point3d(lenghth - 6 * _dataKung._rule.UnitValue, -0.5 * _dataKung.Width, _dataKung._rule.SingleSize), _dataKung._rule.UnitValue, _dataKung._rule.UnitValue);


            //开始减操作：挖出栱眼-->下端头卷杀-->上端头留出置斗处
            Brep step1 = Brep.CreateBooleanDifference(priSolid, gy1, Utils.GetTolerance)[0];
            Brep step2 = Brep.CreateBooleanDifference(step1, gy2, Utils.GetTolerance)[0];

            Brep step3 = Brep.CreateBooleanDifference(step2, cutSolid, Utils.GetTolerance)[0];

            Brep resultSolid = Brep.CreateBooleanDifference(step3, subSolid, Utils.GetTolerance)[0];

            rotateAngle = Math.PI * rotateAngle / 180.0;
            resultSolid.Translate(-lenghth, 0, 0);
            resultSolid.Rotate(rotateAngle, Vector3d.ZAxis, Point3d.Origin);
            return resultSolid;
        }

        protected Brep CreateOriginSolid()
        {
            Brep solid1 = CreateHalfSolid(_dataKung.ExtendOuter, 0);
            Brep solid2 = CreateHalfSolid(_dataKung.ExtendOuter, 180);
            Brep[] solids = { solid1, solid2 };
            Brep resultSolid = Brep.CreateBooleanUnion(solids, Utils.GetTolerance)[0];

            if (! _dataKung.IsEnough)
            {
                Brep priSolid = Utils.CreateBox(2 * _dataKung.FullLength, _dataKung.Width, _dataKung.Height, new Point3d(0, 0, _dataKung._rule.SingleSize));
                priSolid.Flip();
                resultSolid = Brep.CreateBooleanDifference(resultSolid, priSolid, Utils.GetTolerance)[0];
            }

            resultSolid.MergeCoplanarFaces(Utils.GetTolerance);
            return resultSolid;
        }

        public abstract Brep GetShape();
    }

    public class HuaKung : KungShape
    {
        public HuaKung(KungData dataKung) : base(dataKung)
        {
        }

        public override Brep GetShape()
        {
            Brep priSolid = CreateOriginSolid();
            Brep subSolid = Utils.CreateBox(16 * _dataKung._rule.UnitValue, _dataKung.Width, 4 * _dataKung._rule.UnitValue, new Point3d(0, 0, 0));
            subSolid.Flip();
            Brep resultSolid = Brep.CreateBooleanDifference(priSolid, subSolid, Utils.GetTolerance)[0];
            resultSolid.Rotate(Math.PI*0.5, Vector3d.ZAxis, Point3d.Origin);

            Brep subSolid2 = Utils.CreateBox(16 * _dataKung._rule.UnitValue, _dataKung.Width, _dataKung._rule.EnoughSize, new Point3d(0, 0, 0));
            Brep subSolid3 = Utils.CreateBox(8 * _dataKung._rule.UnitValue, _dataKung.Width, _dataKung._rule.EnoughSize, new Point3d(0, 0, 5 * _dataKung._rule.UnitValue));
            Brep subSolid4 = Brep.CreateBooleanDifference(subSolid3, subSolid2, Utils.GetTolerance)[0];

            resultSolid = Brep.CreateBooleanDifference(resultSolid, subSolid4, Utils.GetTolerance)[0];

            return resultSolid;
        }
    }

    public class NiTaoKung : KungShape
    {
        public NiTaoKung(KungData dataKung) : base(dataKung)
        {
        }

        public override Brep GetShape()
        {
            Brep priSolid = CreateOriginSolid();
            Brep subSolid3 = Utils.CreateBox(8 * _dataKung._rule.UnitValue, _dataKung.Width, _dataKung._rule.EnoughSize, new Point3d(0, 0, 5 * _dataKung._rule.UnitValue));
            subSolid3.Flip();

            Brep resultSolid = Brep.CreateBooleanDifference(priSolid, subSolid3, Utils.GetTolerance)[0];

            return resultSolid;
        }
    }

    public class KuaTzuKung : KungShape
    {
        public KuaTzuKung(KungData dataKung) : base(dataKung)
        {
        }

        public override Brep GetShape()
        {
            Brep resultSolid = new NiTaoKung(_dataKung).GetShape();

            return resultSolid;
        }
    }

    public class LingKung : KungShape
    {
        public LingKung(KungData dataKung) : base(dataKung)
        {
        }

        public override Brep GetShape()
        {
            Brep resultSolid = new NiTaoKung(_dataKung).GetShape();

            return resultSolid;
        }
    }

    public class ManKung : KungShape
    {
        public ManKung(KungData dataKung) : base(dataKung)
        {
        }

        public override Brep GetShape()
        {
            Brep resultSolid = new NiTaoKung(_dataKung).GetShape();

            return resultSolid;
        }
    }

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

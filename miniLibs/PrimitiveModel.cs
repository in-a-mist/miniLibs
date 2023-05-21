using Rhino.Geometry;
namespace miniLibs
{
    public class Utils
    {
        public static Rectangle3d CreateRec(double recLenght, double recWidth, Point3d planePositon)
        {
            Plane plane = Plane.WorldXY;
            plane.Translate(new Vector3d(planePositon));
            Interval len = new Interval(-0.5 * recLenght, 0.5 * recLenght);
            Interval wide = new Interval(-0.5 * recWidth, 0.5 * recWidth);
            Rectangle3d rec = new Rectangle3d(plane, len, wide);
            return rec;
        }
        public static Brep CreateBox(double boxLenght, double boxWidth, double boxHeight, Point3d planePositon)
        {
            Curve rec = Utils.CreateRec(boxLenght, boxWidth, planePositon).ToNurbsCurve();
            Vector3d vec = new Vector3d(0, 0,boxHeight);
            Brep srfs = Surface.CreateExtrusion(rec, vec).ToBrep();

            Brep solid = srfs.CapPlanarHoles(Utils.GetTolerance);
            solid.Faces.SplitKinkyFaces();
            return solid;
        }

        public static double GetTolerance => Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
    }

    public abstract class TouFactory
    {
        protected ITou _dataTou;
        protected Point3d _position;
        public TouFactory(ITou dataTou, Point3d position)
        {
            _dataTou = dataTou;
            _position = position;
        }
        protected Brep CreateOriginSolid()
        {
            //矩形放样成实体
            Rectangle3d downRec = Utils. CreateRec(_dataTou.Length - 0.4 * _dataTou.Height, _dataTou.Width - 0.4 * _dataTou.Height, Point3d.Origin);
            Rectangle3d midRec1 = Utils. CreateRec(_dataTou.Length - 0.4 * _dataTou.Height + 2 * _dataTou.InnerOffset, _dataTou.Width - 0.4 * _dataTou.Height + 2 * _dataTou.InnerOffset, new Point3d(0,0, _dataTou.Lower * 0.5));
            Rectangle3d midRec2 = Utils. CreateRec(_dataTou.Length, _dataTou.Width, new Point3d(0,0, _dataTou.Lower));
            Rectangle3d upperRec =Utils.  CreateRec(_dataTou.Length, _dataTou.Width, new Point3d(0,0, _dataTou.Lower + _dataTou.Middle));
            Rectangle3d topRec =  Utils.  CreateRec(_dataTou.Length, _dataTou.Width,new Point3d(0,0, _dataTou.Height));

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
        public Point3d Position => _position;
        public Point3d TopPoint => new Point3d(_position.X, _position.Y, _position.Z + _dataTou.Lower + _dataTou.Middle);
        public abstract Brep GetShape();
    }
    public class Tou:TouFactory
    {
        public Tou(ITou dataTou, Point3d locationPoint) : base(dataTou, locationPoint)
        {
        }
        public override Brep GetShape()
        {
            return CreateOriginSolid();
        }

    }

    public abstract class KongFactory
    {
        protected IKung _dataKung;
        protected Point3d _position;
        public KongFactory(IKung dataKong, Point3d position)
        {
            _dataKung = dataKong;
            _position = position;
        }

        protected Brep CreateOriginSolid()
        {
            Brep box1 = Utils.CreateBox(_dataKung.ExtendOuter + _dataKung.ExtendInner, _dataKung.Width, _dataKung.Height, Point3d.Origin);
            return box1;
        }

        public Point3d Position => _position;
        public abstract Brep GetShape();
    }

    public class Kong : KongFactory
    {
        public Kong(IKung dataKung, Point3d position) : base(dataKung, position)
        {
        }

        public override Brep GetShape()
        {
            return CreateOriginSolid();
        }
    }
}

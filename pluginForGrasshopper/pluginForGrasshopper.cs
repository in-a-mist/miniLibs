using System;
using Grasshopper.Kernel;
using Rhino.Geometry;
using miniLibs;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace pluginForGrasshopper

{
    public class LuTouComponent : GH_Component
    {
        public LuTouComponent()
          : base("LuTou", "栌枓",
              "栌枓",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new LuTouData(rule);
            var tou = new LuTou(data);

            var shape = tou.GetShape();
            shape.Translate(new Vector3d(position));

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data.Height * 0.6);

            DA.SetData(0, shape);
            DA.SetData(1, topPoint);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("467e8a10-fe89-422c-a53f-8bba50799820"); }
        }
    }

    public class ShanTouComponent : GH_Component
    {
        public ShanTouComponent()
          : base("ShanLuTou", "散枓",
              "散枓",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new ShanTouData(rule);
            var tou = new ShanTou(data);

            var shape = tou.GetShape();
            shape.Translate(new Vector3d(position));

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data.Height * 0.6);

            DA.SetData(0, shape);
            DA.SetData(1, topPoint);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("44898400-815E-4FF7-BC8D-49C3F4C018C3"); }
        }
    }

    public class ChiaoHuTouComponent : GH_Component
    {
        public ChiaoHuTouComponent()
          : base("ChiaoHuTou", "交互枓",
              "交互枓",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new ChiaoHuTouData(rule);
            var tou = new ChiaoHuTou(data);

            var shape = tou.GetShape();
            shape.Translate(new Vector3d(position));

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data.Height * 0.6);

            DA.SetData(0, shape);
            DA.SetData(1, topPoint);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("F19CFA01-8BE1-4468-AC52-E80C3FD1F043"); }
        }
    }

    public class ChiSinTouComponent : GH_Component
    {
        public ChiSinTouComponent()
          : base("ChiSinTou", "齐心枓",
              "齐心枓",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new ChiSinTouData(rule);
            var tou = new ChiSinTou(data);

            var shape = tou.GetShape();
            shape.Translate(new Vector3d(position));

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data.Height * 0.6);

            DA.SetData(0, shape);
            DA.SetData(1, topPoint);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("9F01967E-03B1-415B-8A67-098DBCC1FA54"); }
        }
    }

    public class FirstHuaKungComponent : GH_Component
    {
        public FirstHuaKungComponent()
          : base("HuaKung", "华栱1",
              "华栱第一跳",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Level", "L", "铺作层数", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("PointMid", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointOut", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointIns", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
            pManager.HideParameter(2);
            pManager.HideParameter(3);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            int setLevel = 4;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref setLevel)) return;
            if (setLevel < 4 || setLevel >8) return;
            if (!DA.GetData(2, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new HuaKungData(rule,setLevel);
            var kung = new HuaKung(data);

            var shape = kung.GetShape();
            shape.Translate(new Vector3d(position));
            DA.SetData(0, shape);

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data._rule.EnoughSize);
            DA.SetData(1, topPoint);

            double numOut = data.ExtendOuter - 0.5 * data.EndLengthForTou;
            double numIns = data.ExtendInner - 0.5 * data.EndLengthForTou;

            Point3d outPoint = new Point3d(position.X, position.Y-numOut, position.Z + data._rule.SingleSize);
            Point3d insPoint = new Point3d(position.X, position.Y+numIns, position.Z + data._rule.SingleSize);
            DA.SetData(2, outPoint);
            DA.SetData(3, insPoint);

        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("3eab81e6-1422-4741-a140-0682bae3eba7"); }
        }
    }

    public class NiTaoKungComponent : GH_Component
    {
        public NiTaoKungComponent()
          : base("NiTaoKung", "泥道栱",
              "泥道栱",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("PointMid", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointOut", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointIns", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
            pManager.HideParameter(2);
            pManager.HideParameter(3);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new NiTaoKungData(rule);
            var kung = new NiTaoKung(data);

            var shape = kung.GetShape();
            shape.Translate(new Vector3d(position));
            DA.SetData(0, shape);

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data._rule.EnoughSize);
            DA.SetData(1, topPoint);

            double numOut = data.ExtendOuter - 0.5 * data.EndLengthForTou;
            double numIns = data.ExtendInner - 0.5 * data.EndLengthForTou;

            Point3d outPoint = new Point3d(position.X - numOut, position.Y, position.Z + data._rule.SingleSize);
            Point3d insPoint = new Point3d(position.X + numIns, position.Y, position.Z + data._rule.SingleSize);
            DA.SetData(2, outPoint);
            DA.SetData(3, insPoint);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("9AB34456-9991-4256-B2A8-5A6711FFC4CB"); }
        }
    }

    public class KuaTzuKungComponent : GH_Component
    {
        public KuaTzuKungComponent()
          : base("KuaTzuKung", "瓜子栱",
              "瓜子栱",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("PointMid", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointOut", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointIns", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
            pManager.HideParameter(2);
            pManager.HideParameter(3);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new KuaTzuKungData(rule);
            var kung = new KuaTzuKung(data);

            var shape = kung.GetShape();
            shape.Translate(new Vector3d(position));
            DA.SetData(0, shape);

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data._rule.EnoughSize);
            DA.SetData(1, topPoint);

            double numOut = data.ExtendOuter - 0.5 * data.EndLengthForTou;
            double numIns = data.ExtendInner - 0.5 * data.EndLengthForTou;

            Point3d outPoint = new Point3d(position.X - numOut, position.Y, position.Z + data._rule.SingleSize);
            Point3d insPoint = new Point3d(position.X + numIns, position.Y, position.Z + data._rule.SingleSize);
            DA.SetData(2, outPoint);
            DA.SetData(3, insPoint);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("AE4A5EB3-F390-4E6C-9C10-4FE8B23B77B5"); }
        }
    }

    public class LingKungComponent : GH_Component
    {
        public LingKungComponent()
          : base("LingKung", "令栱",
              "令栱",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("PointMid", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointOut", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointIns", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
            pManager.HideParameter(2);
            pManager.HideParameter(3);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new LingKungData(rule);
            var kung = new LingKung(data);

            var shape = kung.GetShape();
            shape.Translate(new Vector3d(position));
            DA.SetData(0, shape);

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data._rule.EnoughSize);
            DA.SetData(1, topPoint);

            double numOut = data.ExtendOuter - 0.5 * data.EndLengthForTou;
            double numIns = data.ExtendInner - 0.5 * data.EndLengthForTou;

            Point3d outPoint = new Point3d(position.X - numOut, position.Y, position.Z + data._rule.SingleSize);
            Point3d insPoint = new Point3d(position.X + numIns, position.Y, position.Z + data._rule.SingleSize);
            DA.SetData(2, outPoint);
            DA.SetData(3, insPoint);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("C3DCEED9-1C3E-4E64-85CB-EA755BE22AB5"); }
        }
    }

    public class ManKungComponent : GH_Component
    {
        public ManKungComponent()
          : base("ManKung", "慢栱",
              "慢栱",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("PointMid", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointOut", "P", "点", GH_ParamAccess.item);
            pManager.AddPointParameter("PointIns", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
            pManager.HideParameter(2);
            pManager.HideParameter(3);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            var rule = new CaiFenRule(size);
            var data = new ManKungData(rule);
            var kung = new ManKung(data);

            var shape = kung.GetShape();
            shape.Translate(new Vector3d(position));
            DA.SetData(0, shape);

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data._rule.EnoughSize);
            DA.SetData(1, topPoint);

            double numOut = data.ExtendOuter - 0.5 * data.EndLengthForTou;
            double numIns = data.ExtendInner - 0.5 * data.EndLengthForTou;

            Point3d outPoint = new Point3d(position.X - numOut, position.Y, position.Z + data._rule.SingleSize);
            Point3d insPoint = new Point3d(position.X + numIns, position.Y, position.Z + data._rule.SingleSize);
            DA.SetData(2, outPoint);
            DA.SetData(3, insPoint);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("172F6545-3181-4444-94D4-BA46A5E7C9BB"); }
        }
    }

    public class ColumnComponent : GH_Component
    {
        public ColumnComponent()
          : base("Colum", "梭柱",
              "梭柱",
              "MiniLibs", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddNumberParameter("Diameter", "D", "直径", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "S", "高度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            double diameter = 0;
            double height = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (!DA.GetData(1, ref diameter)) return;
            if (!DA.GetData(2, ref height)) return;
            if (!DA.GetData(3, ref position)) return;

            if (size <= 0 || diameter <= 0 || height <= 0) return;

            var rule = new CaiFenRule(size);
            var column = new Column(rule, diameter, height);
            var shape = column.GetShape();
            shape.Translate(new Vector3d(position));

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + height);

            DA.SetData(0, shape);
            DA.SetData(1, topPoint);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("63E21C2E-7688-4A47-9034-89A94734D94F"); }
        }
    }

    public class GridsComponent : GH_Component
    {
        public GridsComponent()
          : base("Grids", "平面轴网",
              "殿阁地盘分槽图",
              "MiniLibs", "Primitive")
        {
            Message = "殿阁地盘分槽图";
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("明间", "明间", "明间", GH_ParamAccess.item);
            pManager.AddNumberParameter("次间", "次间", "次间", GH_ParamAccess.list);
            pManager.AddNumberParameter("尽间", "尽间", "尽间", GH_ParamAccess.item);
            pManager.AddNumberParameter("进深", "进深", "进深", GH_ParamAccess.list);
            pManager.AddNumberParameter("副阶", "副阶", "副阶，数值为零则没有副阶", GH_ParamAccess.item);
            pManager.AddIntegerParameter("类型", "类型", "类型,1-单槽，2-双槽，3-分心槽，1-金厢斗底槽", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("殿身点", "殿身", "殿身", GH_ParamAccess.list);
            pManager.AddPointParameter("副阶点", "副阶", "副阶", GH_ParamAccess.list);
            pManager.AddIntegerParameter("开间数", "开间", "开间", GH_ParamAccess.item);
            pManager.AddIntegerParameter("进深数", "进深", "进深", GH_ParamAccess.item);
            pManager.AddIntegerParameter("副阶数", "副阶", "副阶", GH_ParamAccess.item);

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            double middleDis = double.NaN,
                endDis = double.NaN,
                surroDis = double.NaN;
            List<double> jiaShengDis = new List<double>();
            List<double> ciJianDis = new List<double>();

            int typeNum = 0;


            DA.GetData(0, ref middleDis);
            DA.GetDataList(1, ciJianDis);
            DA.GetData(2, ref endDis);
            DA.GetDataList(3, jiaShengDis);
            DA.GetData(4, ref surroDis);
            DA.GetData(5, ref typeNum);




            if (middleDis <= 0) middleDis = 0;
            if (ciJianDis == null) ciJianDis = new List<double>() { 0 };
            if (endDis <= 0) endDis = 0;
            if (jiaShengDis == null) jiaShengDis = new List<double>() { 0 };
            if (surroDis <= 0) surroDis = 0;

            //在Y轴方向上创建系列点
            List<Point2d> ptsYAxis = new List<Point2d>()
            {
                new Point2d(0,0),
            };

            List<double> yAxisDis = new List<double>
            {
                surroDis,
            };

            yAxisDis.AddRange(jiaShengDis);
            yAxisDis.Add(surroDis);

            double sumYAxisDis = 0;
            foreach (var item in yAxisDis)
            {
                sumYAxisDis += item;
                ptsYAxis.Add(new Point2d(0, sumYAxisDis));

            }

            ptsYAxis = ptsYAxis.Distinct<Point2d>().ToList<Point2d>();

            //沿X轴方向上复制之前创建的系列点

            List<double> xAxisDis = new List<double>
            {
                surroDis,
                endDis,
            };
            ciJianDis.Reverse();
            xAxisDis.AddRange(ciJianDis);
            xAxisDis.Add(middleDis);
            ciJianDis.Reverse();
            xAxisDis.AddRange(ciJianDis);
            xAxisDis.Add(endDis);
            xAxisDis.Add(surroDis);

            List<Point2d> ptsXAxis = new List<Point2d>();

            double sumXAxisDis = 0;
            foreach (var item in xAxisDis)
            {
                sumXAxisDis += item;
                foreach (var ptY in ptsYAxis)
                {
                    Point2d ptX = new Point2d(sumXAxisDis, ptY.Y);
                    ptsXAxis.Add(ptX);
                }

            }
            ptsXAxis = ptsXAxis.Distinct<Point2d>().ToList<Point2d>();


            //按平面类型筛选点
            List<Point2d> ptsResult = new List<Point2d>();
            foreach (var item in ptsYAxis)
            {
                ptsResult.Add(item);
            }
            foreach (var item in ptsXAxis)
            {
                ptsResult.Add(item);
            }
            ptsResult = ptsResult.Distinct<Point2d>().ToList<Point2d>();



            double xDisMin = xAxisDis[0] + xAxisDis[1];
            double xDisMax = 0;
            for (int i = 0; i < xAxisDis.Count - 2; i++)
            {
                xDisMax += xAxisDis[i];
            }

            switch (typeNum)
            {
                case 1:
                    if (endDis <= 0)
                    {
                        MessageBox.Show("尽间面阔不应为零值");
                        return;
                    }
                    double yDis1 = yAxisDis[0] + yAxisDis[1];
                    var subPts11 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis1);
                    ptsResult = ptsResult.Except(subPts11).ToList();
                    break;

                case 3:
                    if (yAxisDis.Count <= 4 || endDis <= 0)
                    {
                        MessageBox.Show("若分心槽，进深间数应多于三，尽间面阔不应为零值");
                        return;
                    }
                    double yDis21 = yAxisDis[0] + yAxisDis[1];
                    double yDis22 = yAxisDis[0] + yAxisDis[1] + yAxisDis[2] + yAxisDis[3];
                    var subPts21 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis21);
                    var subPts22 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis22);
                    ptsResult = ptsResult.Except(subPts21).Except(subPts22).ToList();
                    break;

                case 2:
                    if (yAxisDis.Count <= 4 || endDis <= 0)
                    {
                        MessageBox.Show("若双槽，进深间数应多于三，尽间面阔不应为零值");
                        return;
                    }
                    double yDis31 = yAxisDis[0] + yAxisDis[1] + yAxisDis[2];
                    var subPts31 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis31);
                    ptsResult = ptsResult.Except(subPts31).ToList();
                    break;

                case 4:

                    double xDisMin4 = xAxisDis[0] + xAxisDis[1] + yAxisDis[2];
                    double xDisMax4 = 0;
                    for (int i = 0; i < xAxisDis.Count - 3; i++)
                    {
                        xDisMax4 += xAxisDis[i];
                    }

                    if (yAxisDis.Count <= 4 || endDis <= 0 || ciJianDis[0] <= 0)
                    {
                        MessageBox.Show("若金厢斗底槽，面阔、进深间数应多于三，次间、尽间面阔不应为零值");
                        return;
                    }
                    double yDis41 = yAxisDis[0] + yAxisDis[1] + yAxisDis[2];
                    var subPts41 = ptsResult.Where(pt => pt.X >= xDisMin4 && pt.X <= xDisMax4 && pt.Y == yDis41);
                    ptsResult = ptsResult.Except(subPts41).ToList();
                    break;


                default:
                    break;
            }

            //输出需要的数据
            DA.SetData(2, xAxisDis.Count - 2);
            DA.SetData(3, yAxisDis.Count - 2);

            //
            if (surroDis != 0)
            {
                var fujiePts = ptsResult.Where(pt => pt.X == ptsResult[0].X || pt.X == ptsResult.Last().X || pt.Y == ptsYAxis[0].Y || pt.Y == ptsYAxis.Last().Y);
                var diansPts = ptsResult.Except(fujiePts).ToList();
                DA.SetDataList(0, diansPts);
                DA.SetDataList(1, fujiePts);
                DA.SetData(4, 2);
            }
            else
            {
                DA.SetDataList(0, ptsResult);
                DA.SetData(4, 0);
            }
        }
        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("7784DFFC-C8A9-4DDF-A56C-76FBCFB28B1A");
    }


}


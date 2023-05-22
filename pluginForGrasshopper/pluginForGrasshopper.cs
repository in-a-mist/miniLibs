using System;
using Grasshopper.Kernel;
using Rhino.Geometry;
using miniLibs;

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

    public class HuaKungComponent : GH_Component
    {
        public HuaKungComponent()
          : base("HuaKung", "华栱",
              "华栱",
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
            var data = new HuaKungData(rule);
            var kung = new HuaKung(data);

            var shape = kung.GetShape();
            shape.Translate(new Vector3d(position));
            DA.SetData(0, shape);

            Point3d topPoint = new Point3d(position.X, position.Y, position.Z + data._rule.EnoughSize);
            DA.SetData(1, topPoint);

            double numOut = data.ExtendOuter - 5 * data._rule.UnitValue;
            double numIns = data.ExtendInner - 5 * data._rule.UnitValue;

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

            double numOut = data.ExtendOuter - 5 * data._rule.UnitValue;
            double numIns = data.ExtendInner - 5 * data._rule.UnitValue;

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

            double numOut = data.ExtendOuter - 5 * data._rule.UnitValue;
            double numIns = data.ExtendInner - 5 * data._rule.UnitValue;

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

            double numOut = data.ExtendOuter - 5 * data._rule.UnitValue;
            double numIns = data.ExtendInner - 5 * data._rule.UnitValue;

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

            double numOut = data.ExtendOuter - 5 * data._rule.UnitValue;
            double numIns = data.ExtendInner - 5 * data._rule.UnitValue;

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
}


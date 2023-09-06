using System;
using System.Drawing;
using System.Windows.Forms;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using miniLibs;


namespace pluginForGrasshopper
{
    public class UnitConvertion : GH_Component
    {
        public enum SqrtMode { M, CM }
        public SqrtMode CompWorkMode { get; set; } = SqrtMode.CM;
        public UnitConvertion()
          : base("Unit", "转换",
              "古代营造尺换为公制单位,CM：厘米，M：米",
               "MiniLibs", "Primitive")
        { }
        public override void CreateAttributes()
        {
            Attributes = new SqrtAttribute(this);
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("CHI", "CHI", "古代营造尺", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Metric", "M", "公制度量单位", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            CaiFenRule cfr = new CaiFenRule(6.0);
            double a = 0.0;
            if (!DA.GetData(0, ref a)) return;

            double cm = cfr.CHI2CM(a);
            double m = cm * 0.01;


            if (CompWorkMode == SqrtMode.M)
                DA.SetData(0, m);
            else
                DA.SetData(0, cm);
        }
        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => Guid.Parse("39CA8851-6529-44F8-B098-9DA0443B16C1");
    }

    public class SqrtAttribute : GH_ComponentAttributes
    {
        public SqrtAttribute(UnitConvertion component) : base(component) { }
        protected override void Layout()
        {
            base.Layout();
            Bounds = new RectangleF(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height + 20.0f);
        }
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {
            base.Render(canvas, graphics, channel);

            if (channel == GH_CanvasChannel.Objects)
            {
                RectangleF buttonRect = new RectangleF(Bounds.X, Bounds.Bottom - 20, Bounds.Width, 20.0f);
                buttonRect.Inflate(-2.0f, -2.0f);

                using (GH_Capsule capsule = GH_Capsule.CreateCapsule(buttonRect, GH_Palette.Black))
                {
                    capsule.Render(graphics, Selected, Owner.Locked, Owner.Hidden);
                }

                graphics.DrawString(
                    ((UnitConvertion)Owner).CompWorkMode.ToString(),
                    new Font(GH_FontServer.ConsoleSmall, FontStyle.Bold),
                    Brushes.White,
                    buttonRect,
                    new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    });
            }
        }
        public override GH_ObjectResponse RespondToMouseDown(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            RectangleF buttonRect = new RectangleF(Bounds.X, Bounds.Bottom - 20, Bounds.Width, 20.0f);
            if (e.Button == MouseButtons.Left && buttonRect.Contains(e.CanvasLocation))
            {
                UnitConvertion comp = (UnitConvertion)Owner;
                if (comp.CompWorkMode == UnitConvertion.SqrtMode.CM)
                    comp.CompWorkMode = UnitConvertion.SqrtMode.M;
                else
                    comp.CompWorkMode = UnitConvertion.SqrtMode.CM;

                comp.ExpireSolution(true);
                return GH_ObjectResponse.Handled;
            }
            return GH_ObjectResponse.Ignore;
        }
    }
}
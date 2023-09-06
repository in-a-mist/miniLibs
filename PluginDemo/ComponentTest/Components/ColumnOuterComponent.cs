using System;
using ComponentTest.Models.Columns;
using ComponentTest.Models.Utils;
using Grasshopper.Kernel;
using Rhino.Geometry;


namespace ComponentTest.Components
{
    public class ColumnOuterComponent : GH_Component
    {
        public ColumnOuterComponent()
          : base("ColumnOuter", "C",
              "檐柱",
              "Test", "Columns")
        {
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item, Point3d.Origin);
            pManager.HideParameter(0);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "物件", GH_ParamAccess.item);
            pManager.AddPointParameter("Vertex", "V", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            if (! GlobalSettings.LookupSettings()) return;

            double diameter = GlobalSettings.GetInstance().ColumnDiameter;
            double height = GlobalSettings.GetInstance().ColumnHeight;
            Point3d position = Point3d.Unset;

            if (!DA.GetData(0, ref position)) return;

            //
            ColumnOuter column = new ColumnOuter(diameter, height);
            column.Position = position;


            //
            Brep result = column.Create();
            Point3d vertex = column.GetVertex();


            DA.SetData(0, result);
            DA.SetData(1, vertex);
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
            get { return new Guid("78636dc8-4288-47ab-98c8-7ef872e5951b"); }
        }
    }
}

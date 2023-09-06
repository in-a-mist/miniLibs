using System;
using System.Collections.Generic;
using ComponentTest.Models.Columns;
using ComponentTest.Models.Utils;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace ComponentTest.Components
{
    public class ColumnCornerInComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ColumnCornerInComponent class.
        /// </summary>
        public ColumnCornerInComponent()
          : base("ColumnCornerIn", "C",
              "角金柱",
              "Test", "Columns")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item, Point3d.Origin);
            pManager.HideParameter(0);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "物件", GH_ParamAccess.item);
            pManager.AddPointParameter("Vertex", "V", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (!GlobalSettings.LookupSettings()) return;

            double diameter = GlobalSettings.GetInstance().ColumnDiameter;
            double height = GlobalSettings.GetInstance().ColumnHeight;
            Point3d position = Point3d.Unset;

            if (!DA.GetData(0, ref position)) return;

            //
            ColumnCornerIn column = new ColumnCornerIn(diameter, height);
            column.Position = position;


            //
            Brep result = column.Create();
            Point3d vertex = column.GetVertex();


            DA.SetData(0, result);
            DA.SetData(1, vertex);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6e9af0c1-3707-4ac3-9d9e-18a398492254"); }
        }
    }
}
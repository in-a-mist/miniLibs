using System;
using System.Collections.Generic;
using ComponentTest.Models.Utils;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace ComponentTest.Components
{
    public class SettingsComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SettingsComponent class.
        /// </summary>
        public SettingsComponent()
          : base("ArchiSettings", "S",
              "设置",
              "Test", "Base")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Diameter", "D", "外檐柱直径", GH_ParamAccess.item, 30.0);
            //pManager.AddNumberParameter("Height", "H", "外檐柱高度", GH_ParamAccess.item, 120.0);
            pManager.AddNumberParameter("DistanceOuter", "D", "廊步架", GH_ParamAccess.item, 100.0);
            pManager.AddNumberParameter("DistanceInner", "D", "金步架", GH_ParamAccess.item, 100.0);
            pManager.AddNumberParameter("DistanceTop", "D", "顶步架", GH_ParamAccess.item, 100.0);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Outer", "D", "廊步架", GH_ParamAccess.item);
            pManager.AddNumberParameter("Inner", "D", "金步架", GH_ParamAccess.item);
            pManager.AddNumberParameter("Top", "D", "顶步架", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double diameter = double.NaN;
            //double height = double.NaN;
            double disOuter= double.NaN;
            double disInner = double.NaN;
            double disTop = double.NaN;

            if (!DA.GetData(0, ref diameter)) return;
            //if (!DA.GetData(1, ref height)) return;
            if (!DA.GetData(1, ref disOuter)) return;
            if (!DA.GetData(2, ref disInner)) return;
            if (!DA.GetData(3, ref disTop)) return;

            GlobalSettings settings = GlobalSettings.GetInstance();
            settings.ColumnDiameter = diameter;
            settings.ColumnHeight = 11 * diameter;
            settings.DistanceOuterSpan = disOuter;
            settings.DistanceInnerSpan = disInner;
            settings.DistanceTopSpan = disTop;
            settings.ScaleRule = 32.0;
            if (Rhino.RhinoDoc.ActiveDoc.ModelUnitSystem == Rhino.UnitSystem.Meters)
            {
                settings.ScaleRule = 0.32;
            }
            

            //
            DA.SetData(0, disOuter);
            DA.SetData(1, disInner);
            DA.SetData(2, disTop);

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
            get { return new Guid("8efb0832-bd12-43ea-947d-c29e37bdbf3c"); }
        }
    }
}
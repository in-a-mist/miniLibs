using System;
using System.Collections.Generic;
using ComponentTest.Models.Beams;
using ComponentTest.Models.Utils;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace ComponentTest.Components
{
    public class BeamThreeSpanComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public BeamThreeSpanComponent()
          : base("BeamThreeSpan", "B",
              "三架梁",
              "Test", "Beams")
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

            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref position)) return;

            //
            BeamThreeSpan threeSpan = new BeamThreeSpan();
            threeSpan.Position = position;

            //
            Brep result = threeSpan.Create();
            Point3d vertex = threeSpan.GetVertex();


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
            get { return new Guid("876f6971-f483-45f5-b4f3-707308b1ff7a"); }
        }
    }
}
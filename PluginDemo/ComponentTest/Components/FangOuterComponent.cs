using System;
using System.Collections.Generic;
using ComponentTest.Models.Fangs;
using ComponentTest.Models.Utils;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace ComponentTest.Components
{
    public class FangOuterComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the FangOutterComponent1cs class.
        /// </summary>
        public FangOuterComponent()
          : base("FangOuter", "F",
              "檐枋金枋",
              "Test", "Fangs")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item, Point3d.Origin);
            pManager.AddNumberParameter("Length", "L", "长", GH_ParamAccess.item, 100);
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
            double length = double.NaN;

            if (!DA.GetData(0, ref position)) return;
            if (!DA.GetData(1, ref length)) return;

            //
            FangOuter fang = new FangOuter(length);
            fang.Position = position;

            //
            Brep result = fang.Create(GlobalSettings.GetInstance().DistanceOuterSpan);
            Point3d vertex = fang.GetVertex();


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
            get { return new Guid("a9da6697-6b0b-4455-8f84-d3c0829ffff1"); }
        }
    }
}
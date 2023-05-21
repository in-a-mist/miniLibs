using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using miniLibs;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace pluginForGrasshopper

{
    public class LuTouComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public LuTouComponent()
          : base("LuTou", "栌枓",
              "栌枓",
              "WoodFramework", "Primitive")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            CaiFenRule rule = new CaiFenRule(size);
            LuTou lutoudata = new LuTou(rule);
            Tou lutou = new Tou(lutoudata, position);

            var v = lutou.GetShape();
            v.Translate(new Vector3d(position));

            DA.SetData(0, v);
            DA.SetData(1, lutou.TopPoint);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("467e8a10-fe89-422c-a53f-8bba50799820"); }
        }
    }

    public class HuaKungComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public HuaKungComponent()
          : base("HuaKong", "华栱",
              "华栱",
              "WoodFramework", "Primitive")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "截面尺寸宽度", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Geometry", "G", "模型", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "点", GH_ParamAccess.item);
            pManager.HideParameter(1);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = 0;
            Point3d position = Point3d.Unset;
            if (!DA.GetData(0, ref size)) return;
            if (size <= 0) return;
            if (!DA.GetData(1, ref position)) return;

            CaiFenRule rule = new CaiFenRule(size);
            HuaKung huaKongData = new HuaKung(rule);
            Kong huaKong = new Kong(huaKongData, position);
            var v = huaKong.GetShape();
            v.Translate(new Vector3d(position));
            DA.SetData(0, v);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3eab81e6-1422-4741-a140-0682bae3eba7"); }
        }
    }
}


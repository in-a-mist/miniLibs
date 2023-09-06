using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Utils
{
    public class DocTolerance
    {
        public static double ModelToler => Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
    }
}

using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Utils
{
    /// <summary>
    /// 悬山建筑基础尺寸计算
    /// </summary>
    public class GlobalSettings
    {

        private static GlobalSettings instance = new GlobalSettings();
        private GlobalSettings() { }
        public static GlobalSettings GetInstance()
        {
            if (instance == null)
            {
                instance = new GlobalSettings();
            }
            return instance;
        }

        /// <summary>
        /// 外檐柱直径
        /// </summary>
        public double ColumnDiameter { get; set; }
        /// <summary>
        /// 外檐柱高度
        /// </summary>
        public double ColumnHeight { get; set; }
        /// <summary>
        /// 檩径
        /// </summary>
        public double RoofCylinderDiameter => ColumnDiameter;
        /// <summary>
        /// 廊步架
        /// </summary>
        public double DistanceOuterSpan { get; set; }
        /// <summary>
        /// 金步架
        /// </summary>
        public double DistanceInnerSpan { get; set; }
        /// <summary>
        /// 顶步架
        /// </summary>
        public double DistanceTopSpan { get; set; }
        /// <summary>
        /// 营造尺
        /// </summary>
        public double ScaleRule { get; set; }
        /// <summary>
        /// 举架
        /// </summary>
        public double RaiseHeight { get; set; }

        public static bool LookupSettings()
        {
            bool flag = false;
            var components = Grasshopper.Instances.ActiveCanvas.Document.Objects;

            foreach (var item in components)
            {
                if (item.Name == "ArchiSettings")
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
    }
}

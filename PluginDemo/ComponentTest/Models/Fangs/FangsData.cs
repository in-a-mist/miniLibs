using ComponentTest.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentTest.Models.Fangs
{
    /// <summary>
    /// 枋类
    /// </summary>
    public enum FangsType
    {
        /// <summary>
        /// 檐枋、金枋
        /// </summary>
        OutterInner,
        /// <summary>
        /// 金枋,脊枋
        /// </summary>
        InnerTop,
        /// <summary>
        /// 穿插枋
        /// </summary>
        InsertIn,
        /// <summary>
        /// 燕尾枋
        /// </summary>
        SwallowtailSmall,
        SwallowtailBigger,
    }


    public class FangsData
    {
        public double Length { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public FangsData(FangsType fangsType)
        {
            GlobalSettings settings = GlobalSettings.GetInstance();

            switch (fangsType)
            {
                case FangsType.OutterInner:
                    Height = settings.ColumnDiameter;
                    Width =0.8 * settings.ColumnDiameter;
                    break;
                case FangsType.InnerTop:
                    Height = 0.8 * settings.ColumnDiameter;
                    Width = 0.65 * settings.ColumnDiameter;
                    break;
                case FangsType.InsertIn:
                    Height = settings.ColumnDiameter;
                    Width = 0.8 * settings.ColumnDiameter;
                    Length=settings.DistanceOuterSpan + 2 * settings.ColumnDiameter;
                    break;
                case FangsType.SwallowtailSmall:
                    Height =0.65 * settings.ColumnDiameter;
                    Width = 0.25 * settings.ColumnDiameter;
                    break;
                case FangsType.SwallowtailBigger:
                    Height = 0.8 * settings.ColumnDiameter;
                    Width = 0.25 * settings.ColumnDiameter;
                    break;
                default:
                    break;
            }
        }
    }
}

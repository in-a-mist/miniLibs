using ComponentTest.Models.Utils;

namespace ComponentTest.Models.Beams
{
    /// <summary>
    /// 一跨距的梁即抱头梁;
    /// 两跨距的梁即顶梁（卷棚顶）;
    /// 余同名;
    /// </summary>
    public enum BeamsType
    {
        OneSpan,
        TwoSpan,
        ThreeSpan,
        FourSpan,
        FiveSpan,
        SixSpan,
        SeveSpan,
    }


    public class BeamsData
    {
        public double Length { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public BeamsData(BeamsType beamsType)
        {
            GlobalSettings settings = GlobalSettings.GetInstance();
            switch (beamsType)
            {
                case BeamsType.OneSpan:
                    Length = 1.0 * settings.DistanceOuterSpan + 1.0 * settings.ColumnDiameter;
                    Height = 1.5 * settings.ColumnDiameter;
                    Width = 1.1 * settings.ColumnDiameter;
                    break;
                case BeamsType.TwoSpan:
                    Length = 1.0 * settings.DistanceTopSpan + 2.0 * settings.ColumnDiameter;
                    Height = 1.16 * settings.ColumnDiameter;
                    Width = 0.76 * settings.ColumnDiameter;
                    break;
                case BeamsType.ThreeSpan:
                    Length = 2.0 * settings.DistanceInnerSpan + 2.0 * settings.ColumnDiameter;
                    Height = 1.25 * settings.ColumnDiameter;
                    Width = 0.95 * settings.ColumnDiameter;
                    break;
                case BeamsType.FourSpan:
                    Length = 3.0 * settings.DistanceInnerSpan + 2.0 * settings.ColumnDiameter;
                    Height = 1.4 * settings.ColumnDiameter;
                    Width = 1.1 * settings.ColumnDiameter;
                    break;
                case BeamsType.FiveSpan:
                    Length = 4.0 * settings.DistanceInnerSpan + 2.0 * settings.ColumnDiameter;
                    Height = 1.5 * settings.ColumnDiameter;
                    Width = 1.2 * settings.ColumnDiameter;
                    break;
                case BeamsType.SixSpan:
                    Length = 5.0 * settings.DistanceInnerSpan + 2.0 * settings.ColumnDiameter;
                    Height = 1.5 * settings.ColumnDiameter;
                    Width = 1.2 * settings.ColumnDiameter;
                    break;
                case BeamsType.SeveSpan:
                    Length = 6.0 * settings.DistanceInnerSpan + 2.0 * settings.ColumnDiameter;
                    Height = 1.8 * settings.ColumnDiameter;
                    Width = 1.5 * settings.ColumnDiameter;
                    break;
                default:
                    break;
            }
        }
    }
}

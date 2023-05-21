namespace miniLibs
{
    //古尺单位转化公制单位；例如：
    //唐宋营造尺按 1尺= 0.3m = 30cm 计算；
    //明清营造尺按 1尺= 0.32m = 32cm 计算

    /// <summary>
    /// 营造尺单位转化公制单位
    /// </summary>
    public interface IUnitConvert
    {

        /// <summary>
        /// 营造尺单位换算比率，以厘米计；即1尺=？厘米；
        /// </summary>
        double Ratio { get; }
        /// <summary>
        /// 尺换算成厘米
        /// <para name="chi">chi：尺</para>
        /// </summary>
        double CHI2CM(double chi);
        /// <summary>
        /// 寸换算成厘米
        /// <para name="cun">cun：寸</para>
        /// </summary>
        double CUN2CM(double cun);
    }

    /// <summary>
    /// 材份制度或斗口制计算关系
    /// </summary>
    public interface ICalculatorRule
    {
        /// <summary>
        /// 度量单位数值；
        /// 度量单位：由用材等级截面尺寸计算得出；
        /// </summary>
        double UnitValue { get; }
        /// <summary>
        /// 足材(高度)；
        /// 按宋代截面高度为21个度量单位即21份；
        /// 按清代截面高度为2个度量单位即2斗口；
        /// 单材+栔高=足材；
        /// </summary>
        double EnoughSize { get; }   //足材
        /// <summary>
        /// 单材(高度)；
        /// 按宋代为15份；清代为1.4斗口
        /// </summary>
        double SingleSize { get; }   //单材
        /// <summary>
        /// 栔值(高度)；
        /// 按宋代为6份；清代为0.6斗口
        /// </summary>
        double GapSize { get; }   //栔高
        /// <summary>
        /// 材厚(厚度)；
        /// 按宋代为10份；清代为1.0，1.25斗口不等
        /// </summary>
        double WidthSize { get; }   //材厚
    }

    /// <summary>
    /// 宋材份制;
    /// </summary>
    public class CaiFenRule : IUnitConvert, ICalculatorRule
    {
        private double _sectionWidth;

        /// <summary>
        /// 材份制：单位份值由用材截面宽度计算得出以厘米为单位；
        /// 木材截面宽度以寸为单位
        /// </summary>
        /// <param name="sectionWidth">木材截面宽度：寸</param>
        public CaiFenRule(double sectionWidth)
        {
            _sectionWidth = sectionWidth;
        }
        public  double Ratio => 30;
        public double CHI2CM(double chi) => Ratio * chi;
        public double CUN2CM(double cun) => CHI2CM(0.1 * cun);
        public double UnitValue => this.CUN2CM(_sectionWidth) * 0.1;
        public double EnoughSize => 21 * UnitValue;
        public double SingleSize => 15 * UnitValue;
        public double GapSize => 6 * UnitValue;
        public double WidthSize => 10 * UnitValue;
    }


}

namespace miniLibs
{
    /// <summary>
    /// 枓的属性：名称、长、宽、高、
    /// (耳、腰、底、内凹、底面杀分,可以不需要，这些数据跟其高度关联起来也可以)
    /// </summary>
    public interface ITou
    {
        string Name { get; }
        double Length { get; }
        double Width { get; }
        double Height { get; }
        double Upper { get; }
        double Middle { get; }
        double Lower { get; }
        double InnerOffset { get; }
        double CutShrink { get; }
    }

    /// <summary>
    /// 栱的属性：名称、外挑长、里挑长、总长、是否足材、卷杀高度、卷杀瓣数、卷杀总长度
    /// </summary>
    public interface IKung
    {
        string Name { get; }
        double ExtendOuter { get; }
        double ExtendInner { get; }
        double FullLength { get; }
        double Width { get; }
        double Height { get; }
        bool IsEnough { get; }
        double CutHeight { get; }
        double CutAmount { get; }
        double CutLength { get; }

    }

    //栌枓
    public class LuTou : ITou
    {
        private ICalculatorRule _rule;
        public LuTou(ICalculatorRule rule)
        {
            _rule = rule;
        }
        public string Name => "宋式栌枓";
        public double Length => 32 * _rule.UnitValue;
        public double Width => 32 * _rule.UnitValue;
        public double Height => 20 * _rule.UnitValue;
        public double Upper => 0.4 * Height;
        public double Middle => 0.2 * Height;
        public double Lower => 0.4 * Height;
        public double InnerOffset => 0.05 * Height;
        public double CutShrink => 0.2 * Height;
    }


    //华栱
    public class HuaKung : IKung
    {
        private ICalculatorRule _rule;
        public HuaKung(ICalculatorRule rule)
        {
            _rule = rule;
        }
        public string Name => "宋式华栱";
        public double ExtendOuter => 31 * _rule.UnitValue;
        public double ExtendInner => 31 * _rule.UnitValue;
        public double FullLength => ExtendOuter + ExtendInner;
        public double Width => _rule.WidthSize;
        public double Height => IsEnough ? _rule.EnoughSize : _rule.SingleSize;
        public bool IsEnough => true;
        public double CutHeight => 9 * _rule.UnitValue;
        public double CutAmount => 4;
        public double CutLength => 4 * _rule.UnitValue;
    }
}

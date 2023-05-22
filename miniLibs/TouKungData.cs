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
        double CutFullLength { get; }

    }

    public abstract class TouData : ITou
    {
        public ICalculatorRule _rule;
        public TouData(ICalculatorRule rule)
        {
            _rule = rule;
        }
        public abstract string Name { get; }
        public abstract double Length { get; }
        public abstract double Width { get; }
        public abstract double Height { get; }
        public double Upper => 0.4 * Height;
        public double Middle => 0.2 * Height;
        public double Lower => 0.4 * Height;
        public double InnerOffset => 0.05 * Height;
        public double CutShrink => 0.2 * Height;
    }

    public class LuTouData : TouData
    {
        public LuTouData(ICalculatorRule rule) : base(rule)
        {
        }
        public override string Name => "宋式栌枓";
        public override double Length => 32 * _rule.UnitValue;
        public override double Width => 32 * _rule.UnitValue;
        public override double Height => 20 * _rule.UnitValue;

    }

    public class ShanTouData : TouData
    {
        public ShanTouData(ICalculatorRule rule) : base(rule)
        {
        }
        public override string Name => "宋式散枓";
        public override double Length => 14 * _rule.UnitValue;
        public override double Width => 16 * _rule.UnitValue;
        public override double Height => 10 * _rule.UnitValue;
    }

    public class ChiaoHuTouData : TouData
    {
        public ChiaoHuTouData(ICalculatorRule rule) : base(rule)
        {
        }
        public override string Name => "宋式交互枓";
        public override double Length => 18 * _rule.UnitValue;
        public override double Width => 16 * _rule.UnitValue;
        public override double Height => 10 * _rule.UnitValue;
    }

    public class ChiSinTouData : TouData
    {
        public ChiSinTouData(ICalculatorRule rule) : base(rule)
        {
        }
        public override string Name => "宋式齐心枓";
        public override double Length => 16 * _rule.UnitValue;
        public override double Width => 16 * _rule.UnitValue;
        public override double Height => 10 * _rule.UnitValue;
    }



    //*************************************************************

    public abstract class KungData : IKung
    {
        public ICalculatorRule _rule;
        public KungData(ICalculatorRule rule)
        {
            _rule = rule;
        }
        public abstract string Name { get; }
        public abstract double ExtendOuter { get; }
        public abstract double ExtendInner { get; }
        public double FullLength => ExtendOuter + ExtendInner;
        public double Width => _rule.WidthSize;
        public double Height => IsEnough ? _rule.EnoughSize : _rule.SingleSize;
        public abstract bool IsEnough { get; }
        public abstract double CutHeight { get; }
        public abstract double CutAmount { get; }
        public abstract double CutFullLength { get; }
    }

    public class HuaKungData : KungData
    {
        public HuaKungData(ICalculatorRule rule) : base(rule)
        {
        }

        public override string Name => "宋式华栱";
        public override double ExtendOuter => 36 * _rule.UnitValue;
        public override double ExtendInner => 36 * _rule.UnitValue;
        public override bool IsEnough => true;
        public override double CutHeight => 9 * _rule.UnitValue;
        public override double CutAmount => 4;
        public override double CutFullLength => 16 * _rule.UnitValue;
    }

    public class NiTaoKungData : KungData
    {
        public NiTaoKungData(ICalculatorRule rule) : base(rule)
        {
        }

        public override string Name => "宋式泥道栱";
        public override double ExtendOuter => 31 * _rule.UnitValue;
        public override double ExtendInner => 31 * _rule.UnitValue;
        public override bool IsEnough => true;
        public override double CutHeight => 9 * _rule.UnitValue;
        public override double CutAmount => 4;
        public override double CutFullLength => 14 * _rule.UnitValue;
    }

    public class KuaTzuKungData : KungData
    {
        public KuaTzuKungData(ICalculatorRule rule) : base(rule)
        {
        }

        public override string Name => "宋式瓜子栱";
        public override double ExtendOuter => 31 * _rule.UnitValue;
        public override double ExtendInner => 31 * _rule.UnitValue;
        public override bool IsEnough => false;
        public override double CutHeight => 9 * _rule.UnitValue;
        public override double CutAmount => 4;
        public override double CutFullLength => 16 * _rule.UnitValue;
    }

    public class LingKungData : KungData
    {
        public LingKungData(ICalculatorRule rule) : base(rule)
        {
        }

        public override string Name => "宋式令栱";
        public override double ExtendOuter => 36 * _rule.UnitValue;
        public override double ExtendInner => 36 * _rule.UnitValue;
        public override bool IsEnough => false;
        public override double CutHeight => 9 * _rule.UnitValue;
        public override double CutAmount => 5;
        public override double CutFullLength => 20 * _rule.UnitValue;
    }

    public class ManKungData : KungData
    {
        public ManKungData(ICalculatorRule rule) : base(rule)
        {
        }

        public override string Name => "宋式慢栱";
        public override double ExtendOuter => 46 * _rule.UnitValue;
        public override double ExtendInner => 46 * _rule.UnitValue;
        public override bool IsEnough => false;
        public override double CutHeight => 9 * _rule.UnitValue;
        public override double CutAmount => 4;
        public override double CutFullLength => 12 * _rule.UnitValue;
    }
}

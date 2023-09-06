using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniLibs
{

    //铺作里外出跳数据表用数组表示(宋代铺作出跳暂没想到合适的生成方法，唯穷举)
    //计算里外跳总长
    public abstract class SetExtendData
    {
        public abstract int ExtendAmount { get; }
        public ICalculatorRule _rule;
        public SetExtendData(ICalculatorRule rule)
        {
            _rule = rule;
        }

        public abstract double[] OutterValue();
        public abstract double[] InnerValue();
        public double OutterLength
        {
            get
            {
                double sum = 0;
                foreach (double item in OutterValue())
                {
                    sum += item;
                }
                return sum;
            }
        }

        public double InnerLength
        {
            get
            {
                double sum = 0;
                foreach (double item in InnerValue())
                {
                    sum += item;
                }
                return sum;
            }
        }
    }
    public class SetWith4:SetExtendData
    {
        public SetWith4(ICalculatorRule rule) : base(rule)
        {
        }

        public override int ExtendAmount => 1;
        public override double[] OutterValue()
        {
            double[] extend = new double[1];
            extend[0] = 30 * _rule.UnitValue;
            return extend;
        }
        public override double[] InnerValue()
        {
            double[] extend = new double[1];
            extend[0] = 30 * _rule.UnitValue;
            return extend;
        }

    }

    public class SetWith5 : SetExtendData
    {
        public SetWith5(ICalculatorRule rule) : base(rule)
        {
        }

        public override int ExtendAmount => 2;
        public override double[] OutterValue()
        {
            double[] extend = new double[2];
            extend[0] = 30 * _rule.UnitValue;
            extend[1] = 30 * _rule.UnitValue;
            return extend;
        }
        public override double[] InnerValue()
        {
            double[] extend = new double[2];
            extend[0] = 30 * _rule.UnitValue;
            extend[1] = 30 * _rule.UnitValue;
            return extend;
        }

    }

    public class SetWith6 : SetExtendData
    {
        public SetWith6(ICalculatorRule rule) : base(rule)
        {
        }

        public override int ExtendAmount => 3;
        public override double[] OutterValue()
        {
            double[] extend = new double[3];
            extend[0] = 30 * _rule.UnitValue;
            extend[1] = 30 * _rule.UnitValue;
            extend[2] = 30 * _rule.UnitValue;
            return extend;
        }
        public override double[] InnerValue()
        {
            double[] extend = new double[2];
            extend[0] = 30 * _rule.UnitValue;
            extend[1] = 30 * _rule.UnitValue;
            return extend;
        }

    }

    public class SetWith7 : SetExtendData
    {
        public SetWith7(ICalculatorRule rule) : base(rule)
        {
        }

        public override int ExtendAmount => 4;
        public override double[] OutterValue()
        {
            double[] extend = new double[4];
            extend[0] = 30 * _rule.UnitValue;
            extend[1] = 26 * _rule.UnitValue;
            extend[2] = 26 * _rule.UnitValue;
            extend[3] = 26 * _rule.UnitValue;
            return extend;
        }
        public override double[] InnerValue()
        {
            double[] extend = new double[3];
            extend[0] = 28 * _rule.UnitValue;
            extend[1] = 26 * _rule.UnitValue;
            extend[2] = 26 * _rule.UnitValue;
            return extend;
        }

    }

    public class SetWith8 : SetExtendData
    {
        public SetWith8(ICalculatorRule rule) : base(rule)
        {
        }

        public override int ExtendAmount => 5;
        public override double[] OutterValue()
        {
            double[] extend = new double[5];
            extend[0] = 30 * _rule.UnitValue;
            extend[1] = 26 * _rule.UnitValue;
            extend[2] = 26 * _rule.UnitValue;
            extend[3] = 26 * _rule.UnitValue;
            extend[4] = 26 * _rule.UnitValue;
            return extend;
        }
        public override double[] InnerValue()
        {
            double[] extend = new double[3];
            extend[0] = 28 * _rule.UnitValue;
            extend[1] = 26 * _rule.UnitValue;
            extend[2] = 26 * _rule.UnitValue;
            return extend;
        }

    }




    //********************************************
    //********************************************
    //一个不通用的宋式铺作出跳计算(以下昂为例子)
    public class SetExtendedData
    {
        private int _extendAmount;
        public SetExtendedData(int setLevel)
        {
            if (setLevel < 4|| setLevel>8) return;
            _extendAmount = setLevel - 3;
        }
        public double[] OutterValue
        {
            get
            {
                double[] outter = new double[5];
                switch (_extendAmount)
                {
                    case 1:
                    case 2:
                    case 3:
                        //outter = new double[_extendAmount];
                        for (int i = 0; i < _extendAmount; i++)
                        {
                            outter[i] = 30;
                        }
                        break;
                    case 4:
                    case 5:
                        //outter = new double[_extendAmount];
                        for (int i = 0; i < _extendAmount; i++)
                        {
                            outter[i] = 26;
                        }
                        outter[0] = 30;
                        break;
                    
                }

                return outter;

            }
        }
        public double[] InnerValue
        {
            get
            {
                double[] inner = new double[3];
                switch (_extendAmount)
                {
                    case 1:
                        //inner = new double[1];
                        inner[0] = 30;
                        break;
                    case 2:
                    case 3:
                        //inner = new double[2];
                        for (int i = 0; i < 2; i++)
                        {
                            inner[i] = 30;
                        }
                        break;
                    case 4:
                    case 5:
                        //inner = new double[3];
                        for (int i = 0; i < 3; i++)
                        {
                            inner[i] = 26;
                        }
                        inner[0] = 28;
                        break;
                }
                return inner;

            }
        }
        public double OutterLength
        {
            get
            {
                double sum = 0;
                foreach (double item in OutterValue)
                {
                    sum += item;
                }
                return sum;
            }
        }
        public double InnerLength
        {
            get
            {
                double sum = 0;
                foreach (double item in InnerValue)
                {
                    sum += item;
                }
                return sum;
            }
        }
    }
}

using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;
using System.Windows.Forms;


namespace ArchitectElementsLibrary
{
    public class Grids : GH_Component
    {
        //protected List<Plane> yPlane = new List<Plane>();
        //protected List<Plane> xPlane = new List<Plane>();
        //protected List<string> yTexts = new List<string>();
        //protected List<string> xTexts = new List<string>();
        //protected List<Curve> yCurveDispaly = new List<Curve>();
        //protected List<Curve> xCurveDispaly = new List<Curve>();


        public Grids()
          : base("Grids", "G",
              "平面轴网",
              "Test", "Base")
        {
            Message = "轴网";
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("明间", "明间", "明间", GH_ParamAccess.item);
            pManager.AddNumberParameter("次间", "次间", "次间", GH_ParamAccess.list);
            pManager.AddNumberParameter("尽间", "尽间", "尽间", GH_ParamAccess.item);
            pManager.AddNumberParameter("进深", "进深", "进深", GH_ParamAccess.list);
            pManager.AddNumberParameter("副阶", "副阶", "副阶，数值为零则没有副阶", GH_ParamAccess.item);
            pManager.AddIntegerParameter("类型", "类型", "类型,1-单槽，2-双槽，3-分心槽，1-金厢斗底槽", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("殿身点", "殿身", "殿身", GH_ParamAccess.list);
            pManager.AddPointParameter("副阶点", "副阶", "副阶", GH_ParamAccess.list);
            pManager.AddIntegerParameter("开间数", "开间", "开间", GH_ParamAccess.item);
            pManager.AddIntegerParameter("进深数", "进深", "进深", GH_ParamAccess.item);
            pManager.AddIntegerParameter("副阶数", "副阶", "副阶", GH_ParamAccess.item);

        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {

            double middleDis = double.NaN,
                endDis = double.NaN,
                surroDis = double.NaN;
            List<double> jiaShengDis = new List<double>();
            List<double> ciJianDis = new List<double>();

            int typeNum = 0;


            DA.GetData(0, ref middleDis);
            DA.GetDataList(1, ciJianDis);
            DA.GetData(2, ref endDis);
            DA.GetDataList(3, jiaShengDis);
            DA.GetData(4, ref surroDis);
            DA.GetData(5, ref typeNum);




            if (middleDis <= 0) middleDis = 0;
            if (ciJianDis == null) ciJianDis = new List<double>() { 0 };
            if (endDis <= 0) endDis = 0;
            if (jiaShengDis == null) jiaShengDis = new List<double>() { 0 };
            if (surroDis <= 0) surroDis = 0;

            //在Y轴方向上创建系列点
            List<Point2d> ptsYAxis = new List<Point2d>()
            {
                new Point2d(0,0),
            };

            List<double> yAxisDis = new List<double>
            {
                surroDis,
            };

            yAxisDis.AddRange(jiaShengDis);
            yAxisDis.Add(surroDis);

            double sumYAxisDis = 0;
            foreach (var item in yAxisDis)
            {
                sumYAxisDis += item;
                ptsYAxis.Add(new Point2d(0, sumYAxisDis));

            }

            ptsYAxis = ptsYAxis.Distinct<Point2d>().ToList<Point2d>();

            //沿X轴方向上复制之前创建的系列点

            List<double> xAxisDis = new List<double>
            {
                surroDis,
                endDis,
            };
            ciJianDis.Reverse();
            xAxisDis.AddRange(ciJianDis);
            xAxisDis.Add(middleDis);
            ciJianDis.Reverse();
            xAxisDis.AddRange(ciJianDis);
            xAxisDis.Add(endDis);
            xAxisDis.Add(surroDis);

            List<Point2d> ptsXAxis = new List<Point2d>();

            double sumXAxisDis = 0;
            foreach (var item in xAxisDis)
            {
                sumXAxisDis += item;
                foreach (var ptY in ptsYAxis)
                {
                    Point2d ptX = new Point2d(sumXAxisDis, ptY.Y);
                    ptsXAxis.Add(ptX);
                }

            }
            ptsXAxis = ptsXAxis.Distinct<Point2d>().ToList<Point2d>();


            //按平面类型筛选点
            List<Point2d> ptsResult = new List<Point2d>();
            foreach (var item in ptsYAxis)
            {
                ptsResult.Add(item);
            }
            foreach (var item in ptsXAxis)
            {
                ptsResult.Add(item);
            }
            ptsResult = ptsResult.Distinct<Point2d>().ToList<Point2d>();



            double xDisMin = xAxisDis[0] + xAxisDis[1];
            double xDisMax = 0;
            for (int i = 0; i < xAxisDis.Count - 2; i++)
            {
                xDisMax += xAxisDis[i];
            }

            switch (typeNum)
            {
                case 1:
                    if (endDis <= 0)
                    {
                        MessageBox.Show("尽间面阔不应为零值");
                        return;
                    }
                    double yDis1 = yAxisDis[0] + yAxisDis[1];
                    var subPts11 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis1);
                    ptsResult = ptsResult.Except(subPts11).ToList();
                    break;

                case 3:
                    if (yAxisDis.Count <= 4 || endDis <= 0)
                    {
                        MessageBox.Show("若分心槽，进深间数应多于三，尽间面阔不应为零值");
                        return;
                    }
                    double yDis21 = yAxisDis[0] + yAxisDis[1];
                    double yDis22 = yAxisDis[0] + yAxisDis[1] + yAxisDis[2] + yAxisDis[3];
                    var subPts21 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis21);
                    var subPts22 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis22);
                    ptsResult = ptsResult.Except(subPts21).Except(subPts22).ToList();
                    break;

                case 2:
                    if (yAxisDis.Count <= 4 || endDis <= 0)
                    {
                        MessageBox.Show("若双槽，进深间数应多于三，尽间面阔不应为零值");
                        return;
                    }
                    double yDis31 = yAxisDis[0] + yAxisDis[1] + yAxisDis[2];
                    var subPts31 = ptsResult.Where(pt => pt.X >= xDisMin && pt.X <= xDisMax && pt.Y == yDis31);
                    ptsResult = ptsResult.Except(subPts31).ToList();
                    break;

                case 4:

                    double xDisMin4 = xAxisDis[0] + xAxisDis[1] + yAxisDis[2];
                    double xDisMax4 = 0;
                    for (int i = 0; i < xAxisDis.Count - 3; i++)
                    {
                        xDisMax4 += xAxisDis[i];
                    }

                    if (yAxisDis.Count <= 4 || endDis <= 0 || ciJianDis[0] <= 0)
                    {
                        MessageBox.Show("若金厢斗底槽，面阔、进深间数应多于三，次间、尽间面阔不应为零值");
                        return;
                    }
                    double yDis41 = yAxisDis[0] + yAxisDis[1] + yAxisDis[2];
                    var subPts41 = ptsResult.Where(pt => pt.X >= xDisMin4 && pt.X <= xDisMax4 && pt.Y == yDis41);
                    ptsResult = ptsResult.Except(subPts41).ToList();
                    break;


                default:
                    break;
            }

            //输出需要的数据
            DA.SetData(2, xAxisDis.Count - 2);
            DA.SetData(3, yAxisDis.Count - 2);

            //
            if (surroDis != 0)
            {
                var fujiePts = ptsResult.Where(pt => pt.X == ptsResult[0].X || pt.X == ptsResult.Last().X || pt.Y == ptsYAxis[0].Y || pt.Y == ptsYAxis.Last().Y);
                var diansPts = ptsResult.Except(fujiePts).ToList();
                DA.SetDataList(0, diansPts);
                DA.SetDataList(1, fujiePts);
                DA.SetData(4, 2);
            }
            else
            {
                DA.SetDataList(0, ptsResult);
                DA.SetData(4, 0);
            }
        }
        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("0550E61D-3194-4B20-8A3A-5E5B5AABFEE6");
    }
}

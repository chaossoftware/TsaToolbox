using ChaosSoft.Core.Data;
using ChaosSoft.Core.IO;
using ChaosSoft.NumericalMethods;
using ChaosSoft.NumericalMethods.Extensions;
using ChaosSoft.NumericalMethods.Lyapunov;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TsaToolbox.Properties;

namespace TsaToolbox
{
    internal class LyapunovExponents
    {
        private const string DefaultStartPoint = "1";

        private readonly double[] _zero = new double[] { 0 };
        
        public LyapunovExponents()
        {
        }

        public ITimeSeriesLyapunov Method { get; set; }

        public void ExecuteLyapunovMethod(MainWindow wnd, double[] series)
        {
            try
            {
                wnd.Dispatcher.Invoke(() =>
                {
                    wnd.le_resultTbox.Background = Brushes.Gold;
                    wnd.le_resultTbox.Text = Resources.Calculating;
                });

                Method.Calculate(series);
                wnd.Dispatcher.Invoke(() => SetLyapunovResult(wnd));
            }
            catch (Exception ex)
            {
                wnd.Dispatcher.Invoke(() =>
                {
                    wnd.le_logTbox.Text = ex.ToString();
                    wnd.le_resultTbox.Background = Brushes.Coral;
                    wnd.le_resultTbox.Text = Resources.Error;
                });
            }
        }

        public void AdjustSlope(MainWindow wnd)
        {
            try
            {
                if (Method is LleKantz k)
                {
                    if (string.IsNullOrEmpty(wnd.le_k_epsCombo.Text))
                    {
                        return;
                    }

                    k.SetSlope(wnd.le_k_epsCombo.Text);
                }

                var res = FillLyapunovChart(wnd);

                if (Method is LleKantz || Method is LleRosenstein)
                {
                    wnd.le_resultTbox.Text = res;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.LePlotError + Environment.NewLine + ex.Message);
            }
        }

        public void CleanUp(MainWindow wnd)
        {
            Method = null;

            wnd.le_mainSlopeChart.Plot(_zero, _zero);
            wnd.le_secondarySlopeChart.Plot(_zero, _zero);

            wnd.le_logTbox.Text = string.Empty;
            wnd.le_resultTbox.Text = string.Empty;
            wnd.le_resultTbox.Background = Brushes.LightGray;
        }

        private void SetLyapunovResult(MainWindow wnd)
        {
            wnd.le_resultTbox.Background = Brushes.LightGreen;
            string result;

            if (Method is LeSpecSanoSawada ss)
            {
                wnd.le_resultTbox.Text = Format.General(ss.Result, " ", 6);
            }
            else
            {
                wnd.le_resultTbox.Text = (Method as IDescribable).GetResultAsString();
            }
           
            wnd.le_logTbox.Text = Method.ToString() + "\n\nResult:\n" + (Method as IDescribable).GetResultAsString() + "\n\nLog:\n" + Method.Log.ToString();

            if (Method is LleKantz || Method is LleRosenstein)
            {
                wnd.le_kantzResultGbox.Visibility = Visibility.Visible;
            }
            else
            {
                wnd.le_kantzResultGbox.Visibility = Visibility.Hidden;
            }

            if (Method is LleKantz k)
            {
                k.SetSlope(k.SlopesList.Keys.First());
                wnd.le_k_epsCombo.ItemsSource = k.SlopesList.Keys;
                wnd.le_k_epsCombo.SelectedIndex = 0;
            }

            if (Method.Slope.Length > 1)
            {
                try
                {
                    if (Method is LleKantz || Method is LleRosenstein)
                    {
                        var leSectorEnd = DataSeriesUtils.SlopeChangePointIndex(Method.Slope, 3, Method.Slope.Amplitude.Y / 30);

                        if (leSectorEnd <= 0)
                        {
                            leSectorEnd = Method.Slope.Length;
                        }

                        wnd.le_k_endTbox.Text = leSectorEnd.ToString();
                        wnd.le_k_startTbox.Text = DefaultStartPoint;
                    }

                    result = FillLyapunovChart(wnd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.LePlotError + Environment.NewLine + ex.Message);
                    result = Resources.Nda;
                }
            }
            else
            {
                result = Resources.Nda;
            }

            if (Method is LleKantz || Method is LleRosenstein)
            {
                wnd.le_resultTbox.Text = result;
            }
        }

        private string FillLyapunovChart(MainWindow wnd)
        {
            wnd.le_mainSlopeChart.Plot(_zero, _zero);
            wnd.le_secondarySlopeChart.Plot(_zero, _zero);

            var result = string.Empty;

            wnd.le_slopeChart.BottomTitle = "t";
            wnd.le_mainSlopeChart.Plot(Method.Slope.XValues, Method.Slope.YValues);

            if (Method is LleWolf)
            {
                wnd.le_slopeChart.LeftTitle = "LE";
                wnd.le_slopeChartTitle.Text = "Lyapunov Exponent in Time";
            }
            else
            {
                var startPoint = wnd.le_k_startTbox.ReadInt() - 1;
                var endPoint = wnd.le_k_endTbox.ReadInt() - 1;

                var tsSector = new DataSeries();

                tsSector.AddDataPoint(Method.Slope.DataPoints[startPoint].X, Method.Slope.DataPoints[startPoint].Y);
                tsSector.AddDataPoint(Method.Slope.DataPoints[endPoint].X, Method.Slope.DataPoints[endPoint].Y);

                wnd.le_slopeChart.LeftTitle = "Slope";
                wnd.le_slopeChartTitle.Text = "Lyapunov Function";
                wnd.le_secondarySlopeChart.Plot(tsSector.XValues, tsSector.YValues);

                var slope = Math.Atan2(Method.Slope.DataPoints[endPoint].Y - Method.Slope.DataPoints[startPoint].Y, Method.Slope.DataPoints[endPoint].X - Method.Slope.DataPoints[startPoint].X);
                result = Format.General(slope);
            }

            return result;
        }
    }
}

using MathLib;
using MathLib.Data;
using MathLib.NumericalMethods.Lyapunov;
using System;
using System.Windows;
using System.Windows.Media;

namespace TimeSeriesToolbox
{
    internal class LyapunovExponents
    {
        private const string DefaultStartPoint = "1";

        private readonly double[] _zero = new double[] { 0 };
        
        public LyapunovExponents()
        {
        }

        public LyapunovMethod Method { get; set; }

        public void ExecuteLyapunovMethod(MainWindow wnd)
        {
            try
            {
                wnd.Dispatcher.Invoke(() =>
                {
                    wnd.le_resultTbox.Background = Brushes.Gold;
                    wnd.le_resultTbox.Text = StringData.Calculating;
                });

                Method.Calculate();
                wnd.Dispatcher.Invoke(() => SetLyapunovResult(wnd));
            }
            catch (Exception ex)
            {
                wnd.Dispatcher.Invoke(() =>
                {
                    wnd.le_logTbox.Text = ex.ToString();
                    wnd.le_resultTbox.Background = Brushes.Coral;
                    wnd.le_resultTbox.Text = Properties.Resources.Error;
                });
            }
        }

        public void AdjustSlope(MainWindow wnd)
        {
            try
            {
                if (Method is KantzMethod)
                {
                    ((KantzMethod)Method).SetSlope(wnd.le_k_epsCombo.Text);
                }

                var res = FillLyapunovChart(wnd);

                if (Method is KantzMethod || Method is RosensteinMethod)
                {
                    wnd.le_resultTbox.Text = res;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.LePlotError + Environment.NewLine + ex.Message);
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

            wnd.le_resultTbox.Text = Method.GetResult();
            wnd.le_logTbox.Text = Method.ToString() + "\n\n" + Method.Log.ToString();

            if (Method is KantzMethod || Method is RosensteinMethod)
            {
                wnd.le_kantzResultGbox.Visibility = Visibility.Visible;
            }
            else
            {
                wnd.le_kantzResultGbox.Visibility = Visibility.Hidden;
            }

            if (Method is KantzMethod)
            {
                wnd.le_k_epsCombo.ItemsSource = ((KantzMethod)Method).SlopesList.Keys;
                wnd.le_k_epsCombo.SelectedIndex = 0;
                ((KantzMethod)Method).SetSlope(wnd.le_k_epsCombo.Text);
            }

            if (Method.Slope.Length > 1)
            {
                try
                {
                    if (Method is KantzMethod || Method is RosensteinMethod)
                    {
                        var leSectorEnd = Ext.SlopeChangePointIndex(Method.Slope, 2, Method.Slope.Amplitude.Y / 30);

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
                    MessageBox.Show(Properties.Resources.LePlotError + Environment.NewLine + ex.Message);
                    result = Properties.Resources.Nda;
                }
            }
            else
            {
                result = Properties.Resources.Nda;
            }

            if (Method is KantzMethod || Method is RosensteinMethod)
            {
                wnd.le_resultTbox.Text = result;
            }
        }

        private string FillLyapunovChart(MainWindow wnd)
        {
            var startPoint = wnd.le_k_startTbox.ReadInt();
            var endPoint = wnd.le_k_endTbox.ReadInt();

            wnd.le_mainSlopeChart.Plot(_zero, _zero);
            wnd.le_secondarySlopeChart.Plot(_zero, _zero);

            var result = string.Empty;

            wnd.le_slopeChart.BottomTitle = "t";
            wnd.le_mainSlopeChart.Plot(Method.Slope.XValues, Method.Slope.YValues);

            if (Method is WolfMethod)
            {
                wnd.le_slopeChart.LeftTitle = "LE";
                wnd.le_slopeChartTitle.Text = "Lyapunov Exponent in Time";
            }
            else
            {
                var tsSector = new Timeseries();

                tsSector.AddDataPoint(Method.Slope.DataPoints[startPoint - 1].X, Method.Slope.DataPoints[startPoint - 1].Y);
                tsSector.AddDataPoint(Method.Slope.DataPoints[endPoint - 1].X, Method.Slope.DataPoints[endPoint - 1].Y);

                wnd.le_slopeChart.LeftTitle = "Slope";
                wnd.le_slopeChartTitle.Text = "Lyapunov Function";
                wnd.le_secondarySlopeChart.Plot(tsSector.XValues, tsSector.YValues);

                var slope = (Method.Slope.DataPoints[endPoint].Y - Method.Slope.DataPoints[startPoint].Y) / (Method.Slope.DataPoints[endPoint].X - Method.Slope.DataPoints[startPoint].X);
                result = string.Format("{0:G5}", slope);
            }

            return result;
        }
    }
}

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
                    wnd.le_mainSlopeChart.Plot(_zero, _zero);
                    wnd.le_secondarySlopeChart.Plot(_zero, _zero);
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
                    wnd.le_resultTbox.Text = StringData.Error;
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
                MessageBox.Show("Error plotting Lyapunov slope: " + ex.Message);
            }
        }

        public void CleanUp(MainWindow wnd)
        {
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

            if (wnd.le_kantzRad.IsChecked.Value || wnd.le_rosRad.IsChecked.Value)
            {
                wnd.le_kantzResultGbox.Visibility = Visibility.Visible;
            }
            else
            {
                wnd.le_kantzResultGbox.Visibility = Visibility.Hidden;
            }

            if (wnd.le_kantzRad.IsChecked.Value)
            {
                wnd.le_k_epsCombo.ItemsSource = ((KantzMethod)Method).SlopesList.Keys;
                wnd.le_k_epsCombo.SelectedIndex = 0;
                ((KantzMethod)Method).SetSlope(wnd.le_k_epsCombo.Text);
            }

            if (Method.Slope.Length > 1)
            {
                wnd.le_k_startTbox.Text = "1";
                wnd.le_k_endTbox.Text = (Method.Slope.Length - 1).ToString();

                try
                {
                    if (!wnd.le_wolfRad.IsChecked.Value)
                    {
                        var leSectorEnd = Ext.SlopeChangePointIndex(Method.Slope, 2, Method.Slope.Amplitude.Y / 30);

                        if (leSectorEnd > 0)
                        {
                            wnd.le_k_endTbox.Text = leSectorEnd.ToString();
                        }
                    }

                    result = FillLyapunovChart(wnd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error plotting Lyapunov slope: " + ex.Message);
                    result = StringData.NoValue;
                }
            }
            else
            {
                result = StringData.NoValue;
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

            int range = endPoint - startPoint + 1;
            var result = string.Empty;

            if (wnd.le_wolfRad.IsChecked.Value)
            {
                var timeseries = new Timeseries();

                for (int i = startPoint; i < range; i++)
                {
                    timeseries.AddDataPoint(Method.Slope.DataPoints[i].X, Method.Slope.DataPoints[i].Y);
                }

                wnd.le_slopeChart.BottomTitle = "t";
                wnd.le_slopeChart.LeftTitle = "LE";
                wnd.le_slopeChartTitle.Text = "Lyapunov Exponent in Time";
                wnd.le_mainSlopeChart.Plot(timeseries.XValues, timeseries.YValues);
            }
            else
            {
                var tsSector = new Timeseries();

                tsSector.AddDataPoint(Method.Slope.DataPoints[startPoint].X, Method.Slope.DataPoints[startPoint].Y);
                tsSector.AddDataPoint(Method.Slope.DataPoints[range - 1].X, Method.Slope.DataPoints[range - 1].Y);

                wnd.le_slopeChart.BottomTitle = "t";
                wnd.le_slopeChart.LeftTitle = "Slope";
                wnd.le_slopeChartTitle.Text = "Lyapunov Function";
                wnd.le_mainSlopeChart.Plot(Method.Slope.XValues, Method.Slope.YValues);
                wnd.le_secondarySlopeChart.Plot(tsSector.XValues, tsSector.YValues);

                var slope = (Method.Slope.DataPoints[endPoint].Y - Method.Slope.DataPoints[startPoint].Y) / (Method.Slope.DataPoints[endPoint].X - Method.Slope.DataPoints[startPoint].X);
                result = string.Format("{0:F5}", slope);
            }

            return result;
        }
    }
}

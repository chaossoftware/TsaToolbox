using MathLib;
using MathLib.Data;
using MathLib.NumericalMethods;
using MathLib.NumericalMethods.Lyapunov;
using MathLib.Transform;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TimeSeriesToolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly LyapunovExponents _lyapunov;
        private SourceData sourceData;
        private LyapunovMethod le;
        //private Charts charts;
        private readonly double[] _zero = new double[] { 0 };

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            //_lyapunov = new LyapunovExponents(this);
        }

        private void ts_btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "All files|*.*|Time series data|*.dat *.txt *.csv"
            };

            if (!openFileDialog.ShowDialog().Value)
            {
                return;
            }

            try
            {
                CleanUp();

                if (ts_parameterizedOpenCbox.IsChecked.Value)
                {
                    sourceData = new SourceData(
                        openFileDialog.FileName,
                        ts_LinesToSkipTbox.ReadInt(), 
                        ts_LinesToReadTbox.ReadInt());
                }
                else
                {
                    sourceData = new SourceData(openFileDialog.FileName);
                }

                FillUiWithData();

                RefreshTimeSeries();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Unable to read file:" + ex.Message);
            }
        }

        private void CleanUp()
        {
            sourceData = null;
            ch_SignalChart.Plot(_zero, _zero);
            ch_PseudoPoincareChart.Plot(_zero, _zero);

            //chartFft.ClearChart();
            
            le_mainSlopeChart.Plot(_zero, _zero);
            le_secondarySlopeChart.Plot(_zero, _zero);
            tsp_autocorChart.Plot(_zero, _zero);

            //wav_plotPBox.Image = null;
            //routines.Lyapunov = null;
            //routines.DeleteTempFiles();
        }

        private void FillUiWithData()
        {
            if (ts_tsColumnTbox.ReadInt() > sourceData.ColumnsCount)
            {
                ts_timestampColumnCbox.IsChecked = false;
                ts_tsColumnTbox.Text = "1";
            }

            if (ts_endPointTbox.ReadInt() > sourceData.LinesCount)
            {
                ts_startPointTbox.Text = "1";
                ts_endPointTbox.Text = sourceData.LinesCount.ToString();
            }

            statusFileInfoText.Text = sourceData.ToString().Replace("\n", " | ");
            ts_endPointTbox.Text = sourceData.LinesCount.ToString();
        }

        private bool RefreshTimeSeries()
        {
            if (sourceData == null)
            {
                MessageBox.Show(StringData.MsgEmptyFile);
                return false;
            }

            sourceData.SetTimeSeries(
                ts_tsColumnTbox.ReadInt() - 1,
                ts_startPointTbox.ReadInt() - 1,
                ts_endPointTbox.ReadInt() - 1,
                ts_eachNPointsTbox.ReadInt(),
                ts_timestampColumnCbox.IsChecked.Value
            );

            statusTsInfoText.Text = $"Range [{ts_startPointTbox.Text}; {ts_endPointTbox.Text}] | Signal column: {ts_tsColumnTbox.Text}";

            if (ts_timestampColumnCbox.IsChecked.Value)
            {
                statusTsInfoText.Text += " (1st column is timestamp)";
            }

            statusDtTbox.Text = ts_timestampColumnCbox.IsChecked.Value ?
                string.Format(CultureInfo.InvariantCulture, "{0:G8}", sourceData.Step) :
                "NaN";

            return true;
        }

        private void ts_parameterizedOpenCbox_Unchecked(object sender, RoutedEventArgs e)
        {
            ts_LinesToSkipTbox.IsEnabled = false;
            ts_LinesToReadTbox.IsEnabled = false;
        }

        private void ts_parameterizedOpenCbox_Checked(object sender, RoutedEventArgs e)
        {
            ts_LinesToSkipTbox.IsEnabled = true;
            ts_LinesToReadTbox.IsEnabled = true;
        }

        private void ts_setTimeseriesBtn_Click(object sender, RoutedEventArgs e) =>
            RefreshTimeSeries();

        private void le_rosRad_Checked(object sender, RoutedEventArgs e) =>
            le_rosGbox.Visibility = Visibility.Visible;

        private void le_rosRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_rosGbox.Visibility = Visibility.Hidden;

        private void le_kantzRad_Checked(object sender, RoutedEventArgs e) =>
            le_kantzGbox.Visibility = Visibility.Visible;

        private void le_kantzRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_kantzGbox.Visibility = Visibility.Hidden;

        private void le_wolfRad_Checked(object sender, RoutedEventArgs e) =>
            le_wolfGbox.Visibility = Visibility.Visible;

        private void le_wolfRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_wolfGbox.Visibility = Visibility.Hidden;

        private void le_ssRad_Checked(object sender, RoutedEventArgs e) =>
            le_ssGbox.Visibility = Visibility.Visible;

        private void le_ssRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_ssGbox.Visibility = Visibility.Hidden;

        private void ch_buildBtn_Click(object sender, RoutedEventArgs e)
        {
            ch_SignalChart.Plot(sourceData.TimeSeries.XValues, sourceData.TimeSeries.YValues);

            var pPoincare = PseudoPoincareMap.GetMapDataFrom(sourceData.TimeSeries.YValues, 1);
            ch_PseudoPoincareChart.Plot(pPoincare.XValues, pPoincare.YValues);
        }

        private void ch_SignalChart_MouseDoubleClick(object sender, MouseButtonEventArgs e) =>
            new PreviewForm(Properties.Resources.Signal, "t", "f(t)")
                .PlotLine(sourceData.TimeSeries.XValues, sourceData.TimeSeries.YValues)
                .ShowDialog();

        private void ch_PseudoPoincareChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var pPoincare = PseudoPoincareMap.GetMapDataFrom(sourceData.TimeSeries.YValues, 1);

            new PreviewForm(Properties.Resources.PseudoPoincare, "f(t)", "f(t+1)")
                .PlotMap(pPoincare.XValues, pPoincare.YValues)
                .ShowDialog();
        }

        private void tsp_autocorBtn_Click(object sender, RoutedEventArgs e)
        {
            var autoCor = new AutoCorrelationFunction()
                .GetAutoCorrelationOfSeries(sourceData.TimeSeries.YValues);

            tsp_autocorChart.PlotY(autoCor);
        }

        private void tsp_autocorChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var autoCor = new AutoCorrelationFunction()
                .GetAutoCorrelationOfSeries(sourceData.TimeSeries.YValues);

            new PreviewForm(Properties.Resources.PseudoPoincare, "t", "ACF")
                .PlotLine(autoCor)
                .ShowDialog();
        }

        private void le_calculateBtn_Click(object sender, RoutedEventArgs e)
        {
            SetLyapunovMethod(sourceData.TimeSeries.YValues);

            new Thread(() => ExecuteLyapunovMethod())
                    .Start();
        }

        private void ExecuteLyapunovMethod()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    le_resultTbox.Background = Brushes.OrangeRed;
                    le_resultTbox.Text = StringData.Calculating;
                });

                le.Calculate();
                Dispatcher.Invoke(() => SetLyapunovResult());
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    le_logTbox.Text = ex.ToString();
                    le_resultTbox.Background = Brushes.Red;
                    le_resultTbox.Text = StringData.Error;
                });
            }
        }

        private void SetLyapunovMethod(double[] series)
        {
            var dim = le_eDimTbox.ReadInt();
            var tau = le_tauTbox.ReadInt();
            var scaleMin = le_epsMinTbox.ReadDouble();

            if (le_wolfRad.IsChecked.Value)
            {
                var dt = le_w_dtTbox.ReadDouble();
                var scaleMax = le_w_epsMaxTbox.ReadDouble();
                var evolSteps = le_w_evolStepsTbox.ReadInt();

                le = new WolfMethod(series, dim, tau, dt, scaleMin, scaleMax, evolSteps);
            }
            else if (le_rosRad.IsChecked.Value)
            {
                var iter = le_r_iterTbox.ReadInt();
                var window = le_r_windowTbox.ReadInt();

                le = new RosensteinMethod(series, dim, tau, iter, window, scaleMin);
            }
            else if (le_kantzRad.IsChecked.Value)
            {
                var iter = le_k_iterTbox.ReadInt();
                var window = le_k_windowTbox.ReadInt();
                var scaleMax = le_k_epsMaxTbox.ReadDouble();
                var scales = le_k_scalesTbox.ReadInt();

                le = new KantzMethod(series, dim, tau, iter, window, scaleMin, scaleMax, scales);
            }
            else if (le_ssRad.IsChecked.Value)
            {
                var inverse = le_ss_inverseCbox.IsChecked.Value;
                var scaleFactor = le_ss_scaleFactorTbox.ReadDouble();
                var minNeigh = le_ss_minNeighbTbox.ReadInt();

                le = new SanoSawadaMethod(series, dim, tau, series.Length, scaleMin, scaleFactor, minNeigh, inverse);
            }
        }

        private void SetLyapunovResult()
        {
            le_resultTbox.Background = Brushes.Khaki;
            string result;

            le_resultTbox.Text = le.GetResult();
            le_logTbox.Text = le.ToString() + "\n\n" + le.Log.ToString();

            if (le_kantzRad.IsChecked.Value || le_rosRad.IsChecked.Value)
            {
                le_kantzResultGbox.Visibility = Visibility.Visible;
            }
            else
            {
                le_kantzResultGbox.Visibility = Visibility.Hidden;
            }

            if (le_kantzRad.IsChecked.Value)
            {
                le_k_epsCombo.ItemsSource = ((KantzMethod)le).SlopesList.Keys;
                le_k_epsCombo.SelectedIndex = 0;
                ((KantzMethod)le).SetSlope(le_k_epsCombo.Text);
            }

            if (le.Slope.Length > 1)
            {
                le_k_startTbox.Text = "1";
                le_k_endTbox.Text = (le.Slope.Length - 1).ToString();

                try
                {
                    if (!le_wolfRad.IsChecked.Value)
                    {
                        var leSectorEnd = Ext.SlopeChangePointIndex(le.Slope, 2, le.Slope.Amplitude.Y / 30);

                        if (leSectorEnd > 0)
                        {
                            le_k_endTbox.Text = leSectorEnd.ToString();
                        }
                    }

                    result = FillLyapunovChart(1, le_k_endTbox.ReadInt(), le_wolfRad.IsChecked.Value);
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

            if (le is KantzMethod || le is RosensteinMethod)
            {
                le_resultTbox.Text = result;
            }
        }

        public string FillLyapunovChart(int startPoint, int endPoint, bool isWolf)
        {
            le_mainSlopeChart.Plot(_zero, _zero);
            le_secondarySlopeChart.Plot(_zero, _zero);

            int range = endPoint - startPoint + 1;
            var result = string.Empty;

            if (isWolf)
            {
                var timeseries = new Timeseries();

                for (int i = startPoint; i < range; i++)
                {
                    timeseries.AddDataPoint(le.Slope.DataPoints[i].X, le.Slope.DataPoints[i].Y);
                }

                le_slopeChart.BottomTitle = "t";
                le_slopeChart.LeftTitle = "LE";
                le_slopeChartTitle.Text = "Lyapunov Exponent in Time";
                le_mainSlopeChart.Plot(timeseries.XValues, timeseries.YValues);
            }
            else
            {
                var tsSector = new Timeseries();

                tsSector.AddDataPoint(le.Slope.DataPoints[startPoint].X, le.Slope.DataPoints[startPoint].Y);
                tsSector.AddDataPoint(le.Slope.DataPoints[range - 1].X, le.Slope.DataPoints[range - 1].Y);

                le_slopeChart.BottomTitle = "t";
                le_slopeChart.LeftTitle = "Slope";
                le_slopeChartTitle.Text = "Lyapunov Function";
                le_mainSlopeChart.Plot(le.Slope.XValues, le.Slope.YValues);
                le_secondarySlopeChart.Plot(tsSector.XValues, tsSector.YValues);

                var slope = (le.Slope.DataPoints[endPoint].Y - le.Slope.DataPoints[startPoint].Y) / (le.Slope.DataPoints[endPoint].X - le.Slope.DataPoints[startPoint].X);
                result = string.Format("{0:F5}", slope);
            }

            return result;
        }

        private void le_k_adjustBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (le is KantzMethod)
                {
                    ((KantzMethod)le).SetSlope(le_k_epsCombo.Text);
                }

                var res = FillLyapunovChart(le_k_startTbox.ReadInt(), le_k_endTbox.ReadInt(), le_wolfRad.IsChecked.Value);

                if (le is KantzMethod || le is RosensteinMethod)
                {
                    le_resultTbox.Text = res;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error plotting Lyapunov slope: " + ex.Message);
            }
        }
    }
}

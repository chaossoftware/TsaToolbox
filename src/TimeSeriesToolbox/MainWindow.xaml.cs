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
        private readonly LyapunovExponents _lyapunov;
        private SourceData sourceData;
        //private Charts charts;
        private readonly double[] _zero = new double[] { 0 };

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            _lyapunov = new LyapunovExponents();
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
            ch_acfChart.Plot(_zero, _zero);

            _lyapunov.CleanUp(this);
            //chartFft.ClearChart();
            //wav_plotPBox.Image = null;
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
            if (ch_signalCbox.IsChecked.Value)
            {
                ch_SignalChart.Plot(sourceData.TimeSeries.XValues, sourceData.TimeSeries.YValues);
            }

            if (ch_poincareCbox.IsChecked.Value)
            {
                var pPoincare = PseudoPoincareMap.GetMapDataFrom(sourceData.TimeSeries.YValues, 1);
                ch_PseudoPoincareChart.Plot(pPoincare.XValues, pPoincare.YValues);
            }

            if (ch_acfCbox.IsChecked.Value)
            {
                var autoCor = new AutoCorrelationFunction().GetFromSeries(sourceData.TimeSeries.YValues);
                ch_acfChart.PlotY(autoCor);
            }
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

        private void tsp_autocorChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var autoCor = new AutoCorrelationFunction()
                .GetFromSeries(sourceData.TimeSeries.YValues);

            new PreviewForm(Properties.Resources.PseudoPoincare, "t", "ACF")
                .PlotLine(autoCor)
                .ShowDialog();
        }

        private void le_calculateBtn_Click(object sender, RoutedEventArgs e)
        {
            SetLyapunovMethod(sourceData.TimeSeries.YValues);
            _lyapunov.CleanUp(this);

            new Thread(() => _lyapunov.ExecuteLyapunovMethod(this))
                    .Start();
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

                _lyapunov.Method = new WolfMethod(series, dim, tau, dt, scaleMin, scaleMax, evolSteps);
            }
            else if (le_rosRad.IsChecked.Value)
            {
                var iter = le_r_iterTbox.ReadInt();
                var window = le_r_windowTbox.ReadInt();

                _lyapunov.Method = new RosensteinMethod(series, dim, tau, iter, window, scaleMin);
            }
            else if (le_kantzRad.IsChecked.Value)
            {
                var iter = le_k_iterTbox.ReadInt();
                var window = le_k_windowTbox.ReadInt();
                var scaleMax = le_k_epsMaxTbox.ReadDouble();
                var scales = le_k_scalesTbox.ReadInt();

                _lyapunov.Method = new KantzMethod(series, dim, tau, iter, window, scaleMin, scaleMax, scales);
            }
            else if (le_ssRad.IsChecked.Value)
            {
                var inverse = le_ss_inverseCbox.IsChecked.Value;
                var scaleFactor = le_ss_scaleFactorTbox.ReadDouble();
                var minNeigh = le_ss_minNeighbTbox.ReadInt();

                _lyapunov.Method = new SanoSawadaMethod(series, dim, tau, series.Length, scaleMin, scaleFactor, minNeigh, inverse);
            }
        }

        private void le_k_adjustBtn_Click(object sender, RoutedEventArgs e) =>
            _lyapunov.AdjustSlope(this);
    }
}

using ChaosSoft.Core.Data;
using ChaosSoft.Core.IO;
using ChaosSoft.Core.NumericalMethods;
using ChaosSoft.Core.NumericalMethods.EmbeddingDimension;
using ChaosSoft.Core.NumericalMethods.Lyapunov;
using ChaosSoft.Core.Transform;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TimeSeriesToolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LyapunovExponents _lyapunov;
        private readonly CommandProcessor _commandProcessor;
        internal SourceData sourceData;
        private readonly double[] _zero = new double[] { 0 };

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            _lyapunov = new LyapunovExponents();
            _commandProcessor = new CommandProcessor(tboxConsole, this);
        }

        private void ts_btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "All files|*.*|Time series data|*.dat *.txt *.csv"
            };

            if (openFileDialog.ShowDialog().Value)
            {
                try
                {
                    OpenFile(openFileDialog.FileName);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Unable to read file:" + ex.Message);
                }
            }
        }

        public void OpenFile(string fileName)
        {
            CleanUp();

            if (ts_parameterizedOpenCbox.IsChecked.Value)
            {
                sourceData = new SourceData(
                    fileName,
                    ts_LinesToSkipTbox.ReadInt(),
                    ts_LinesToReadTbox.ReadInt());
            }
            else
            {
                sourceData = new SourceData(fileName);
            }

            FillUiWithData();

            RefreshTimeSeries();
        }

        private void CleanUp()
        {
            sourceData = null;
            ch_SignalGraph.Plot(_zero, _zero);
            ch_PseudoPoincareGraph.Plot(_zero, _zero);
            ch_acfGraph.Plot(_zero, _zero);

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
                MessageBox.Show(Properties.Resources.MsgEmptyFile);
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
                ch_SignalGraph.Plot(sourceData.TimeSeries.XValues, sourceData.TimeSeries.YValues);
            }

            if (ch_poincareCbox.IsChecked.Value)
            {
                var pPoincare = PseudoPoincareMap.GetMapDataFrom(sourceData.TimeSeries.YValues, 1);
                ch_PseudoPoincareGraph.Plot(pPoincare.XValues, pPoincare.YValues);
            }

            if (ch_acfCbox.IsChecked.Value)
            {
                var autoCor = new AutoCorrelationFunction().GetFromSeries(sourceData.TimeSeries.YValues);
                ch_acfGraph.PlotY(autoCor);
            }

            if (ch_fnnCbox.IsChecked.Value)
            {
                var fnn = new FalseNearestNeighbors(sourceData.TimeSeries.YValues, fnn_minDim.ReadInt(), fnn_maxDim.ReadInt(), fnn_tau.ReadInt(), fnn_rt.ReadDouble(), fnn_theiler.ReadInt());
                fnn.Calculate();
                an_FnnGraph.Plot(fnn.FalseNeighbors.Keys, fnn.FalseNeighbors.Values);
            }
        }

        private void ch_SignalChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                new PreviewForm(Properties.Resources.Signal, "t", "f(t)")
                .SetSize(set_previewWidthTbox.ReadDouble(), set_previewHeightTbox.ReadDouble())
                .PlotLine(sourceData.TimeSeries.XValues, sourceData.TimeSeries.YValues)
                .ShowDialog();
            }
        }
            

        private void ch_PseudoPoincareChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var pPoincare = PseudoPoincareMap.GetMapDataFrom(sourceData.TimeSeries.YValues, 1);

                new PreviewForm(Properties.Resources.PseudoPoincare, "f(t)", "f(t+1)")
                    .SetSize(set_previewWidthTbox.ReadDouble(), set_previewHeightTbox.ReadDouble())
                    .PlotMap(pPoincare.XValues, pPoincare.YValues)
                    .ShowDialog();
            }
        }

        private void tsp_autocorChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var autoCor = new AutoCorrelationFunction()
                .GetFromSeries(sourceData.TimeSeries.YValues);

                new PreviewForm(Properties.Resources.Acf, "t", "ACF")
                    .SetSize(set_previewWidthTbox.ReadDouble(), set_previewHeightTbox.ReadDouble())
                    .PlotLine(autoCor)
                    .ShowDialog();
            }
        }

        private void le_calculateBtn_Click(object sender, RoutedEventArgs e)
        {
            _lyapunov.CleanUp(this);
            SetLyapunovMethod(sourceData.TimeSeries.YValues);

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

                _lyapunov.Method = new LleWolf(series, dim, tau, dt, scaleMin, scaleMax, evolSteps);
            }
            else if (le_rosRad.IsChecked.Value)
            {
                var iter = le_r_iterTbox.ReadInt();
                var window = le_r_windowTbox.ReadInt();

                _lyapunov.Method = new LleRosenstein(series, dim, tau, iter, window, scaleMin);
            }
            else if (le_kantzRad.IsChecked.Value)
            {
                var iter = le_k_iterTbox.ReadInt();
                var window = le_k_windowTbox.ReadInt();
                var scaleMax = le_k_epsMaxTbox.ReadDouble();
                var scales = le_k_scalesTbox.ReadInt();

                _lyapunov.Method = new LleKantz(series, dim, tau, iter, window, scaleMin, scaleMax, scales);
            }
            else if (le_ssRad.IsChecked.Value)
            {
                var inverse = le_ss_inverseCbox.IsChecked.Value;
                var scaleFactor = le_ss_scaleFactorTbox.ReadDouble();
                var minNeigh = le_ss_minNeighbTbox.ReadInt();

                _lyapunov.Method = new LeSpecSanoSawada(series, dim, tau, series.Length, scaleMin, scaleFactor, minNeigh, inverse);
            }
        }

        private void le_k_adjustBtn_Click(object sender, RoutedEventArgs e) =>
            _lyapunov.AdjustSlope(this);

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sourceData == null)
            {
                MessageBox.Show(Properties.Resources.MsgEmptyFile);
                return;
            }

            var outDir = Path.Combine(sourceData.Folder, sourceData.FileName + "_out");
            string fName = Path.Combine(outDir, sourceData.FileName);

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            if (ch_signalCbox.IsChecked.Value)
            {
                DataWriter.CreateDataFile(fName + "_signal.dat", sourceData.GetTimeSeriesString());
                SaveChartToFile(ch_SignalChart, fName + "_signal.png");
            }

            if (ch_poincareCbox.IsChecked.Value)
            {
                SaveChartToFile(ch_PseudoPoincareChart, fName + "_poincare.png");
            }

            if (ch_acfCbox.IsChecked.Value)
            {
                SaveChartToFile(ch_acfChart, fName + "_acf.png");
            }

            //if (chartFft.HasData)
            //{
            //    chartFft.SaveImage(fName + "_fourier", ImageFormat.Png);
            //}

            //if (wav_plotPBox.Image != null)
            //{
            //    wav_plotPBox.Image.Save(fName + "_wavelet.png", ImageFormat.Png);
            //}

            if (_lyapunov.Method != null)
            {
                GenerateLeFile(fName);
                SaveChartToFile(le_slopeChart, fName + "_lyapunovSlope.png");
            }
        }

        private void GenerateLeFile(string baseFileName)
        {
            var sb = new StringBuilder();
            sb.AppendLine(_lyapunov.Method.ToString())
                .AppendLine()
                .AppendLine("Result:")
                .AppendLine(le_resultTbox.Text)
                .AppendLine()
                .AppendLine("Execution log:")
                .AppendLine()
                .AppendLine(_lyapunov.Method.Log.ToString());
            DataWriter.CreateDataFile(baseFileName + "_lyapunov.txt", sb.ToString());
        }

        private void SaveChartToFile(InteractiveDataDisplay.WPF.Chart plot, string path)
        {
            plot.Arrange(new Rect(plot.RenderSize));
            plot.Measure(plot.RenderSize);
            Rect bounds = VisualTreeHelper.GetDescendantBounds(plot);

            var scaleX = set_chartSaveWidthTbox.ReadDouble() / plot.Width;
            var scaleY = set_chartSaveHeightTbox.ReadDouble() / plot.Height;

            var width = (bounds.Width + bounds.X) * scaleX;
            var height = (bounds.Height + bounds.Y) * scaleY;

            RenderTargetBitmap rtb =
                new RenderTargetBitmap((int)Math.Round(width, MidpointRounding.AwayFromZero),
                (int)Math.Round(height, MidpointRounding.AwayFromZero),
                96, 96, PixelFormats.Pbgra32);

            DrawingVisual dv = new DrawingVisual();
            
            using (DrawingContext ctx = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(plot);
                ctx.DrawRectangle(vb, null,
                    new Rect(new Point(bounds.X, bounds.Y), new Point(width, height)));
            }

            rtb.Render(dv);
            var iSource = (BitmapSource)rtb.GetAsFrozen();

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(iSource));
                encoder.Save(fileStream);
            }
        }

        private void tboxConsole_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Back:
                    var restOfCommand = LastInline().Text;

                    if (string.IsNullOrEmpty(restOfCommand))
                    {
                        e.Handled = true;
                        return;
                    }

                    break;

                case Key.Enter:
                    var commandToProcess = LastInline().Text.Trim();
                    _commandProcessor.ProcessCommand(commandToProcess);
                    break;

                case Key.Up:
                    var lastInline = LastInline();
                    lastInline.Text = _commandProcessor.LastCommand;
                    tboxConsole.CaretPosition = lastInline.ContentEnd;
                    e.Handled = true;
                    break;

                case Key.Tab:
                    var lastInline1 = LastInline();
                    var matchingCommands = _commandProcessor.Commands.Keys.Where(c => c.StartsWith(lastInline1.Text));

                    if (matchingCommands.Any())
                    {
                        lastInline1.Text = matchingCommands.First();

                        tboxConsole.CaretPosition = lastInline1.ContentEnd;
                        var thread = new Thread(() => { Thread.Sleep(500); Dispatcher.BeginInvoke(new Action(() => tboxConsole.Focus())); });
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.Start();
                    }

                    break;
            }

            Run LastInline() => (tboxConsole.Document.Blocks.LastBlock as Paragraph).Inlines.LastInline as Run;
        }
    }
}

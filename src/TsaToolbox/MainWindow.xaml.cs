using ChaosSoft.Core.Data;
using ChaosSoft.Core.IO;
using ChaosSoft.MatlabIntegration;
using ChaosSoft.NumericalMethods;
using ChaosSoft.NumericalMethods.Lyapunov;
using ChaosSoft.NumericalMethods.PhaseSpace;
using ChaosSoft.NumericalMethods.Transform;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TsaToolbox.Models;

namespace TsaToolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LyapunovExponents _lyapunov;
        private readonly CommandProcessor _commandProcessor;

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();

            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            statusVersionText.Text = $" v{versionInfo.ProductVersion}";
            _lyapunov = new LyapunovExponents();
            _commandProcessor = new CommandProcessor(tboxConsole, this);

            Instance = this;
        }

        public static MainWindow Instance { get; set; }

        public Settings Settings { get; set; }

        public DataSource Source { get; set; }

        public void CleanUp()
        {
            Source.Data = null;
            ClearPlot(ch_SignalChart);
            ClearPlot(ch_PseudoPoincareChart);
            ClearPlot(ch_acfChart);
            ClearPlot(an_FnnChart);
            ClearPlot(an_miChart);
            ClearPlot(ch_FftChart);

            ch_wavChart.Reset();
            _lyapunov.CleanUp(this);

            DeleteWaveletTempFile();
        }

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

        internal void ClearPlot(ScottPlot.WpfPlot plot)
        {
            plot.Plot.Clear();
            plot.Render();
        }

        private void SavePlot(ScottPlot.WpfPlot plot, string fileName) =>
            plot.Plot.SaveFig(fileName, Settings.SaveChartWidth, Settings.SaveChartHeight);

        internal void PlotScatter(ScottPlot.WpfPlot plot, DataSeries series, string xLabel, string yLabel)
        {
            ClearPlot(plot);
            plot.Plot.AddScatter(series.XValues, series.YValues, System.Drawing.Color.Blue, 0.5f, 0f);
            RenderPlot(plot, xLabel, yLabel);
        }

        private void PlotScatter(ScottPlot.WpfPlot plot, double[] xs, double[] ys, string xLabel, string yLabel)
        {
            ClearPlot(plot);
            plot.Plot.AddScatter(xs, ys, System.Drawing.Color.Blue, 0.5f, 0f);
            RenderPlot(plot, xLabel, yLabel);
        }

        private void PlotSignal(ScottPlot.WpfPlot plot, double[] series, string xLabel, string yLabel)
        {
            ClearPlot(plot);
            plot.Plot.AddSignal(series, 1, System.Drawing.Color.Blue);
            RenderPlot(plot, xLabel, yLabel);
        }

        private void PlotScatterPoints(ScottPlot.WpfPlot plot, DataSeries series, string xLabel, string yLabel)
        {
            ClearPlot(plot);
            plot.Plot.AddScatterPoints(series.XValues, series.YValues, System.Drawing.Color.Blue, 1);
            RenderPlot(plot, xLabel, yLabel);
        }

        private void RenderPlot(ScottPlot.WpfPlot plot, string xLabel, string yLabel)
        {
            plot.Plot.XAxis.LabelStyle(fontSize: 12);
            plot.Plot.YAxis.LabelStyle(fontSize: 12);
            plot.Plot.XAxis.Label(xLabel);
            plot.Plot.YAxis.Label(yLabel);
            plot.Plot.Layout(padding: 0);
            plot.Render();
        }

        private void AddVerticalLine(ScottPlot.WpfPlot plot, double x)
        {
            plot.Plot.AddVerticalLine(x, System.Drawing.Color.Red, 1, ScottPlot.LineStyle.DashDot);
            plot.Plot.AddAnnotation(x.ToString(), 0, 0);
            plot.Render();
        }

        private void ch_buildBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ch_signalCbox.IsChecked.Value)
            {
                PlotScatter(ch_SignalChart, Source.Data.TimeSeries, "t", "f(t)");
            }

            if (ch_poincareCbox.IsChecked.Value)
            {
                var pPoincare = DelayedCoordinates.GetData(Source.Data.TimeSeries.YValues, 1);
                PlotScatterPoints(ch_PseudoPoincareChart, pPoincare, "Xn", "Xn+1");
            }

            if (ch_acfCbox.IsChecked.Value)
            {
                BuildAcfChart();
            }

            if (ch_fnnCbox.IsChecked.Value)
            {
                BuildFnnChart();
            }

            if (ch_miCbox.IsChecked.Value)
            {
                BuildMiChart();
            }
        }

        private void BuildAcfChart()
        {
            var autoCor = Statistics.Acf(Source.Data.TimeSeries.YValues);

            PlotSignal(ch_acfChart, autoCor, "t", "acf");

            int i;

            for (i = 1; i < autoCor.Length; i++)
            {
                if (Math.Sign(autoCor[i]) != Math.Sign(autoCor[i - 1]))
                {
                    break;
                }
            }

            AddVerticalLine(ch_acfChart, i);
        }

        private void BuildFnnChart()
        {
            var fnn = new FalseNearestNeighbors(
                fnn_minDim.ReadInt(), 
                fnn_maxDim.ReadInt(), 
                fnn_tau.ReadInt(), 
                fnn_rt.ReadDouble(), 
                fnn_theiler.ReadInt());

            fnn.Calculate(Source.Data.TimeSeries.YValues);

            PlotScatter(an_FnnChart,
                fnn.FalseNeighbors.Keys.Select(x => (double)x).ToArray(),
                fnn.FalseNeighbors.Values.Select(y => (double)y).ToArray(),
                "d",
                "fnn");

            int key = fnn.FalseNeighbors.Keys.First(k => fnn.FalseNeighbors[k] == 0);
            AddVerticalLine(an_FnnChart, key);
        }

        private void BuildMiChart()
        {
            var mi = new MutualInformation(mi_partitions.ReadInt(), mi_maxDelay.ReadInt());
            mi.Calculate(Source.Data.TimeSeries.YValues);

            PlotScatter(an_miChart, mi.EntropySlope, "d", "mi");

            int index = 1;
            bool firstMinimumReached = false;

            while (index < mi.EntropySlope.Length && !firstMinimumReached)
            {
                firstMinimumReached = mi.EntropySlope.YValues[index] > mi.EntropySlope.YValues[index - 1];
                index++;
            }

            double tau = mi.EntropySlope.XValues[index - 2];

            AddVerticalLine(an_miChart, tau);
        }

        private void ch_buildMlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ch_fftCbox.IsChecked.Value)
            {
                var fftTs = GetFft();

                PlotScatter(ch_FftChart, fftTs.XValues, fftTs.YValues, "ω", "F(ω)");
            }

            if (ch_WaveletCbox.IsChecked.Value)
            {
                DeleteWaveletTempFile();

                var brush = GetWavelet(
                    ch_wavChart, 
                    ch_wavChart.Width * 2, 
                    ch_wavChart.Height * 2, 
                    Properties.Resources.WaveletFile);

                ch_wavChart.Reset();

                ch_wavChart.Plot.Style(dataBackgroundImage: brush);
                ch_wavChart.Plot.Grid(enable: false);

                ch_wavChart.Plot.SetAxisLimits(
                    Source.Data.TimeSeries.Min.X,
                    Source.Data.TimeSeries.Max.X,
                    wav_omLeft.ReadDouble(),
                    wav_omRight.ReadDouble());

                RenderPlot(ch_wavChart, "t", "ω");
            }
        }

        private void le_calculateBtn_Click(object sender, RoutedEventArgs e)
        {
            _lyapunov.CleanUp(this);
            SetLyapunovMethod(Source.Data.TimeSeries.YValues.Length);

            new Thread(() => _lyapunov.ExecuteLyapunovMethod(this, Source.Data.TimeSeries.YValues))
                    .Start();
        }

        private void SetLyapunovMethod(int lenght)
        {
            var dim = le_eDimTbox.ReadInt();
            var tau = le_tauTbox.ReadInt();
            var scaleMin = le_epsMinTbox.ReadDouble();

            if (le_wolfRad.IsChecked.Value)
            {
                var dt = le_w_dtTbox.ReadDouble();
                var scaleMax = le_w_epsMaxTbox.ReadDouble();
                var evolSteps = le_w_evolStepsTbox.ReadInt();

                _lyapunov.Method = new LleWolf(dim, tau, dt, scaleMin, scaleMax, evolSteps);
            }
            else if (le_rosRad.IsChecked.Value)
            {
                var iter = le_r_iterTbox.ReadInt();
                var window = le_r_windowTbox.ReadInt();

                _lyapunov.Method = new LleRosenstein(dim, tau, iter, window, scaleMin);
            }
            else if (le_kantzRad.IsChecked.Value)
            {
                var iter = le_k_iterTbox.ReadInt();
                var window = le_k_windowTbox.ReadInt();
                var scaleMax = le_k_epsMaxTbox.ReadDouble();
                var scales = le_k_scalesTbox.ReadInt();

                _lyapunov.Method = new LleKantz(dim, tau, iter, window, scaleMin, scaleMax, scales);
            }
            else if (le_ssRad.IsChecked.Value)
            {
                var inverse = le_ss_inverseCbox.IsChecked.Value;
                var scaleFactor = le_ss_scaleFactorTbox.ReadDouble();
                var minNeigh = le_ss_minNeighbTbox.ReadInt();

                _lyapunov.Method = new LeSpecSanoSawada(dim, tau, lenght, scaleMin, scaleFactor, minNeigh, inverse);
            }
        }

        private void le_k_adjustBtn_Click(object sender, RoutedEventArgs e) =>
            _lyapunov.AdjustSlope(this);

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Source.Data == null)
            {
                MessageBox.Show(Properties.Resources.MsgEmptyFile);
                return;
            }

            string outDir =
                Settings.SeparateOutputDir ?
                Path.Combine(Settings.OutputDir, Source.Data.FileName) :
                Settings.OutputDir;

            string fName = Path.Combine(outDir, Source.Data.FileName);

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            if (ch_signalCbox.IsChecked.Value)
            {
                DataWriter.CreateDataFile(fName + "_signal.dat", Format.General(Source.Data.TimeSeries.YValues, "\n", 6));
                SavePlot(ch_SignalChart, fName + "_signal.png");
            }

            if (ch_poincareCbox.IsChecked.Value)
            {
                SavePlot(ch_PseudoPoincareChart, fName + "_poincare.png");
            }

            if (ch_acfCbox.IsChecked.Value)
            {
                SavePlot(ch_acfChart, fName + "_acf.png");
            }

            if (ch_fftCbox.IsChecked.Value)
            {
                SavePlot(ch_FftChart, fName + "_fft.png");
            }

            if (ch_WaveletCbox.IsChecked.Value)
            {
                SavePlot(ch_wavChart, fName + "_wavelet.png");
            }

            if (_lyapunov.Method != null)
            {
                GenerateLeFile(fName);
                SavePlot(le_slopeChart, fName + "_lyapunovSlope.png");
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

        private DataSeries GetFft()
        {
            int logScale = ch_logScaleCbox.IsChecked.Value ? 1 : 0;
            double dt = fft_dt.ReadDouble();
            double omStart = fft_omLeft.ReadDouble();
            double omEnd = fft_omRight.ReadDouble();
            
            return Fourier.GetFourier(Source.Data.TimeSeries.YValues, omStart, omEnd, dt, logScale);
        }

        private Bitmap GetWavelet(Visual visual, double width, double height, string fileName)
        {
            double tStart = Source.Data.TimeSeries.Min.X;
            double tEnd = Source.Data.TimeSeries.Max.X;

            try
            {
                Wavelet.BuildWavelet(Source.Data.TimeSeries.YValues,
                    fileName,
                    (wav_typeCbox.SelectedItem as ComboBoxItem).ToolTip.ToString(),
                    tStart,
                    tEnd,
                    wav_omLeft.ReadDouble(),
                    wav_omRight.ReadDouble(),
                    wav_dt.ReadDouble(),
                    wav_paletteCbox.Text,
                    width,
                    height);

                string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                
                var data = new BitmapImage();
                var stream = File.OpenRead(Path.Combine(dir, fileName));

                data.BeginInit();
                data.CacheOption = BitmapCacheOption.OnLoad;
                data.StreamSource = stream;
                data.EndInit();
                stream.Close();
                stream.Dispose();
                data.Freeze();

                DpiScale dpi = VisualTreeHelper.GetDpi(visual);

                double dWidth = data.Width * data.DpiX / dpi.PixelsPerInchX;
                double dHeight = data.Height * data.DpiY / dpi.PixelsPerInchY;

                var xOffset = dWidth / 12.5;
                var yOffset = dHeight / 12.5;
                var rect = new Int32Rect((int)xOffset, (int)yOffset, (int)(dWidth - 2 * xOffset), (int)(dHeight - 2 * yOffset));

                var croppedBitmap = new CroppedBitmap(data, rect);

                using (MemoryStream outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(croppedBitmap));
                    enc.Save(outStream);
                    Bitmap bitmap = new Bitmap(outStream);
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    return new Bitmap(bitmap);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to build {Properties.Resources.Wavelet}:\n" + ex.Message);
                return null;
            }
        }

        private void le_k_epsCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) =>
            _lyapunov.AdjustSlope(this);

        private void DeleteWaveletTempFile() =>
            File.Delete(Properties.Resources.WaveletFile);
    }
}

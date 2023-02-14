using ChaosSoft.Core.Data;
using ChaosSoft.Core.IO;
using ChaosSoft.NumericalMethods;
using ChaosSoft.NumericalMethods.PhaseSpace;
using ChaosSoft.NumericalMethods.Lyapunov;
using ChaosSoft.NumericalMethods.Transform;
using ChaosSoft.MatlabIntegration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TsaToolbox.Models;
using System.Diagnostics;

namespace TsaToolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LyapunovExponents _lyapunov;
        private readonly CommandProcessor _commandProcessor;
        private readonly double[] _zero = new double[] { 0 };

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();

            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            Title += $" v{versionInfo.ProductVersion}";
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
            ch_SignalGraph.Plot(_zero, _zero);
            ch_PseudoPoincareGraph.Plot(_zero, _zero);
            ch_acfGraph.Plot(_zero, _zero);
            an_FnnGraph.Plot(_zero, _zero);
            an_miGraph.Plot(_zero, _zero);

            ch_acfCbox.Content = Properties.Resources.Acf;
            ch_fnnCbox.Content = Properties.Resources.Fnn;
            ch_miCbox.Content = Properties.Resources.Mi;

            _lyapunov.CleanUp(this);
            ch_FftGraph.Plot(_zero, _zero);
            ch_wavPlot.Background = null;
            DeleteTempFiles();
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

        private void ch_buildBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ch_signalCbox.IsChecked.Value)
            {
                ch_SignalGraph.Plot(Source.Data.TimeSeries.XValues, Source.Data.TimeSeries.YValues);
            }

            if (ch_poincareCbox.IsChecked.Value)
            {
                var pPoincare = DelayedCoordinates.GetData(Source.Data.TimeSeries.YValues, 1);
                ch_PseudoPoincareGraph.Plot(pPoincare.XValues, pPoincare.YValues);
            }

            if (ch_acfCbox.IsChecked.Value)
            {
                var autoCor = Statistics.Acf(Source.Data.TimeSeries.YValues);
                ch_acfGraph.PlotY(autoCor);

                int i;

                for (i = 1; i < autoCor.Length; i++)
                {
                    if (Math.Sign(autoCor[i]) != Math.Sign(autoCor[i - 1]))
                    {
                        break;
                    }
                }

                ch_acfCbox.Content = $"{Properties.Resources.Acf} (={i})";
            }

            if (ch_fnnCbox.IsChecked.Value)
            {
                var fnn = new FalseNearestNeighbors(fnn_minDim.ReadInt(), fnn_maxDim.ReadInt(), fnn_tau.ReadInt(), fnn_rt.ReadDouble(), fnn_theiler.ReadInt());
                fnn.Calculate(Source.Data.TimeSeries.YValues);
                an_FnnGraph.Plot(fnn.FalseNeighbors.Keys, fnn.FalseNeighbors.Values);

                int key = fnn.FalseNeighbors.Keys.First(k => fnn.FalseNeighbors[k] == 0);
                ch_fnnCbox.Content = $"{Properties.Resources.Fnn} (={key})";
            }

            if (ch_miCbox.IsChecked.Value)
            {
                var mi = new MutualInformation(mi_partitions.ReadInt(), mi_maxDelay.ReadInt());
                mi.Calculate(Source.Data.TimeSeries.YValues);
                an_miGraph.Plot(mi.EntropySlope.XValues, mi.EntropySlope.YValues);

                double index = mi.EntropySlope.XValues[Array.IndexOf(mi.EntropySlope.YValues, mi.EntropySlope.YValues.Min())];
                ch_miCbox.Content = $"{Properties.Resources.Mi} (={(int)index})";
            }
        }

        private void ch_buildMlBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ch_fftCbox.IsChecked.Value)
            {
                var fftTs = GetFft();
                ch_FftGraph.Plot(fftTs.XValues, fftTs.YValues);
            }

            if (ch_WaveletCbox.IsChecked.Value)
            {
                DeleteWaveletTempChart();
                var brush = GetWavelet(
                    ch_wavChart, 
                    ch_wavChart.Width * 2, 
                    ch_wavChart.Height * 2, 
                    Properties.Resources.WaveletFile);

                ch_wavPlot.Background = brush;
                SetWavPlotRect(ch_wavPlot);
            }
        }


        private void ch_SignalChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                new PreviewForm(Properties.Resources.Signal, "t", "f(t)")
                .SetSize(Settings.PreviewWindowWidth, Settings.PreviewWindowHeight)
                .PlotLine(Source.Data.TimeSeries.XValues, Source.Data.TimeSeries.YValues)
                .ShowDialog();
            }
        }

        private void ch_PseudoPoincareChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var pPoincare = DelayedCoordinates.GetData(Source.Data.TimeSeries.YValues, 1);

                new PreviewForm(Properties.Resources.PseudoPoincare, "f(t)", "f(t+1)")
                    .SetSize(Settings.PreviewWindowWidth, Settings.PreviewWindowHeight)
                    .PlotMap(pPoincare.XValues, pPoincare.YValues)
                    .ShowDialog();
            }
        }

        private void tsp_autocorChart_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var autoCor = Statistics.Acf(Source.Data.TimeSeries.YValues);

                new PreviewForm(Properties.Resources.Acf, "t", "ACF")
                    .SetSize(Settings.PreviewWindowWidth, Settings.PreviewWindowHeight)
                    .PlotLine(autoCor)
                    .ShowDialog();
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

            var outDir = Path.Combine(Source.Data.Folder, Source.Data.FileName + "_out");
            string fName = Path.Combine(outDir, Source.Data.FileName);

            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            if (ch_signalCbox.IsChecked.Value)
            {
                DataWriter.CreateDataFile(fName + "_signal.dat", Format.General(Source.Data.TimeSeries.YValues, "\n", 6));
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

            var scaleX = Settings.SaveChartWidth / plot.Width;
            var scaleY = Settings.SaveChartHeight / plot.Height;

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
                    new Rect(new System.Windows.Point(bounds.X, bounds.Y), new System.Windows.Point(width, height)));
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

        private void ch_FftChart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var previewForm = new PreviewForm(Properties.Resources.Fft, "ω", "F(ω)")
                    .SetSize(Settings.PreviewWindowWidth, Settings.PreviewWindowHeight);

                var data = GetFft();

                previewForm.PlotLine(data.XValues, data.YValues);

                previewForm.ShowDialog();
            }
        }

        private void ch_WavChart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                try
                {
                    DeleteWaveletPreviewTempChart();

                    var previewForm = new PreviewForm(Properties.Resources.Wavelet, "t", "ω")
                        .SetSize(Settings.PreviewWindowWidth, Settings.PreviewWindowHeight);

                    previewForm.Show();
                    previewForm.Topmost = true;

                    var brush = GetWavelet(
                        previewForm.previewChart, 
                        previewForm.grid.ActualWidth, 
                        previewForm.grid.ActualHeight, 
                        Properties.Resources.WaveletPreviewFile);

                    previewForm.previewPlot.Background = brush;
                    SetWavPlotRect(previewForm.previewPlot);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error previewing wavelet:\n" + ex);
                }

            }
        }

        private DataSeries GetFft()
        {
            int logScale = ch_logScaleCbox.IsChecked.Value ? 1 : 0;
            double dt = fft_dt.ReadDouble();
            double omStart = fft_omLeft.ReadDouble();
            double omEnd = fft_omRight.ReadDouble();
            
            return Fourier.GetFourier(Source.Data.TimeSeries.YValues, omStart, omEnd, dt, logScale);
        }

        private ImageBrush GetWavelet(Visual visual, double width, double height, string fileName)
        {
            double tStart = Source.Data.TimeSeries.Min.X;
            double tEnd = Source.Data.TimeSeries.Max.X;

            try
            {
                Wavelet.BuildWavelet(Source.Data.TimeSeries.YValues,
                    fileName,
                    wav_typeCbox.Text,
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

                var brush = new ImageBrush(croppedBitmap)
                {
                    Stretch = Stretch.Fill
                };

                return brush;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to build {Properties.Resources.Wavelet}:\n" + ex.Message);
                return null;
            }
        }

        private void le_k_epsCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) =>
            _lyapunov.AdjustSlope(this);

        public void DeleteTempFiles()
        {
            DeleteWaveletTempChart();
            DeleteWaveletPreviewTempChart();
        }

        private void DeleteWaveletTempChart()
        {
            File.Delete(Properties.Resources.WaveletFile);
        }

        private void DeleteWaveletPreviewTempChart()
        {
            File.Delete(Properties.Resources.WaveletPreviewFile);
        }

        private void SetWavPlotRect(InteractiveDataDisplay.WPF.Plot plot)
        {
            var rect = new InteractiveDataDisplay.WPF.DataRect(
                Source.Data.TimeSeries.Min.X,
                wav_omLeft.ReadDouble(),
                Source.Data.TimeSeries.Max.X,
                wav_omRight.ReadDouble());

            plot.SetPlotRect(rect);
        }
    }
}

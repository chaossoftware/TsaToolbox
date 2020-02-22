using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MathLib.Data;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace TimeSeriesToolbox.Charts
{
    /// <summary>
    /// Interaction logic for SciLineChart.xaml
    /// </summary>
    public partial class SciLineChartPreview : INotifyPropertyChanged
    {
        private ZoomingOptions _zoomingMode;

        public SciLineChartPreview()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection();

            ZoomingMode = ZoomingOptions.Xy;

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        public string AxisY { get; set; }

        public string AxisX { get; set; }


        public ZoomingOptions ZoomingMode
        {
            get { return _zoomingMode; }
            set
            {
                _zoomingMode = value;
                OnPropertyChanged();
            }
        }

        private void ToogleZoomingMode(object sender, RoutedEventArgs e)
        {
            switch (ZoomingMode)
            {
                case ZoomingOptions.None:
                    ZoomingMode = ZoomingOptions.X;
                    break;
                case ZoomingOptions.X:
                    ZoomingMode = ZoomingOptions.Y;
                    break;
                case ZoomingOptions.Y:
                    ZoomingMode = ZoomingOptions.Xy;
                    break;
                case ZoomingOptions.Xy:
                    ZoomingMode = ZoomingOptions.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetZoomOnClick(object sender, RoutedEventArgs e)
        {
            //Use the axis MinValue/MaxValue properties to specify the values to display.
            //use double.Nan to clear it.

            //X.MinValue = double.NaN;
            //X.MaxValue = double.NaN;
            //Y.MinValue = double.NaN;
            //Y.MaxValue = double.NaN;
        }

        public Func<double, string> YFormatter { get; set; }

        public void AddTimeSeries(Timeseries timeseries)
        {
            var lineSeries = new LineSeries
            {
                Values = new ChartValues<ObservablePoint>(),
                PointGeometry = DefaultGeometries.None,
                Fill = Brushes.Transparent,
                StrokeThickness = 0.5
            };

            foreach (var dp in timeseries.DataPoints)
            {
                lineSeries.Values.Add(new ObservablePoint(dp.X, dp.Y));
            }

            SeriesCollection.Add(lineSeries);
        }
        
        public void Clear() =>
            SeriesCollection.Clear();
    }

    public class ZoomingModeCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ZoomingOptions)value)
            {
                case ZoomingOptions.None:
                    return "None";
                case ZoomingOptions.X:
                    return "X";
                case ZoomingOptions.Y:
                    return "Y";
                case ZoomingOptions.Xy:
                    return "XY";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

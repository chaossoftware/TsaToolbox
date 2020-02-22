using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MathLib.Data;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace TimeSeriesToolbox.Charts
{
    /// <summary>
    /// Interaction logic for SciLineChart.xaml
    /// </summary>
    public partial class SciLineChart : UserControl
    {
        public SciLineChart()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection();
            
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        public string AxisY { get; set; }

        public string AxisX { get; set; }

        public string[] Labels { get; set; }

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
}

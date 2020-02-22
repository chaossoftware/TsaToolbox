using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MathLib.Data;
using System.Windows.Controls;
using System.Windows.Media;

namespace TimeSeriesToolbox.Charts
{
    /// <summary>
    /// Interaction logic for SciScatterChart.xaml
    /// </summary>
    public partial class SciScatterChart : UserControl
    {
        public SciScatterChart()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection();

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        public string AxisY { get; set; }

        public string AxisX { get; set; }

        public void AddTimeSeries(Timeseries timeseries)
        {
            var lineSeries = new ScatterSeries
            {
                Values = new ChartValues<ObservablePoint>(),
                Fill = Brushes.Transparent,
                PointGeometry = DefaultGeometries.Circle,
                MaxPointShapeDiameter = 2,
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

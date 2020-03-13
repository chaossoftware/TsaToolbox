using System.Collections;
using System.Windows;

namespace TimeSeriesToolbox
{
    /// <summary>
    /// Interaction logic for PreviewForm.xaml
    /// </summary>
    public partial class PreviewForm : Window
    {
        public PreviewForm(string title, string xAxis, string yAxis)
        {
            InitializeComponent();

            Title = title;
            previewChart.LeftTitle = yAxis;
            previewChart.BottomTitle = xAxis;
        }

        public PreviewForm SetSize(double width, double heigh)
        {
            Width = width;
            Height = heigh;
            return this;
        }

        public PreviewForm PlotLine(IEnumerable x, IEnumerable y)
        {
            lineChart.Plot(x, y);
            return this;
        }

        public PreviewForm PlotLine(IEnumerable y)
        {
            lineChart.PlotY(y);
            return this;
        }

        public PreviewForm PlotMap(IEnumerable x, IEnumerable y)
        {
            markerChart.Plot(x, y);
            return this;
        }
    }
}

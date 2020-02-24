using InteractiveDataDisplay.WPF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

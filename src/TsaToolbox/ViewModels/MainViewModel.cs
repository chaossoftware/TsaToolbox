using ChaosSoft.Core.Data;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using TsaToolbox.Models;

namespace TsaToolbox.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private readonly DataSource _source;

        private string fileInfo;
        private string timeSeriesInfo;
        private string timeStep;

        public MainViewModel(Settings settings, DataSource source)
        {
            _source = source;

            SourceAndSettingsVM = new SourceAndSettingsViewModel(settings, source);
            SourceAndSettingsVM.PropertyChanged += OnSourcePropertyChanged;
        }

        public ViewModelBase SourceAndSettingsVM { get; }


        public string FileInfo
        {
            get => fileInfo;

            set
            {
                fileInfo = value;
                OnPropertyChanged(nameof(FileInfo));
            }
        }

        public string TimeSeriesInfo
        { 
            get => timeSeriesInfo;

            set
            {
                timeSeriesInfo = value;
                OnPropertyChanged(nameof(TimeSeriesInfo));
            }
        }

        public string TimeStep
        {
            get => timeStep;

            set
            {
                timeStep = value;
                OnPropertyChanged(nameof(TimeStep));
            }
        }


        private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SourceAndSettingsViewModel.DataLoaded))
            {
                FileInfo = _source.Data.ToString().Replace("\n", "  ‧  ");
            }

            if (e.PropertyName == nameof(SourceAndSettingsViewModel.TimeSeriesStale))
            {
                string info = $"Col: {_source.SignalColumn}  ‧  Range: [{_source.StartPoint}; {_source.EndPoint}]";

                if (_source.TimeInFirstColumn)
                {
                    double[] xs = _source.Data.GetColumn(0);
                    info += $"  ‧  t = [{xs[_source.StartPoint - 1]}; {xs[_source.EndPoint - 1]}]";
                }

                TimeSeriesInfo = info;

                // dirty hack in case when time in file has G format
                // at big offsets accuracy could be lost due to big integer part
                if (_source.TimeInFirstColumn)
                {
                    FieldInfo field = typeof(SourceData).GetField("_dataColumns", BindingFlags.NonPublic | BindingFlags.Instance);
                    double[][] data = field.GetValue(_source.Data) as double[][];
                    double step = data[0][_source.EachNPoints] - data[0][0];
                    TimeStep = string.Format(CultureInfo.InvariantCulture, "{0:G8}", step);
                }
                else
                {
                    TimeStep = double.NaN.ToString();
                }
              }
        }
    }
}

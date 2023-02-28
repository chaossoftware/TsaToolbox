using System.ComponentModel;
using System.Globalization;
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

                TimeStep = _source.TimeInFirstColumn ?
                    string.Format(CultureInfo.InvariantCulture, "{0:G8}", _source.Data.Step) :
                    "NaN";
            }
        }
    }
}

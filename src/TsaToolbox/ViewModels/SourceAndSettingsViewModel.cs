using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TsaToolbox.Commands;
using TsaToolbox.Models;

namespace TsaToolbox.ViewModels
{
    public class SourceAndSettingsViewModel : ViewModelBase
    {
        private readonly Settings _settings;
        private readonly DataSource _source;

        private Brush setButtonColor;
        private IEnumerable<int> dataColumnsCount;
        private bool dataLoaded;
        private bool timeSeriesStale;

        public SourceAndSettingsViewModel(Settings settings, DataSource source)
        {
            _settings = settings;
            _source = source;

            LoadDataCommand = new LoadDataCommand(source, this);
            SetTimeseriesCommand = new SetTimeseriesCommand(source, this);

            // Default values.
            PreviewWindowWidth = "1024";
            PreviewWindowHeight = "600";
            SaveChartWidth = "215";
            SaveChartHeight = "160";
            EachNPoints = "1";

            TimeSeriesStale = false;
            //ResetSetButton();
        }

        public ICommand LoadDataCommand { get; }

        public ICommand SetTimeseriesCommand { get; }

        public string LinesToSkip 
        {
            get => _source.LinesToSkip.ToString();

            set
            {
                _source.LinesToSkip = int.Parse(value);
                OnPropertyChanged(nameof(LinesToSkip));
            }
        }

        public string LinesToRead
        {
            get => _source.LinesToRead.ToString();

            set
            {
                _source.LinesToRead = int.Parse(value);
                OnPropertyChanged(nameof(LinesToRead));
            }
        }

        public bool TimeInFirstColumn
        {
            get => _source.TimeInFirstColumn;

            set
            {
                _source.TimeInFirstColumn = value;

                if (value && _source.SignalColumn == 1)
                {
                    SignalColumn = 2;
                }

                OnTsPropertyChanged(nameof(TimeInFirstColumn));
            }
        }

        public IEnumerable<int> DataColumnsCount
        {
            get => dataColumnsCount;

            set
            {
                dataColumnsCount = value;
                OnPropertyChanged(nameof(DataColumnsCount));
            }
        }

        public int SignalColumn
        {
            get => _source.SignalColumn;

            set
            {
                _source.SignalColumn = value;
                OnTsPropertyChanged(nameof(SignalColumn));
            }
        }

        public string StartPoint
        {
            get => _source.StartPoint.ToString();

            set
            {
                _source.StartPoint = int.Parse(value);
                OnTsPropertyChanged(nameof(StartPoint));
            }
        }

        public string EndPoint
        {
            get => _source.EndPoint.ToString();

            set
            {
                _source.EndPoint = int.Parse(value);
                OnTsPropertyChanged(nameof(EndPoint));
            }
        }

        public string EachNPoints
        {
            get => _source.EachNPoints.ToString();

            set
            {
                _source.EachNPoints = int.Parse(value);
                OnTsPropertyChanged(nameof(EachNPoints));
            }
        }

        public Brush SetButtonColor
        {
            get => setButtonColor;

            set
            {
                setButtonColor = value;
                OnPropertyChanged(nameof(SetButtonColor));
            }
        }


        public string PreviewWindowWidth
        {
            get => _settings.PreviewWindowWidth.ToString();

            set
            {
                _settings.PreviewWindowWidth = int.Parse(value);
                OnPropertyChanged(nameof(PreviewWindowWidth));
            }
        }

        public string PreviewWindowHeight
        {
            get => _settings.PreviewWindowHeight.ToString();

            set
            {
                _settings.PreviewWindowHeight = int.Parse(value);
                OnPropertyChanged(nameof(PreviewWindowHeight));
            }
        }

        public string SaveChartWidth
        {
            get => _settings.SaveChartWidth.ToString();

            set
            {
                _settings.SaveChartWidth = int.Parse(value);
                OnPropertyChanged(nameof(SaveChartWidth));
            }
        }

        public string SaveChartHeight
        {
            get => _settings.SaveChartHeight.ToString();

            set
            {
                _settings.SaveChartHeight = int.Parse(value);
                OnPropertyChanged(nameof(SaveChartHeight));
            }
        }

        public bool DataLoaded
        {
            get => dataLoaded;

            set
            {
                dataLoaded = value;
                OnPropertyChanged(nameof(DataLoaded));
            }
        }

        public bool TimeSeriesStale
        {
            get => timeSeriesStale;

            set
            {
                if (timeSeriesStale != value)
                {
                    timeSeriesStale = value;
                    SetButtonColor = value ? Brushes.IndianRed : SystemColors.ControlLightBrush;
                    OnPropertyChanged(nameof(TimeSeriesStale));
                }
            }
        }

        protected void OnTsPropertyChanged(string propertyName)
        {
            TimeSeriesStale = true;
            OnPropertyChanged(propertyName);
        }
    }
}

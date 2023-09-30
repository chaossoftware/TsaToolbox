using System.Collections.Generic;
using System.Windows.Input;
using TsaToolbox.Commands;
using TsaToolbox.Models;

namespace TsaToolbox.ViewModels
{
    public class SourceAndSettingsViewModel : ViewModelBase
    {
        private readonly Settings _settings;
        private readonly DataSource _source;

        private IEnumerable<int> dataColumnsCount;
        private bool dataLoaded;
        private bool timeSeriesStale;
        private bool multilineData;

        public SourceAndSettingsViewModel(Settings settings, DataSource source)
        {
            _settings = settings;
            _source = source;

            LoadDataCommand = new LoadDataCommand(source, this);
            SetTimeseriesCommand = new SetTimeseriesCommand(source, this);

            // Default values.
            AxisTickLabelSize = 13;
            AxisLabelSize = 14;
            SaveChartWidth = 320;
            SaveChartHeight = 240;
            SeparateOutputDir = true;
            OutputDir = "Results";
            EachNPoints = 1;

            TimeSeriesStale = false;
        }

        public ICommand LoadDataCommand { get; }

        public ICommand SetTimeseriesCommand { get; }

        public int LinesToSkip 
        {
            get => _source.LinesToSkip;

            set
            {
                _source.LinesToSkip = value;
                OnPropertyChanged(nameof(LinesToSkip));
            }
        }

        public int LinesToRead
        {
            get => _source.LinesToRead;

            set
            {
                _source.LinesToRead = value;
                OnPropertyChanged(nameof(LinesToRead));
            }
        }

        public bool ReadFromBytes
        {
            get => _source.ReadFromBytes;

            set
            {
                _source.ReadFromBytes = value;
                OnPropertyChanged(nameof(ReadFromBytes));
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

        public int StartPoint
        {
            get => _source.StartPoint;

            set
            {
                _source.StartPoint = value > 0 && value < _source.Data.LinesCount ? value : 0;
                OnTsPropertyChanged(nameof(StartPoint));
            }
        }

        public int EndPoint
        {
            get => _source.EndPoint;

            set
            {
                _source.EndPoint = value > 0 && value < _source.Data?.LinesCount ? value : _source.Data.LinesCount;
                OnTsPropertyChanged(nameof(EndPoint));
            }
        }

        public int EachNPoints
        {
            get => _source.EachNPoints;

            set
            {
                _source.EachNPoints = value > 0 && value < _source.Data?.LinesCount ? value : 1;
                OnTsPropertyChanged(nameof(EachNPoints));
            }
        }

        public int AxisTickLabelSize
        {
            get => _settings.AxisTickLabelSize;

            set
            {
                _settings.AxisTickLabelSize = value;
                OnPropertyChanged(nameof(AxisTickLabelSize));
            }
        }

        public int AxisLabelSize
        {
            get => _settings.AxisLabelSize;

            set
            {
                _settings.AxisLabelSize = value;
                OnPropertyChanged(nameof(AxisLabelSize));
            }
        }

        public bool ShowGridLines
        {
            get => _settings.ShowGridLines;

            set
            {
                _settings.ShowGridLines = value;
                OnPropertyChanged(nameof(ShowGridLines));
            }
        }

        public int SaveChartWidth
        {
            get => _settings.SaveChartWidth;

            set
            {
                _settings.SaveChartWidth = value;
                OnPropertyChanged(nameof(SaveChartWidth));
            }
        }

        public int SaveChartHeight
        {
            get => _settings.SaveChartHeight;

            set
            {
                _settings.SaveChartHeight = value;
                OnPropertyChanged(nameof(SaveChartHeight));
            }
        }

        public string OutputDir
        {
            get => _settings.OutputDir;

            set
            {
                _settings.OutputDir = value;
                OnPropertyChanged(nameof(OutputDir));
            }
        }

        public bool SeparateOutputDir
        {
            get => _settings.SeparateOutputDir;

            set
            {
                _settings.SeparateOutputDir = value;
                OnPropertyChanged(nameof(SeparateOutputDir));
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
                    OnPropertyChanged(nameof(TimeSeriesStale));
                }
            }
        }

        public bool MultilineData
        {
            get => multilineData;

            set
            {
                multilineData = value;
                OnPropertyChanged(nameof(MultilineData));
            }
        }

        protected void OnTsPropertyChanged(string propertyName)
        {
            TimeSeriesStale = true;
            OnPropertyChanged(propertyName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Input;
using TsaToolbox.Commands;
using TsaToolbox.Models;

namespace TsaToolbox.ViewModels
{
    public class SourceAndSettingsViewModel
    {
        private readonly Settings _settings;
        private readonly DataSource _source;

        private IEnumerable<int> dataColumnsCount;
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
            SaveChartScaling = 1;
            SeparateOutputDir = true;
            OutputDir = "Results";
            EachNPoints = 1;

            TimeSeriesStale = false;
        }

        public event EventHandler TimeSeriesSet;

        public event EventHandler DataLoaded;


        public ICommand LoadDataCommand { get; }

        public ICommand SetTimeseriesCommand { get; }

        [Notify]
        public int LinesToSkip 
        {
            get => _source.LinesToSkip;
            set => _source.LinesToSkip = value;
        }

        [Notify]
        public int LinesToRead
        {
            get => _source.LinesToRead;
            set => _source.LinesToRead = value;
        }

        [Notify]
        public bool ReadFromBytes
        {
            get => _source.ReadFromBytes;
            set => _source.ReadFromBytes = value;
        }

        [Notify]
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

                StaleTimeSeries();
            }
        }

        [Notify]
        public IEnumerable<int> DataColumnsCount
        {
            get => dataColumnsCount;
            set => dataColumnsCount = value;
        }

        [Notify]
        public int SignalColumn
        {
            get => _source.SignalColumn;

            set
            {
                _source.SignalColumn = value;
                StaleTimeSeries();
            }
        }

        [Notify]
        public int StartPoint
        {
            get => _source.StartPoint;

            set
            {
                _source.StartPoint = value > 0 && value < _source.Data.LinesCount ? value : 0;
                StaleTimeSeries();
            }
        }

        [Notify]
        public int EndPoint
        {
            get => _source.EndPoint;

            set
            {
                _source.EndPoint = value > 0 && value < _source.Data?.LinesCount ? value : _source.Data.LinesCount;
                StaleTimeSeries();
            }
        }

        [Notify]
        public int EachNPoints
        {
            get => _source.EachNPoints;

            set
            {
                _source.EachNPoints = value > 0 && value < _source.Data?.LinesCount ? value : 1;
                StaleTimeSeries();
            }
        }

        [Notify]
        public int AxisTickLabelSize
        {
            get => _settings.AxisTickLabelSize;
            set => _settings.AxisTickLabelSize = value;
        }

        [Notify]
        public int AxisLabelSize
        {
            get => _settings.AxisLabelSize;
            set => _settings.AxisLabelSize = value;
        }

        [Notify]
        public bool ShowGridLines
        {
            get => _settings.ShowGridLines;
            set => _settings.ShowGridLines = value;
        }

        [Notify]
        public int SaveChartWidth
        {
            get => _settings.SaveChartWidth;
            set => _settings.SaveChartWidth = value;
        }

        [Notify]
        public int SaveChartHeight
        {
            get => _settings.SaveChartHeight;
            set => _settings.SaveChartHeight = value;
        }

        [Notify]
        public double SaveChartScaling
        {
            get => _settings.SaveChartScaling;
            set => _settings.SaveChartScaling = value;
        }

        [Notify]
        public string OutputDir
        {
            get => _settings.OutputDir;
            set => _settings.OutputDir = value;
        }

        [Notify]
        public bool SeparateOutputDir
        {
            get => _settings.SeparateOutputDir;
            set => _settings.SeparateOutputDir = value;
        }

        [Notify]
        public bool TimeSeriesStale
        {
            get => timeSeriesStale;
            set => timeSeriesStale = value;
        }

        [Notify]
        public bool MultilineData
        {
            get => multilineData;
            set => multilineData = value;
        }

        public void StaleTimeSeries()
        {
            if (!timeSeriesStale)
            {
                TimeSeriesStale = true;
            }
        }

        public void SetTimeSeries()
        {
            TimeSeriesStale = false;
            TimeSeriesSet?.Invoke(this, new EventArgs());

        }

        public void FireDataLoadedEvent() =>
            DataLoaded?.Invoke(this, new EventArgs());
    }
}

using ChaosSoft.Core.Data;
using System;
using System.Reflection;
using TsaToolbox.Models;
using TsaToolbox.Models.Setups;

namespace TsaToolbox.ViewModels;

public class MainViewModel
{
    private const string BaseTitle = "TSA toolbox";
    private readonly DataSource _source;
    private double timeStep;

    public MainViewModel(Settings settings, DataSource source, Setup setup)
    {
        _source = source;

        SourceAndSettingsVM = new SourceAndSettingsViewModel(settings, source);
        SourceAndSettingsVM.DataLoaded += UpdateFileInfo;
        SourceAndSettingsVM.TimeSeriesSet += UpdateTimeSeriesInfo;

        
        FftVM = new FftViewModel(setup.Fft);
        WaveletVM = new WaveletViewModel(setup.Wavelet);
        Title = BaseTitle;
    }

    public SourceAndSettingsViewModel SourceAndSettingsVM { get; }

    public FftViewModel FftVM { get; }

    public WaveletViewModel WaveletVM { get; }

    [Notify]
    public string FileInfo { get; set; }

    [Notify]
    public string TimeSeriesInfo { get; set; }

    [Notify]
    public double TimeStep
    {
        get => timeStep;

        set
        {
            timeStep = value;
            FftVM.Dt = value;
        }
    }

    [Notify]
    public string Title { get; set; }

    private void UpdateFileInfo(object sender, EventArgs e)
    {
        FileInfo = $"Lines ‧ {_source.Data.LinesCount}  ::  Columns ‧ {_source.Data.ColumnsCount}";
        Title = BaseTitle + ": " + _source.Data.FileName;
    }

    private void UpdateTimeSeriesInfo(object sender, EventArgs e)
    {
        string info = $"Column ‧ {_source.SignalColumn}  ::  Range ‧ [{_source.StartPoint}; {_source.EndPoint}]";

        if (_source.TimeInFirstColumn)
        {
            double[] xs = _source.Data.GetColumn(0);
            info += $"  ::  t = [{xs[_source.StartPoint - 1]}; {xs[_source.EndPoint - 1]}]";
        }

        TimeSeriesInfo = info;

        // dirty hack in case when time in file has G format
        // at big offsets accuracy could be lost due to big integer part
        if (_source.TimeInFirstColumn)
        {
            FieldInfo field = typeof(SourceData).GetField("_dataColumns", BindingFlags.NonPublic | BindingFlags.Instance);
            double[][] data = field.GetValue(_source.Data) as double[][];
            double step = data[0][_source.EachNPoints] - data[0][0];
            TimeStep = step;
        }
        else
        {
            TimeStep = double.NaN;
        }
    }
}

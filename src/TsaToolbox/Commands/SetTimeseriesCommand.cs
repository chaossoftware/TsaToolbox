using System.Windows;
using TsaToolbox.Models;
using TsaToolbox.ViewModels;

namespace TsaToolbox.Commands
{
    public class SetTimeseriesCommand : CommandBase
    {
        private readonly DataSource _source;
        private readonly SourceAndSettingsViewModel _viewModel;

        public SetTimeseriesCommand(DataSource source, SourceAndSettingsViewModel viewModel)
        {
            _source = source;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_source.Data == null)
            {
                MessageBox.Show(Properties.Resources.MsgEmptyFile);
                return;
            }

            _source.Data.SetTimeSeries(
                _source.SignalColumn - 1,
                _source.StartPoint - 1,
                _source.EndPoint,
                _source.EachNPoints,
                _source.TimeInFirstColumn
            );

            _viewModel.TimeSeriesStale = false;
        }
    }
}

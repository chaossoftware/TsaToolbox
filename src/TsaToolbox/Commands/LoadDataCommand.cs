﻿using ChaosSoft.Core;
using ChaosSoft.Core.Data;
using Microsoft.Win32;
using System;
using System.Windows;
using TsaToolbox.Models;
using TsaToolbox.ViewModels;

namespace TsaToolbox.Commands
{
    public class LoadDataCommand : CommandBase
    {
        private readonly DataSource _source;
        private readonly SourceAndSettingsViewModel _viewModel;

        public LoadDataCommand(DataSource source, SourceAndSettingsViewModel viewModel)
        {
            _source = source;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            bool parameterized = (bool)parameter;

            var openFileDialog = new OpenFileDialog()
            {
                Filter = "All files|*.*|TimeSeries data|*.dat *.txt *.csv"
            };

            if (openFileDialog.ShowDialog().Value)
            {
                try
                {
                    OpenFile(openFileDialog.FileName, parameterized);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Unable to read file: " + ex.Message);
                }
            }
        }

        public void OpenFile(string fileName, bool parameterized)
        {
            MainWindow.Instance.CleanUp();

            _source.Data = parameterized ?
                new SourceData(fileName, _source.LinesToSkip, _source.LinesToRead) :
                new SourceData(fileName);

            _viewModel.DataColumnsCount = ArrayUtil.GenerateArray(_source.Data.ColumnsCount, 1, 1);

            if (_source.SignalColumn > _source.Data.ColumnsCount || _source.SignalColumn == 0)
            {
                _viewModel.TimeInFirstColumn = false;
                _viewModel.SignalColumn = 1;
            }

            if (_source.EndPoint > _source.Data.LinesCount || _source.EndPoint == 0)
            {
                _viewModel.StartPoint = "1";
                _viewModel.EndPoint = _source.Data.LinesCount.ToString();
            }

            _viewModel.DataLoaded = true;
            _viewModel.SetTimeseriesCommand.Execute(null);
        }
    }
}
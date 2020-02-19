﻿using MathLib.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeSeriesToolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LyapunovExponents _lyapunov;
        private SourceData sourceData;
        private Charts charts;

        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            InitializeComponent();
            _lyapunov = new LyapunovExponents(this);
        }

        private void ts_btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "All files|*.*|Time series data|*.dat *.txt *.csv"
            };

            openFileDialog.ShowDialog();

            if (string.IsNullOrEmpty(openFileDialog.FileName))
            {
                return;
            }

            try
            {
                CleanUp();

                if (ts_parameterizedOpenCbox.IsChecked.Value)
                {
                    sourceData = new SourceData(
                        openFileDialog.FileName,
                        ts_LinesToSkipTbox.ReadInt(), 
                        ts_LinesToReadTbox.ReadInt());
                }
                else
                {
                    sourceData = new SourceData(openFileDialog.FileName);
                }

                FillUiWithData();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Unable to read file:" + ex.Message);
            }
        }

        private void CleanUp()
        {
            sourceData = null;
            //chartSignal.ClearChart();
            //chartPoincare.ClearChart();
            //chartFft.ClearChart();
            //chartLyapunov.ClearChart();

            //wav_plotPBox.Image = null;
            //routines.Lyapunov = null;
            //routines.DeleteTempFiles();
        }

        private void FillUiWithData()
        {
            statusFileInfoText.Text = sourceData.ToString().Replace("\n", " | ");
            ts_endPointTbox.Text = sourceData.LinesCount.ToString();
        }

        private bool RefreshTimeSeries()
        {
            if (sourceData == null)
            {
                MessageBox.Show(StringData.MsgEmptyFile);
                return false;
            }

            sourceData.SetTimeSeries(
                ts_tsColumnTbox.ReadInt() - 1,
                ts_startPointTbox.ReadInt() - 1,
                ts_endPointTbox.ReadInt() - 1,
                ts_eachNPointsTbox.ReadInt(),
                ts_timestampColumnCbox.IsChecked.Value
            );

            statusTsInfoText.Text = $"Range [{ts_startPointTbox.Text}; {ts_endPointTbox.Text}]";

            statusDtTbox.Text = ts_timestampColumnCbox.IsChecked.Value ?
                string.Format(CultureInfo.InvariantCulture, "{0:G8}", sourceData.Step) :
                "NaN";

            return true;
        }

        private void ts_parameterizedOpenCbox_Unchecked(object sender, RoutedEventArgs e)
        {
            ts_LinesToSkipTbox.IsEnabled = false;
            ts_LinesToReadTbox.IsEnabled = false;
        }

        private void ts_parameterizedOpenCbox_Checked(object sender, RoutedEventArgs e)
        {
            ts_LinesToSkipTbox.IsEnabled = true;
            ts_LinesToReadTbox.IsEnabled = true;
        }

        private void ts_setTimeseriesBtn_Click(object sender, RoutedEventArgs e) =>
            RefreshTimeSeries();

        private void le_calculateBtn_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() => _lyapunov.Calculate(sourceData.TimeSeries.YValues))
                    .Start();
        }

        private void le_rosRad_Checked(object sender, RoutedEventArgs e) =>
            le_rosGbox.Visibility = Visibility.Visible;

        private void le_rosRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_rosGbox.Visibility = Visibility.Hidden;

        private void le_kantzRad_Checked(object sender, RoutedEventArgs e) =>
            le_kantzGbox.Visibility = Visibility.Visible;

        private void le_kantzRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_kantzGbox.Visibility = Visibility.Hidden;

        private void le_wolfRad_Checked(object sender, RoutedEventArgs e) =>
            le_wolfGbox.Visibility = Visibility.Visible;

        private void le_wolfRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_wolfGbox.Visibility = Visibility.Hidden;

        private void le_ssRad_Checked(object sender, RoutedEventArgs e) =>
            le_ssGbox.Visibility = Visibility.Visible;

        private void le_ssRad_Unchecked(object sender, RoutedEventArgs e) =>
            le_ssGbox.Visibility = Visibility.Hidden;

    }
}

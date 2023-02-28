using ChaosSoft.Core.Data;
using ChaosSoft.NumericalMethods;
using ChaosSoft.NumericalMethods.Lyapunov;
using ChaosSoft.NumericalMethods.Transform;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using TsaToolbox.Commands;
using TsaToolbox.ViewModels;

namespace TsaToolbox
{
    public class CommandProcessor
    {
        private const string PlotCmd = "plot";
        private const string OpenCmd = "open";
        private const string ClearCmd = "clear";
        private const string HelpCmd = "help";
        private const string LleR = "lle_r";
        private const string LleK = "lle_k";
        private const string LleW = "lle_w";
        private const string LeSpec = "le_spec";

        private readonly List<WpfPlot> _chartsList;

        public Dictionary<string, string> Commands { get; } = new Dictionary<string, string>()
        {
            { OpenCmd, OpenCmd + " .+" },
            { PlotCmd, PlotCmd + " [A-z]+" },
            { LeSpec, LeSpec },
            { LleW, LleW },
            { ClearCmd, ClearCmd },
            { HelpCmd, HelpCmd },
        };

        private MainWindow window;
        private RichTextBox _console;
        private readonly SolidColorBrush consoleBrush;
        private readonly System.Drawing.Color chartBrush;

        public CommandProcessor(RichTextBox console, MainWindow window)
        {
            this.window = window;
            _console = console;
            _chartsList = new List<WpfPlot>();
            consoleBrush = new BrushConverter().ConvertFromString("#FFC7C7C7") as SolidColorBrush;
            chartBrush = System.Drawing.ColorTranslator.FromHtml("#FFC7C7C7");
            AddParagraph();
        }

        public string LastCommand { get; protected set; }

        public void ProcessCommand(string command)
        {
            LastCommand = command;

            if (Commands.Values.Any(c => Regex.IsMatch(command, c)))
            {
                try
                {
                    CommandSwitch(command);
                }
                catch (Exception ex)
                {
                    PrintError(ex.Message);
                }
            }
            else
            {
                PrintError("unknown command, type 'help' to see list of available commands.");
            }

            AddParagraph();
        }

        private void CommandSwitch(string command)
        {
            if (command.StartsWith(OpenCmd))
            {
                OpenFile(command.Replace(OpenCmd, "").Trim());
            }
            else if (command.StartsWith(PlotCmd))
            {
                Plot(command.Replace(PlotCmd, "").Trim());
            }
            else if (command.Equals(HelpCmd))
            {
                PrintHelp();
            }
            else if (command.Equals(ClearCmd))
            {
                Clear();
            }
            else if (command.Equals(LeSpec))
            {
                CalculateLeSpec();
            }
            else if (command.Equals(LleW))
            {
                CalculateLleWolf();
            }
        }

        private void PrintHelp() =>
            PrintInfo("Available commands:\n - " + string.Join("\n - ", Commands.Keys));

        private void Clear()
        {
            foreach (var chart in _chartsList)
            {
                chart.Plot.Clear();
            }

            _chartsList.Clear();
            _console.Document.Blocks.Clear();
            window.tboxConsoleSecondary.Clear();
        }

        private void OpenFile(string file)
        {
            try
            {
                new LoadDataCommand(window.Source, window.SettingsView.DataContext as SourceAndSettingsViewModel).OpenFile(file, false);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        private void Plot(string chart)
        {
            switch (chart)
            {
                case "signal":
                    AddLineChart(window.Source.Data.TimeSeries);
                    break;
                case "attractor":
                    var pPoincare = DelayedCoordinates.GetData(window.Source.Data.TimeSeries.YValues, 1);
                    AddPointsChart(pPoincare);
                    break;
                case "acf":
                    var autoCor = Statistics.Acf(window.Source.Data.TimeSeries.YValues);
                    AddLineChart(autoCor);
                    break;
                default:
                    PrintError($"unknown chart type '{chart}'");
                    break;
            }
        }

        private void AddParagraph()
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new InlineUIContainer(new TextBlock(new Run(Properties.Resources.Cmd))));
            paragraph.Inlines.Add(new Run());
            _console.Document.Blocks.Add(paragraph);

            _console.CaretPosition = paragraph.Inlines.LastInline.ContentEnd;
        }

        private void PrintResult(string result, Brush brush)
        {
            var paragraph = new Paragraph();
            var run = new Run(result)
            {
                Foreground = brush
            };

            paragraph.Inlines.Add(run);
            _console.Document.Blocks.Add(paragraph);
        }

        private void PrintResult(string result) => PrintResult(result, Brushes.YellowGreen);

        private void PrintInfo(string result) => PrintResult(result, consoleBrush);

        private void PrintError(string result) => PrintResult(result, Brushes.Red);

        private WpfPlot AddLineChart(DataSeries series)
        {
            var chart = AddChart();
            chart.Plot.AddScatter(series.XValues, series.YValues, chartBrush);
            return chart;
        }

        private WpfPlot AddLineChart(double[] series)
        {
            var chart = AddChart();
            chart.Plot.AddSignal(series, 1, chartBrush);
            return chart;
        }

        private WpfPlot AddPointsChart(DataSeries series)
        {
            var chart = AddChart();
            chart.Plot.AddScatterPoints(series.XValues, series.YValues, chartBrush);
            return chart;
        }

        private WpfPlot AddChart()
        {
            var chart = new WpfPlot
            {
                Width = 270,
                Height = 200,
                FontFamily = new FontFamily("Courier New")
            };

            chart.Plot.Style(dataBackground: System.Drawing.Color.Transparent);
            chart.Plot.Style(figureBackground: System.Drawing.Color.Transparent);

            chart.Plot.XAxis.TickLabelStyle(color: chartBrush);
            chart.Plot.XAxis.Color(chartBrush);
            chart.Plot.XAxis2.Color(chartBrush);
            chart.Plot.YAxis.TickLabelStyle(color: chartBrush);
            chart.Plot.YAxis.Color(chartBrush);
            chart.Plot.YAxis2.Color(chartBrush);
            chart.Plot.Grid(enable: false);

            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new InlineUIContainer(chart));
            _console.Document.Blocks.Add(paragraph);
            _chartsList.Add(chart);
            return chart;
        }
    
        private void CalculateLeSpec()
        {
            window.tboxConsoleSecondary.Clear();
            var leSpec = new LeSpecSanoSawada(2);
            window.tboxConsoleSecondary.AppendText(leSpec.ToString());
            leSpec.Calculate(window.Source.Data.TimeSeries.YValues);
            PrintResult(leSpec.GetResultAsString());
            window.tboxConsoleSecondary.AppendText("\nLog:\n");
            window.tboxConsoleSecondary.AppendText(leSpec.Log.ToString());
        }

        private void CalculateLleWolf()
        {
            window.tboxConsoleSecondary.Clear();
            var leSpec = new LleWolf(2);
            window.tboxConsoleSecondary.AppendText(leSpec.ToString());
            leSpec.Calculate(window.Source.Data.TimeSeries.YValues);
            PrintResult(leSpec.GetResultAsString());
            window.tboxConsoleSecondary.AppendText("\nLog:\n");
            window.tboxConsoleSecondary.AppendText(leSpec.Log.ToString());
        }
    }
}

using InteractiveDataDisplay.WPF;
using MathLib.NumericalMethods;
using MathLib.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace TimeSeriesToolbox
{
    public class CommandProcessor
    {
        private const string PlotCmd = "plot";
        private const string OpenCmd = "open";
        private const string ClearCmd = "clear";
        private const string HelpCmd = "help";

        public Dictionary<string, string> Commands { get; } = new Dictionary<string, string>()
        {
            { PlotCmd, PlotCmd + " [A-z]+" },
            { OpenCmd, OpenCmd + " .+" },
            { ClearCmd, ClearCmd },
            { HelpCmd, HelpCmd },
        };

        private MainWindow window;
        private RichTextBox _console;
        private SolidColorBrush consoleBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFC7C7C7");
        private readonly double[] _zero = new double[] { 0 };

        private List<Chart> chartsList = new List<Chart>();

        public CommandProcessor(RichTextBox console, MainWindow window)
        {
            this.window = window;
            _console = console;
            AddParagraph();
        }

        public void ProcessCommand(string command)
        {
            if (Commands.Values.Any(c => Regex.IsMatch(command, c)))
            {
                CommandSwitch(command);
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
        }

        private void PrintHelp() =>
            PrintResult("Available commands:\n - " + string.Join("\n - ", Commands.Keys));

        private void Clear()
        {
            foreach(var plot in chartsList)
            {
                var markerPlot = plot.Content as CircleMarkerGraph;

                if (markerPlot != null)
                {
                    markerPlot.Plot(_zero, _zero);
                    plot.Content = null;
                }
            }

            chartsList.Clear();
            _console.Document.Blocks.Clear();
        }

        private void OpenFile(string file)
        {
            try
            {
                window.OpenFile(file);
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
        }

        private void Plot(string chart)
        {
            try
            {
                switch (chart)
                {
                    case "signal":
                        AddLineChart().Plot(window.sourceData.TimeSeries.XValues, window.sourceData.TimeSeries.YValues);
                        break;
                    case "poincare":
                        var pPoincare = PseudoPoincareMap.GetMapDataFrom(window.sourceData.TimeSeries.YValues, 1);
                        AddMarkerChart().Plot(pPoincare.XValues, pPoincare.YValues);
                        break;
                    case "acf":
                        var autoCor = new AutoCorrelationFunction().GetFromSeries(window.sourceData.TimeSeries.YValues);
                        AddLineChart().PlotY(autoCor);
                        break;
                    default:
                        PrintError($"unknown chart type '{chart}'");
                        break;
                }

            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
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

        private void PrintResult(string result) => PrintResult(result, consoleBrush);

        private void PrintError(string result) => PrintResult(result, Brushes.Red);

        private LineGraph AddLineChart()
        {
            var chart = AddChart();

            var linePlot = new LineGraph
            {
                IsAutoFitEnabled = true,
                Stroke = consoleBrush,
                StrokeThickness = 0.5
            };

            chart.Content = linePlot;
            return linePlot;
        }

        private CircleMarkerGraph AddMarkerChart()
        {
            var chart = AddChart();

            var markerPlot = new CircleMarkerGraph
            {
                IsAutoFitEnabled = true,
                Stroke = consoleBrush,
                Min = 1,
                Max = 4
            };

            chart.Content = markerPlot;
            return markerPlot;
        }

        private Chart AddChart()
        {
            var chart = new Chart
            {
                LegendVisibility = Visibility.Hidden,
                Width = 270,
                Height = 200,
                Background = null,
                Foreground = null,
                FontFamily = new FontFamily("Courier New")
            };

            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new InlineUIContainer(chart));
            _console.Document.Blocks.Add(paragraph);
            chartsList.Add(chart);
            return chart;
        }
    }
}

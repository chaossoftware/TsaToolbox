using InteractiveDataDisplay.WPF;
using MathLib.NumericalMethods;
using MathLib.NumericalMethods.Lyapunov;
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
        private const string LleR = "lle_r";
        private const string LleK = "lle_k";
        private const string LleW = "lle_w";
        private const string LeSpec = "le_spec";

        private readonly List<Chart> _chartsList;

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

        public CommandProcessor(RichTextBox console, MainWindow window)
        {
            this.window = window;
            _console = console;
            _chartsList = new List<Chart>();
            consoleBrush = new BrushConverter().ConvertFromString("#FFC7C7C7") as SolidColorBrush;
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
            var markerCharts = _chartsList.Where(c => c.Content is CircleMarkerGraph);

            foreach (var chart in markerCharts)
            {
               chart.Content = null;
            }

            _chartsList.Clear();
            _console.Document.Blocks.Clear();
            window.tboxConsoleSecondary.Clear();
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
            switch (chart)
            {
                case "signal":
                    AddLineChart().Plot(window.sourceData.TimeSeries.XValues, window.sourceData.TimeSeries.YValues);
                    break;
                case "attractor":
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

        private LineGraph AddLineChart()
        {
            var chart = AddChart();

            var linePlot = new LineGraph
            {
                IsAutoFitEnabled = true,
                Stroke = consoleBrush,
                StrokeThickness = 0.5,
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
            _chartsList.Add(chart);
            return chart;
        }
    
        private void CalculateLeSpec()
        {
            window.tboxConsoleSecondary.Clear();
            var leSpec = new LesSanoSawada(window.sourceData.TimeSeries.YValues);
            window.tboxConsoleSecondary.AppendText(leSpec.ToString());
            leSpec.Calculate();
            PrintResult(leSpec.GetResult());
            window.tboxConsoleSecondary.AppendText("\nLog:\n");
            window.tboxConsoleSecondary.AppendText(leSpec.Log.ToString());
        }

        private void CalculateLleWolf()
        {
            window.tboxConsoleSecondary.Clear();
            var leSpec = new LleWolf(window.sourceData.TimeSeries.YValues);
            window.tboxConsoleSecondary.AppendText(leSpec.ToString());
            leSpec.Calculate();
            PrintResult(leSpec.GetResult());
            window.tboxConsoleSecondary.AppendText("\nLog:\n");
            window.tboxConsoleSecondary.AppendText(leSpec.Log.ToString());
        }
    }
}

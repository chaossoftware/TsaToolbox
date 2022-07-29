using ChaosSoft.Core.Data;

namespace TsaToolbox.Models
{
    public class DataSource
    {
        // Data file settings.

        public bool ParameterizedOpen { get; set; }

        public int LinesToSkip { get; set; }

        public int LinesToRead { get; set; }

        // Timeseries settings.

        public bool TimeInFirstColumn { get; set; }

        public int SignalColumn { get; set; }

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }

        public int EachNPoints { get; set; }

        public SourceData Data { get; set; }
    }
}

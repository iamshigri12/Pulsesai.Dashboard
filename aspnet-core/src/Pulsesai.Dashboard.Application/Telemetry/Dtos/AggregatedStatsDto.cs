using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry.Dtos
{
    public class AggregatedStatsDto
    {
        public int Count { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Avg { get; set; }
        public double StdDev { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry.Dtos
{
    public class SensorReadingDto
    {
        public Guid Id { get; set; }
        public string SensorId { get; set; }
        public double Value { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}

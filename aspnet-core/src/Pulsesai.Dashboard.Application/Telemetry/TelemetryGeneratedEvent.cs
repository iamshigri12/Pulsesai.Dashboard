using Abp.Events.Bus;
using Pulsesai.Dashboard.Pulses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry
{
    public class TelemetryGeneratedEvent : EventData
    {
        public List<SensorReading> Readings { get; }

        public TelemetryGeneratedEvent(List<SensorReading> readings)
        {
            Readings = readings;
        }
    }
}

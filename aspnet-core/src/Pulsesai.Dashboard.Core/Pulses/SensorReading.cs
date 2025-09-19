using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Pulses
{
    public class SensorReading : Entity<Guid>
    {
        public string SensorId { get; set; }
        public double Value { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        protected SensorReading() { }
        public SensorReading(Guid id, string sensorId, double value, DateTimeOffset ts)
            : base()
        {
            SensorId = sensorId;
            Value = value;
            Timestamp = ts;
        }
    }
}

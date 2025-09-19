using Abp.Events.Bus;
using Abp.Threading.BackgroundWorkers;
using Pulsesai.Dashboard.Pulses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry
{
    public class TelemetryGeneratorWorker : BackgroundWorkerBase
    {
        private readonly IEventBus _eventBus;
        private readonly Random _rng = new Random();
        private CancellationTokenSource _cts;

        public TelemetryGeneratorWorker(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public override void Start()
        {
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    var now = DateTimeOffset.UtcNow;
                    var batch = new List<SensorReading>();

                    for (int i = 0; i < 10; i++)
                    {
                        batch.Add(new SensorReading(Guid.NewGuid(), "sensor-1",
                            20 + _rng.NextDouble() * 10, now));
                    }

                    _eventBus.Trigger(new TelemetryGeneratedEvent(batch));

                    await Task.Delay(10, _cts.Token);
                }
            }, _cts.Token);
        }

        public override void Stop() => _cts.Cancel();
    }

}

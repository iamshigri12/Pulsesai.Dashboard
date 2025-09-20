using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Events.Bus;
using Abp.Threading.BackgroundWorkers;
using Pulsesai.Dashboard.Pulses;
using Pulsesai.Dashboard.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry
{
    public class TelemetryGeneratorWorker : BackgroundWorkerBase, ISingletonDependency
    {
        private readonly IEventBus _eventBus;
        private readonly Random _rng = new Random();
        private readonly IRepository<SensorReading, Guid> _repo;
        private readonly IUnitOfWorkManager _uowManager;
        private readonly TelemetryBuffer _buffer;
        private CancellationTokenSource _cts;

        public TelemetryGeneratorWorker(IEventBus eventBus, IRepository<SensorReading, Guid> repo, IUnitOfWorkManager uowManager,TelemetryBuffer buffer )
        {
            _eventBus = eventBus;
            _repo = repo;
            _buffer = buffer;
            _uowManager = uowManager;
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
                    _buffer.AddRange(batch);

                    using (var uow = _uowManager.Begin())
                    {
                        await _repo.InsertRangeAsync(batch);
                        await uow.CompleteAsync();
                    }
                    _eventBus.Trigger(new TelemetryGeneratedEvent(batch));
                  
                    await Task.Delay(10, _cts.Token);
                }
            }, _cts.Token);
        }

        public override void Stop() => _cts.Cancel();
    }

}

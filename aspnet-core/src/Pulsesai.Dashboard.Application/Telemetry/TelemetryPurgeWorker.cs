using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Threading.BackgroundWorkers;
using Microsoft.Extensions.Logging;
using Pulsesai.Dashboard.Pulses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry
{

    public class TelemetryPurgeWorker : BackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<SensorReading, Guid> _repo;
        private readonly IUnitOfWorkManager _uowManager; 
        private CancellationTokenSource _cts;

        public TelemetryPurgeWorker(
            IRepository<SensorReading, Guid> repo,
            IUnitOfWorkManager uowManager 
            )
        {
            _repo = repo;
            _uowManager = uowManager;
 
        }

        public override void Start()
        {
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    while (!_cts.IsCancellationRequested)
                    {
                        var cutoff = DateTimeOffset.UtcNow.AddMinutes(-1);

                        using (var uow = _uowManager.Begin())
                        {
                             await _repo.BatchDeleteAsync(r => r.Timestamp < cutoff);
                            await uow.CompleteAsync();
                            
                        }

                        
                        await Task.Delay(TimeSpan.FromMinutes(1), _cts.Token);
                    }
                }
                catch (OperationCanceledException) {   }
                catch (Exception ex)
                {
                    
                }
            }, _cts.Token);
        }

        public override void Stop()
        {
            _cts?.Cancel();
        }
    }

}

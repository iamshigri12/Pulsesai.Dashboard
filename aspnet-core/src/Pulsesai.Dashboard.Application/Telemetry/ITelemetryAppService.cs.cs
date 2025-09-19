using Abp.Application.Services;
using Pulsesai.Dashboard.Pulses;
using Pulsesai.Dashboard.Telemetry.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry
{
    public interface ITelemetryAppService : IApplicationService
    {
        Task<SensorReadingDto[]> GetRecentAsync(int max = 1000);
        Task<AggregatedStatsDto> GetStatsAsync(int minutes = 5);
    }
     
}

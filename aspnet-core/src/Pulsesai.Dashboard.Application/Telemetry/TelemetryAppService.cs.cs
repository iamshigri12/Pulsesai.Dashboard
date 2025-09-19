using Abp.Application.Services;
using Abp.Domain.Repositories;
using Pulsesai.Dashboard.Pulses;
using Pulsesai.Dashboard.Services;
using Pulsesai.Dashboard.Telemetry.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Telemetry
{

    public class TelemetryAppService : ApplicationService, ITelemetryAppService
    {
        private readonly TelemetryBuffer _buffer;
        private readonly IRepository<SensorReading, Guid> _repo;

        public TelemetryAppService(
            TelemetryBuffer buffer,
            IRepository<SensorReading, Guid> repo)
        {
            _buffer = buffer;
            _repo = repo;
        }

        public async Task<SensorReadingDto[]> GetRecentAsync(int max = 1000)
        {
            var arr = _buffer.ToArray();
            var data = (arr.Length <= max ? arr : arr.Skip(arr.Length - max))
                .Select(x => new SensorReadingDto
                {
                    Id = x.Id,
                    SensorId = x.SensorId,
                    Value = x.Value,
                    Timestamp = x.Timestamp
                })
                .ToArray();

            return await Task.FromResult(data);
        }


        public async Task<AggregatedStatsDto> GetStatsAsync(int minutes = 5)
        {
            var cutoff = DateTimeOffset.UtcNow.AddMinutes(-minutes);
            var items = await _repo.GetAllListAsync(r => r.Timestamp >= cutoff);

            if (!items.Any())
            {
                return new AggregatedStatsDto();
            }

            var values = items.Select(i => i.Value).ToArray();
            var avg = values.Average();
            var std = Math.Sqrt(values.Select(v => Math.Pow(v - avg, 2)).Average());

            return new AggregatedStatsDto
            {
                Count = values.Length,
                Min = values.Min(),
                Max = values.Max(),
                Avg = avg,
                StdDev = std
            };
        }

      
    }

}

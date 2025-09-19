using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Microsoft.AspNetCore.SignalR;
using Pulsesai.Dashboard.Telemetry;
using System.Threading.Tasks;

namespace Pulsesai.Dashboard.Web.Host.Hub
{
    public class TelemetryGeneratedEventHandler :
    IEventHandler<TelemetryGeneratedEvent>, ITransientDependency
    {
        private readonly IHubContext<TelemetryHub> _hub;

        public TelemetryGeneratedEventHandler(IHubContext<TelemetryHub> hub)
        {
            _hub = hub;
        }

        public void HandleEvent(TelemetryGeneratedEvent eventData)
        {
            _hub.Clients.All.SendAsync("ReceiveReadings", eventData.Readings);
        }

        
    }

}

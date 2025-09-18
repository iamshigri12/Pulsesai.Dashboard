using System.Collections.Generic;

namespace Pulsesai.Dashboard.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}

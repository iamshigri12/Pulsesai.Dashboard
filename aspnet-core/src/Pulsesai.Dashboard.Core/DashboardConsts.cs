using Pulsesai.Dashboard.Debugging;

namespace Pulsesai.Dashboard;

public class DashboardConsts
{
    public const string LocalizationSourceName = "Dashboard";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "f03bba43a6274992b78e6b39f4fcefe9";
}

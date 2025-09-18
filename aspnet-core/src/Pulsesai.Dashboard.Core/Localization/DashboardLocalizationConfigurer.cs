using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Pulsesai.Dashboard.Localization;

public static class DashboardLocalizationConfigurer
{
    public static void Configure(ILocalizationConfiguration localizationConfiguration)
    {
        localizationConfiguration.Sources.Add(
            new DictionaryBasedLocalizationSource(DashboardConsts.LocalizationSourceName,
                new XmlEmbeddedFileLocalizationDictionaryProvider(
                    typeof(DashboardLocalizationConfigurer).GetAssembly(),
                    "Pulsesai.Dashboard.Localization.SourceFiles"
                )
            )
        );
    }
}

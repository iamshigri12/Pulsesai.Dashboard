﻿using Abp.Dependency;
using System;

namespace Pulsesai.Dashboard.Timing;

public class AppTimes : ISingletonDependency
{
    /// <summary>
    /// Gets the startup time of the application.
    /// </summary>
    public DateTime StartupTime { get; set; }
}

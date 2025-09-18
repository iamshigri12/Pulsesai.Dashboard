﻿using Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace Pulsesai.Dashboard.Authorization.Accounts.Dto;

public class IsTenantAvailableInput
{
    [Required]
    [StringLength(AbpTenantBase.MaxTenancyNameLength)]
    public string TenancyName { get; set; }
}

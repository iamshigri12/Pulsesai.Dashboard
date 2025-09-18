using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace Pulsesai.Dashboard.Roles.Dto;

public class PagedRoleResultRequestDto : PagedResultRequestDto, IShouldNormalize
{
    public string Keyword { get; set; }
    public string Sorting { get; set; }

    public void Normalize()
    {
        if (string.IsNullOrEmpty(Sorting))
        {
            Sorting = "Name,DisplayName";
        }

        Keyword = Keyword?.Trim();
    }
}


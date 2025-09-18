using System.ComponentModel.DataAnnotations;

namespace Pulsesai.Dashboard.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}
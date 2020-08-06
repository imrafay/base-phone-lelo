using System.ComponentModel.DataAnnotations;

namespace PhoneLelo.Project.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
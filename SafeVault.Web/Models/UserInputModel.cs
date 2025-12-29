using System.ComponentModel.DataAnnotations;

namespace SafeVault.Web.Models
{
    public class UserInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username {get; set;} = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email {get; set;} = string.Empty;

    }
}
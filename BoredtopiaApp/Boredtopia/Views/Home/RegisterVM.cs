using System.ComponentModel.DataAnnotations;

namespace Boredtopia.Views.Home;

public class RegisterVM
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Repeat password")]
    [Compare(nameof(Password))]
    public string PasswordRepeat { get; set; }
}
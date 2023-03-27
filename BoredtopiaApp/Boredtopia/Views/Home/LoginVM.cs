using System.ComponentModel.DataAnnotations;

namespace Boredtopia.Views.Home;

public class LoginVM
{
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
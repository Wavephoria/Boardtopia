using System.ComponentModel.DataAnnotations;

namespace Boredtopia.Views.Home;

public class ChangeVM
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
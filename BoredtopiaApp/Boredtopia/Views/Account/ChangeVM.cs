using System.ComponentModel.DataAnnotations;

namespace Boredtopia.Views.Home;

public class ChangeVM
{

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public string Name { get; set; }

    [DataType(DataType.Password)]
    public string OldPassword { get; set; }
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }


}
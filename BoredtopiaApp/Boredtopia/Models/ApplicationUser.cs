using Microsoft.AspNetCore.Identity;

namespace Boredtopia.Controllers;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

}
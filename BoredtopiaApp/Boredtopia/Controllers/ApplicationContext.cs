using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boredtopia.Controllers;

public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

    private DbSet<User> Users { get; set; }
}
using Boredtopia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boredtopia.Controllers;

public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

    public DbSet<RockPaperScissor> _rpsStats { get; set; }
    public DbSet<Wordle> _wordleStats { get; set; }
    public DbSet<TicTacToe> _tttStats { get; set; }

}

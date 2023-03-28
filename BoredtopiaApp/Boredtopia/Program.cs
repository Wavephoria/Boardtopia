using Azure.Core;
using Boredtopia.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString;
if (OperatingSystem.IsMacOS())
{
    connectionString = builder.Configuration.GetConnectionString("MyDbConnectionMac");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("MyDbConnection");
}

builder.Services.AddDbContext<ApplicationContext>(a => a.UseSqlServer(connectionString));

builder.Services.AddTransient<AccountServices>();
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(a => a.LoginPath = "/login");

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

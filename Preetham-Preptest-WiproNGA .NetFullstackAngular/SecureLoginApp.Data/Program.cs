using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuthApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("RoleBasedAuthDb"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.InitializeAsync(services);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

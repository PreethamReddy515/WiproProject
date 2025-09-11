using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureLoginMVC.Data;
using SecureLoginMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity + password policy
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true; // special char
    options.Password.RequireDigit = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddDataProtection();

// Require HTTPS for controllers (recommended)
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new RequireHttpsAttribute());
});

// Seed
builder.Services.AddScoped<DbSeeder>();

// Configure AccessDenied path so unauthorized role access redirects there
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Seed roles/users
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
}

app.Run();

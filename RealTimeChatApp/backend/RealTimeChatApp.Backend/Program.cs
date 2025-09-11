using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using RealTimeChatApp.Backend.Data;
using RealTimeChatApp.Backend.Models;
using System.Text;
using YourNamespace.Hubs;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// ===== DB Context =====
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ===== Identity =====
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ===== JWT Authentication =====
var jwt = builder.Configuration.GetSection("Jwt");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = key
    };

    // Enable JWT for SignalR
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/chat"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

// ===== SignalR =====
builder.Services.AddSignalR();

// ===== Controllers =====
builder.Services.AddControllers();

// ===== Swagger =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== CORS =====
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("client", policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:5173")
    );
});

var app = builder.Build();

// ===== Middleware =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Serve wwwroot by default
app.UseStaticFiles();

// Serve /uploads explicitly
var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "uploads");
if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});


// app.UseHttpsRedirection();
app.UseCors("client");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");

app.Run();

// ===== SignalR Hub =====
//public class ChatHub : Hub
//{
//    // Send message to a specific user
//    public async Task SendPrivate(string receiverUserId, string message)
//        => await Clients.User(receiverUserId).SendAsync("ReceiveMessage",
//            Context.UserIdentifier, message, DateTime.UtcNow);

//    // Send message to a group
//    public async Task SendGroup(string groupName, string message)
//        => await Clients.Group(groupName).SendAsync("ReceiveGroupMessage",
//            Context.UserIdentifier, message, DateTime.UtcNow);

//    // Typing indicator
//    public async Task Typing(string toUserId)
//        => await Clients.User(toUserId).SendAsync("Typing", Context.UserIdentifier);

//    // Optional: Join a group
//    public async Task JoinGroup(string groupName)
//        => await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

//    // Optional: Leave a group
//    public async Task LeaveGroup(string groupName)
//        => await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
//}

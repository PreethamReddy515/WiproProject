using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Backend.Models;

namespace RealTimeChatApp.Backend.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<GroupMember> GroupMembers => Set<GroupMember>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
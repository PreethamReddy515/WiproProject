using Microsoft.EntityFrameworkCore;
namespace EmployCrud.Models
{
    public class EFCoreDbContext: DbContext
    {
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        {

        }
        //OnConfiguring() method is used to select and configure the data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPTOP-O5RU4NNJ;Database=wiprojuly;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employ>().ToTable("Employ");
        }

        public DbSet<Employ> Employees { get; set; }
    }
}

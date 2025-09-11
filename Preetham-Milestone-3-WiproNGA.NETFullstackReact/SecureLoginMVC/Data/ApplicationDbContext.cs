using System.Globalization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecureLoginMVC.Models;

namespace SecureLoginMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IDataProtector _protector;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDataProtectionProvider dataProtectionProvider) : base(options)
        {
            _protector = dataProtectionProvider.CreateProtector("SecureLoginMVC.ProductPriceProtector.v1");
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var priceConverter = new ValueConverter<decimal, string>(
                v => _protector.Protect(v.ToString(CultureInfo.InvariantCulture)),
                v => decimal.Parse(_protector.Unprotect(v), CultureInfo.InvariantCulture)
            );

            builder.Entity<Product>()
                   .Property(p => p.Price)
                   .HasConversion(priceConverter)
                   .HasMaxLength(256);
        }
    }
}

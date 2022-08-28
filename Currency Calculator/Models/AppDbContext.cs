using Microsoft.EntityFrameworkCore;

namespace Currency_Calculator.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<Currency> Currency { get; set; } = null!;
        public DbSet<CurrencyConversion> CurrencyConversion { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => new
            {
                u.Username
            });
            modelBuilder.Entity<Currency>().HasKey(c => new
            {
                c.CurrencyCode
            });
            modelBuilder.Entity<CurrencyConversion>().HasKey(c => new
            {
                c.BaseCurrencyCode,
                c.TargetCurrencyCode
            });
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(c => c.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //precomuputed the passwordHash given the following passwordHash and password = "admin"
            byte[] passwordHash = Convert.FromHexString("FA8A83010EF182D1E605D400E49DE263998AB49EC60E8AC78B8E8A0A36282F0ECD7110D251A44FDEA17E257B8C9493E2C1B29B111E5C0BD48FBE4BE9B700DC6F");
            byte[] passwordSalt = Convert.FromHexString("D8B2D5A1C3F009659AFF134FCEF9F3AA71833912CF5FC23D9E94330A792639DE8CEDEE78789A27E9F7939B2F7D5AE7893F5A2B615C6C3852C87CA37BE5E8C5A79CC9B9A834541B67FC0CE0E5F8C5C7823D83D8FA3CCD7C81446041F53C52A978DCBEBD0A93F901ABFD005B3A1B3B2E58877C629C197B59015A7B7B29BE0BF389");
            modelBuilder.Entity<User>().HasData(new User { Username = "admin", PasswordHash = passwordHash, PasswordSalt = passwordSalt });

            Currency eurCurrency = new Currency { CurrencyCode = "EUR" };
            Currency usdCurrency = new Currency { CurrencyCode = "USD" };
            Currency chfCurrency = new Currency { CurrencyCode = "CHF" };
            Currency gbpCurrency = new Currency { CurrencyCode = "GBP" };
            Currency jpyCurrency = new Currency { CurrencyCode = "JPY" };
            Currency cadCurrency = new Currency { CurrencyCode = "CAD" };
            modelBuilder.Entity<Currency>().HasData(eurCurrency);
            modelBuilder.Entity<Currency>().HasData(usdCurrency);
            modelBuilder.Entity<Currency>().HasData(chfCurrency);
            modelBuilder.Entity<Currency>().HasData(gbpCurrency);
            modelBuilder.Entity<Currency>().HasData(jpyCurrency);
            modelBuilder.Entity<Currency>().HasData(cadCurrency);

            modelBuilder.Entity<CurrencyConversion>().HasData(new CurrencyConversion { BaseCurrencyCode = "EUR", TargetCurrencyCode = "USD", ConversionRate = 1.3764m });
            modelBuilder.Entity<CurrencyConversion>().HasData(new CurrencyConversion { BaseCurrencyCode = "EUR", TargetCurrencyCode = "CHF", ConversionRate = 1.2079m });
            modelBuilder.Entity<CurrencyConversion>().HasData(new CurrencyConversion { BaseCurrencyCode = "EUR", TargetCurrencyCode = "GBP", ConversionRate = 0.8731m });
            modelBuilder.Entity<CurrencyConversion>().HasData(new CurrencyConversion { BaseCurrencyCode = "USD", TargetCurrencyCode = "JPY", ConversionRate = 76.7200m });
            modelBuilder.Entity<CurrencyConversion>().HasData(new CurrencyConversion { BaseCurrencyCode = "CHF", TargetCurrencyCode = "USD", ConversionRate = 1.1379m });
            modelBuilder.Entity<CurrencyConversion>().HasData(new CurrencyConversion { BaseCurrencyCode = "GBP", TargetCurrencyCode = "CAD", ConversionRate = 1.5648m });
        }
    }
}

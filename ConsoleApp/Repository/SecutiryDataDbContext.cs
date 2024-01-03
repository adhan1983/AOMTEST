using AOMTEST.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AOMTEST.Repository
{
    public class SecutiryDataDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public SecutiryDataDbContext(DbContextOptions<SecutiryDataDbContext> options) : base(options) { }        
        public DbSet<PriceData> PriceData { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("strConnection");
            
            builder.UseSqlServer(connectionString);

        }


    }
}

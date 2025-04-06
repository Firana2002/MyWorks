using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Course>
     Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
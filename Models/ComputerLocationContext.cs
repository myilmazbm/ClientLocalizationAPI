using Microsoft.EntityFrameworkCore;

namespace ClientLocalizationAPI.Models
{
    public class ComputerLocationContext : DbContext
    {
        public DbSet<ComputerLocation> ComputerLocations { get; set; }
        public ComputerLocationContext(DbContextOptions<ComputerLocationContext> options) : base(options)
        {

        }

        public DbSet<ComputerLocation> ComputerLocation { get; set; }
    }

}

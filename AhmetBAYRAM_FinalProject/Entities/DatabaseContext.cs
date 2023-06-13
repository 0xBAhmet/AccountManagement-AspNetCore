using Microsoft.EntityFrameworkCore;



namespace AhmetBAYRAM_FinalProject.Entities
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}

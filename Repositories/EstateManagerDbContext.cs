namespace EstateManager.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using EstateManager.Entities;

    public class EstateManagerDbContext : DbContext
    {
        public EstateManagerDbContext(DbContextOptions<EstateManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Estate> Estates { get; set; } = null!;
    }
}
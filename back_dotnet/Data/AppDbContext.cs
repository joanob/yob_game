using Microsoft.EntityFrameworkCore;

namespace Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserDTO> Users { get; set; } = null!;
    public DbSet<StorageDTO> Storage { get; set; } = null!;
    public DbSet<ProductionDTO> Production { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDTO>().HasIndex(u => u.Username).IsUnique();

        modelBuilder.Entity<StorageDTO>().HasKey(s => new { s.UserId, s.ResourceId });
        modelBuilder.Entity<UserDTO>().HasMany<StorageDTO>().WithOne().HasForeignKey(s => s.UserId);

        modelBuilder.Entity<ProductionDTO>().HasKey(p => new { p.UserId, p.ProductionBuildingId });
        modelBuilder.Entity<UserDTO>().HasMany<ProductionDTO>().WithOne().HasForeignKey(p => p.UserId);
    }
}
using mf_dev_back_end_2026_e2_t1_g5.Models;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_back_end_2026_e2_t1_g5.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Consumo> Consumos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Veiculo>()
            .HasIndex(v => v.PublicId)
            .IsUnique();
    }
}
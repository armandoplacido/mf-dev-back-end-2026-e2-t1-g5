using mf_dev_back_end_2026_e2_t1_g5.Models;
using Microsoft.EntityFrameworkCore;

namespace mf_dev_back_end_2026_e2_t1_g5.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos { get; set; }
}
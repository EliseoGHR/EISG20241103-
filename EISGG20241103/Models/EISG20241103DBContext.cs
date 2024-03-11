using Microsoft.EntityFrameworkCore;

namespace EISGG20241103.Models
{
    public class EISG20241103DBContext : DbContext
    {
       
            public EISG20241103DBContext(DbContextOptions<EISG20241103DBContext> options) : base(options)
            {
            }


        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DetalleCliente> DetalleClientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(v => v.DetalleClientes)
                .WithOne(d => d.Cliente)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}

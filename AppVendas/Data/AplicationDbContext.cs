using AppVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVendas.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ItemDaVenda> itensDaVenda { get; set; }
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Venda> vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            modelBuilder.Entity<ItemDaVenda>().ToTable("itensDaVenda");
            modelBuilder.Entity<Produto>().ToTable("produtos");
            modelBuilder.Entity<Venda>().ToTable("vendas");
        }
    }

}

using FinCap.Data.Extensions;
using FinCap.Data.Mappings;
using FinCap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinCap.Data.Context
{
    public class FinCapDbContext : DbContext
    {
        public FinCapDbContext(DbContextOptions<FinCapDbContext> options) : base(options) { }

        #region DbSets
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Entidade> Entidade { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            modelBuilder.ApplyGlobalConfiguration();
        }
    }
}
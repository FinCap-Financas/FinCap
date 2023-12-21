using FinCap.Domain.Entities;
using FinCap.Constants.MaxLengths;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinCap.Data.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Uid);

            builder.Property(c => c.Descricao)
                .HasMaxLength(CategoriaMaxLengths.Descricao)
                .IsRequired();

            builder.Property(c => c.Publica)
                .IsRequired();

            builder.HasMany(c => c.Transacoes)
                .WithOne(t => t.Categoria)
                .HasForeignKey(t => t.UidCategoria)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
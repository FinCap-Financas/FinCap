using FinCap.Constants.MaxLengths;
using FinCap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Data.Mappings
{
    public class CategoriaMap: IEntityTypeConfiguration<Categoria>
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

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Categorias)
                .HasForeignKey(u => u.Uid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

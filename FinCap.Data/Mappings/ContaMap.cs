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
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(u => u.Uid);

            builder.Property(c => c.Descricao)
                .HasMaxLength(ContaMaxLength.Descricao)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.SaldoInicial)
                .IsRequired();

            builder.HasMany(c => c.Transacoes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.Uid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Contas)
                .HasForeignKey(u => u.Uid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

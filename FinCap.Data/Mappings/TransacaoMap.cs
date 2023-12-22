using FinCap.Constants.MaxLengths;
using FinCap.Domain.Entities;
using Fundr.Constants.MaxLengths;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCap.Data.Mappings
{
    public class TransacaoMap : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(t => t.Uid);

            builder.Property(t => t.Descricao)
                .HasMaxLength(TransacaoMaxLengths.Descricao)
                .IsRequired();

            builder.Property(t => t.DataVencimento)
                .IsRequired();

            builder.Property(t => t.DataPagamento)
                .IsRequired();

            builder.HasOne(t => t.Conta)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(c => c.UidConta)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Conta)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(c => c.UidCategoria)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

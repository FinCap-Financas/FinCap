using FinCap.Domain.Entities;
using FinCap.Constants.MaxLengths;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(t => t.Valor)
                .IsRequired();

            builder.Property(t => t.Modo)
                .IsRequired();

            builder.Property(t => t.DataVencimento)
                .IsRequired();

           

            
        }

    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinCap.Domain.Entities;
using FinCap.Constants.MaxLengths;
using Microsoft.EntityFrameworkCore;


namespace FinCap.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure (EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.Uid);

            builder.Property(c => c.Descricao)
             .HasMaxLength(UsuarioMaxLengths.Nome)
             .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.SaldoInicial)
                .HasDefaultValue(0.00) 
                .IsRequired();

            builder.HasMany(c => c.Transacoes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.UidConta)
                .OnDelete(DeleteBehavior.Restrict);                
        }
    }
}
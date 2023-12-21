using FinCap.Domain.Entities;
using FinCap.Constants.MaxLengths;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinCap.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Uid);

            builder.Property(u => u.Nome)
                .HasMaxLength(UsuarioMaxLengths.Nome)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(UsuarioMaxLengths.Email)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Senha)
                .HasMaxLength(UsuarioMaxLengths.SenhaCriptografada)
                .IsRequired();
            
            builder.Property(u => u.FotoPerfil)
                .HasMaxLength(UsuarioMaxLengths.FotoPerfil)
                .IsRequired();
            
            builder.Property(u => u.DataNascimento)
                .IsRequired();

            builder.HasMany(u => u.Categorias)
                .WithOne(c => c.Usuario)
                .HasForeignKey(c => c.UidUsuario)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(u => u.Contas)
                .WithOne(c => c.Usuario)
                .HasForeignKey(c => c.UidUsuario)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

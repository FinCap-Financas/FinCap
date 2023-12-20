using FinCap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinCap.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder ApplyGlobalConfiguration(this ModelBuilder builder) 
        {
            foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    switch (property.Name)
                    {
                        case nameof(Entidade.DataCadastro):
                            property.ValueGenerated = ValueGenerated.OnAdd;
                            property.SetDefaultValueSql("getdate()");
                            property.SetPrecision(3);
                            break;
                        case nameof(Entidade.DataAtualizacao):
                            property.ValueGenerated = ValueGenerated.OnUpdate;
                            property.SetDefaultValue(DateTime.MinValue);
                            property.SetPrecision(3);
                            break;
                        case nameof(Entidade.Deletado):
                            property.SetDefaultValue(false);
                            break;
                        default:
                            break;
                    }
                }
            }

            return builder;
        }
    }
}

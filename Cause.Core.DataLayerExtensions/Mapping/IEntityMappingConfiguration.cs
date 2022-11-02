using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cause.Core.DataLayerExtensions.Mapping
{
    public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }

    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder builder);
    }
}
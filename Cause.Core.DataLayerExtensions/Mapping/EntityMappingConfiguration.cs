﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cause.Core.DataLayerExtensions.Mapping
{
    public abstract class EntityMappingConfiguration<T> : IEntityMappingConfiguration<T> where T : class
    {
        public abstract void Map(EntityTypeBuilder<T> b);

        public virtual void Map(ModelBuilder b)
        {
            Map(b.Entity<T>());
        }
    }
}

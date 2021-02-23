using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cause.Core.DataLayerExtensions.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cause.Core.DataLayerExtensions
{
    public static class ModelBuilderExtensions
    {
        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            return assembly.GetTypes()
                .Where(x => !x.IsAbstract && x.GetInterfaces()
                    .Any(y => IntrospectionExtensions.GetTypeInfo(y).IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
        }

        public static PropertyBuilder<decimal?> HasPrecision(this PropertyBuilder<decimal?> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }

        public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }

        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>));
            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>())
            {
                config.Map(modelBuilder);
            }
        }

        public static void AddTableNameToPrimaryKey(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var tableIdentifier = StoreObjectIdentifier.Create(entity, StoreObjectType.Table);
                foreach (var key in entity.GetProperties().Where(column => column.IsPrimaryKey()))
                {
                    key.SetColumnName(key.GetColumnName(tableIdentifier.Value) + entity.DisplayName());
                }
            }
        }

        public static void UseTablePrefix(this ModelBuilder builder, string prefix)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(prefix + entity.GetTableName());
            }
        }

        public static void UseAutoSnakeCaseMapping(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName().ToSnakeCase());

                var tableIdentifier = StoreObjectIdentifier.Create(entity, StoreObjectType.Table);

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName(tableIdentifier.Value).ToSnakeCase());
                    
                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName().ToSnakeCase());

                foreach (var key in entity.GetForeignKeys())
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());

                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
            }
        }
    }
}
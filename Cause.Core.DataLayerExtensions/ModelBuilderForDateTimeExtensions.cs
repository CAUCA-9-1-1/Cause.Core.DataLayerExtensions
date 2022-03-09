using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cause.Core.DataLayerExtensions
{

    public static class ExperimentalModelBuilderForDateTimeExtensions
    {
        private static string timeStampWithoutTimeZone = "timestamp without time zone";
        private static DateTimeUtcConverter dateTimeConverter = new DateTimeUtcConverter();
        private static NullableDateTimeUtcConverter nullableDateTimeConverter = new NullableDateTimeUtcConverter();


        public static PropertyBuilder<DateTime> HasTimestampWithoutTimeZoneType(this PropertyBuilder<DateTime> builder)
        {
            return builder.HasColumnType(timeStampWithoutTimeZone);
        }

        public static PropertyBuilder<DateTime?> HasTimestampWithoutTimeZoneType(this PropertyBuilder<DateTime?> builder)
        {
            return builder.HasColumnType(timeStampWithoutTimeZone);
        }

        public static ModelBuilder AutoConvertAllDateTimeToUtc(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties().Where(property => IsDateTimeColumn(property)))
                {
                    if (IsDateTimeColumn(property))
                    {
                        property.SetValueConverter(dateTimeConverter);
                                            }
                    else if (IsNullableDateTimeColumn(property))
                        property.SetValueConverter(nullableDateTimeConverter);
                }
            }
            return builder;
        }

        private static bool IsDateTimeColumn(IMutableProperty property)
        {
            return property.GetColumnType() != timeStampWithoutTimeZone && property.ClrType == typeof(DateTime) && !property.IsNullable;
        }

        private static bool IsNullableDateTimeColumn(IMutableProperty property)
        {
            return property.GetColumnType() != timeStampWithoutTimeZone && property.ClrType == typeof(DateTime) && property.IsNullable;
        }
    }
}
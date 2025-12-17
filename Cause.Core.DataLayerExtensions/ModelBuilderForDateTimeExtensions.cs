using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cause.Core.DataLayerExtensions;

public static class ExperimentalModelBuilderForDateTimeExtensions
{
    private const string TimeStampWithoutTimeZone = "timestamp without time zone";
    private static readonly DateTimeUtcConverter DateTimeConverter = new();
    private static readonly NullableDateTimeUtcConverter NullableDateTimeConverter = new();


    public static PropertyBuilder<DateTime> HasTimestampWithoutTimeZoneType(this PropertyBuilder<DateTime> builder)
    {
        return builder.HasColumnType(TimeStampWithoutTimeZone);
    }

    public static PropertyBuilder<DateTime?> HasTimestampWithoutTimeZoneType(this PropertyBuilder<DateTime?> builder)
    {
        return builder.HasColumnType(TimeStampWithoutTimeZone);
    }

    public static ModelBuilder AutoConvertAllDateTimeToUtc(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(IsDateTimeColumn))
            {
                if (IsDateTimeColumn(property))
                {
                    property.SetValueConverter(DateTimeConverter);
                }
                else if (IsNullableDateTimeColumn(property))
                    property.SetValueConverter(NullableDateTimeConverter);
            }
        }
        return builder;
    }

    private static bool IsDateTimeColumn(IMutableProperty property)
    {
        return property.GetColumnType() != TimeStampWithoutTimeZone && property.ClrType == typeof(DateTime) && !property.IsNullable;
    }

    private static bool IsNullableDateTimeColumn(IMutableProperty property)
    {
        return property.GetColumnType() != TimeStampWithoutTimeZone && property.ClrType == typeof(DateTime) && property.IsNullable;
    }
}
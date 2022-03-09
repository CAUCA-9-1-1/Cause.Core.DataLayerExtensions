using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cause.Core.DataLayerExtensions
{
    public class NullableDateTimeUtcConverter : ValueConverter<DateTime?, DateTime?>
    {
        public NullableDateTimeUtcConverter()
            : base(date => ToNullableUtc(date), date => ToNullableLocal(date))
        {
        }

        private static DateTime ToUtc(DateTime date)
        {
            var result = date.ToUniversalTime();
            return result;
        }

        private static DateTime ToLocal(DateTime date)
        {
            var result = date;
            return result;
        }

        private static DateTime? ToNullableLocal(DateTime? date)
        {
            if (date.HasValue)
                return ToLocal(date.Value);
            return date;
        }

        private static DateTime? ToNullableUtc(DateTime? date)
        {
            if (date.HasValue)
            {
                return ToUtc(date.Value);
            }
            return date;
        }
    }
}
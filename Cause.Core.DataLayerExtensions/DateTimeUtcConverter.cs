using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cause.Core.DataLayerExtensions
{
    public class DateTimeUtcConverter : ValueConverter<DateTime, DateTime>
    {        
        public DateTimeUtcConverter() 
            : base(date => ToUtc(date), date => ToLocal(date))
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
    }
}
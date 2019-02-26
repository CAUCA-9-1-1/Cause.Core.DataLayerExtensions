using Microsoft.EntityFrameworkCore;


namespace Cause.Core.DataLayerExtensions
{
    public static class DbContextExtensions
    {
        public static void UseAutoDetectedMappings(this DbContext context, ModelBuilder builder)
        {
            builder.AddEntityConfigurationsFromAssembly(context.GetType().Assembly);
        }
    }
}

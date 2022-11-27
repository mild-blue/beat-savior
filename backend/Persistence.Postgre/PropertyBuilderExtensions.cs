using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace BildMlue.Infrastructure.Persistence.Postgre;

public static class PropertyBuilderExtensions
{
    /// <summary>
    /// Add JSON conversion to a property builder.
    /// </summary>
    /// <param name="propertyBuilder"></param>
    /// <typeparam name="TType">Nested object type.</typeparam>
    /// <returns></returns>
    public static PropertyBuilder<TType> AsJson<TType>(this PropertyBuilder<TType> propertyBuilder) =>
        propertyBuilder
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<TType>(x)!)
            .HasColumnType("jsonb");
}
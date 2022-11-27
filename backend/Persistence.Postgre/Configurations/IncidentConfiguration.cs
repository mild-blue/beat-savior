using BildMlue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BildMlue.Infrastructure.Persistence.Postgre.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.Navigation(x => x.NearestAED).AutoInclude();
        builder.Property(x => x.FirstResponders).AsJson();
        builder.Property(x => x.DefibrillatorCarrier).AsJson();
    }
}
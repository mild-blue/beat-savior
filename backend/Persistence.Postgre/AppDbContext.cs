using BildMlue.Application.Interfaces;
using BildMlue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BildMlue.Infrastructure.Persistence.Postgre;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public required DbSet<TodoItem> TodoItems { get; init; }

    public required DbSet<AutomatedExternalDefibrillator> Aeds { get; init; }
    public required DbSet<Incident> Incidents { get; init; }

    public Task SaveChanges(CancellationToken cancellationToken = default) =>
        SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
using System.Reflection;
using System.Text.Json.Serialization;
using BildMlue.API.Middlewares;
using BildMlue.API.Workers;
using BildMlue.Application.Interfaces;
using BildMlue.Application.Services;
using BildMlue.Domain.Entities;
using BildMlue.Domain.Events;
using BildMlue.Infrastructure.AED;
using BildMlue.Infrastructure.FHIR;
using BildMlue.Infrastructure.FHIR.Extensions;
using BildMlue.Infrastructure.Firebase;
using BildMlue.Infrastructure.Mapping;
using BildMlue.Infrastructure.Persistence.Postgre;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Dependency injection
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddMapping();
builder.Services.AddFhir(builder.Configuration.GetSection("Fhir"));
builder.Services.AddFirebase();

// services
builder.Services.AddScoped(typeof(CrudService<,,,,>));
builder.Services.AddScoped<IAedService, AedService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

// BG workers
builder.Services.AddHostedService<IncidentSynchronizer>();

// Endpoints + validation
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<IAppDbContext>();
builder.Services.AddFluentValidationClientsideAdapters();

// Swagger setup
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseDateOnlyTimeOnlyStringConverters();
    c.DescribeAllParametersInCamelCase();
    c.SupportNonNullableReferenceTypes();
    c.SchemaFilter<RequiredNotNullableSchemaFilter>();

    // custom models
    c.DocumentFilter<CustomModelDocumentFilter<CardiacArrestIncident>>();

    // Generate swagger comments from summary
    foreach (var assembly in new[] {typeof(AppEntity), typeof(IAppDbContext)}
                 .Select(Assembly.GetAssembly)
                 .Append(Assembly.GetExecutingAssembly()))
    {
        var xmlFile = $"{assembly!.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddFluentValidationRulesToSwagger();

// lower case routes
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// setup database
{
    // apply migrations to DB
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var importer = services.GetRequiredService<IAedImporter>();

    if (!builder.Environment.IsDevelopment())
    {
        await context.Database.EnsureDeletedAsync();
    }

    await context.Database.EnsureCreatedAsync();

    await using var json = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AED", "aedmap.json"));
    await importer.Import(json);
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
using BildMlue.Application.Interfaces;
using BildMlue.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BildMlue.Infrastructure.AED;

public class AedImporter : IAedImporter
{
    private readonly IAppDbContext _context;

    public AedImporter(IAppDbContext context) =>
        _context = context;

    public async Task Import(Stream json)
    {
        if (await _context.Set<AutomatedExternalDefibrillator>().AnyAsync())
        {
            return;
        }

        var reader = new JsonTextReader(new StreamReader(json));
        var jsonArray = await JArray.LoadAsync(reader);

        foreach (var item in jsonArray)
        {
            var mobile = item["mobile"]?.ToString() ?? "";

            var aed = new AutomatedExternalDefibrillator
            {
                Name = item["nazev"]?.ToString() ?? "",
                Latitude = item["lat"]?.Value<double?>() ?? 0,
                Longitude = item["lng"]?.Value<double?>() ?? 0,
                HtmlDescription = item["html"]?.ToString() ?? "",
                Address = item["address"]?.ToString() ?? "",
                ImageUrl = item["image"]?.ToString() ?? "",
                IsMobile = mobile.Equals("ano", StringComparison.OrdinalIgnoreCase)
            };

            _context.Set<AutomatedExternalDefibrillator>().Add(aed);
        }

        await _context.SaveChanges();
    }
}
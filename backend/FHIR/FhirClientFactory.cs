using Hl7.Fhir.Rest;
using Microsoft.Extensions.Options;

namespace BildMlue.Infrastructure.FHIR;

public class FhirClientFactory
{
    public FhirClientFactory(IOptions<FhirOptions> options)
    {
        var key = options.Value.ApiKey;
        var url = options.Value.Url;
        var client = new FhirClient(url, messageHandler: new ApiKeyMessageHandler(key));
        client.Settings.PreferredFormat = ResourceFormat.Json;
        client.Settings.PreferredReturn = Prefer.ReturnRepresentation;
        Client = client;
    }

    public FhirClient Client { get; }
}
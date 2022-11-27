using FluentAssertions;
using Newtonsoft.Json.Linq;
using Pathoschild.Http.Client;
using Xunit;
using Xunit.Abstractions;

namespace API.Tests;

public class SwaggerTest : IntegrationTestBase, IClassFixture<DatabaseFixture>
{
    public SwaggerTest(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task StubsShouldBeUpToDate()
    {
        // Arrange
        var expected = JObject.Parse(await File.ReadAllTextAsync("../../../../../frontend/swagger.json"));
        RemoveDescriptions(expected);

        // Act
        var actual = await Client.GetAsync("/swagger/v1/swagger.json").AsRawJsonObject();
        RemoveDescriptions(actual);

        // Assert
        expected.Should().BeEquivalentTo(actual,
            because: "Swagger should be up to date. If not, call make generate-stubs.");
    }

    private static void RemoveDescriptions(JObject openApiSpecification)
    {
        var descriptions = openApiSpecification
            .SelectTokens("$.components.schemas.*.properties.*.description")
            .ToList();

        foreach (var description in descriptions)
        {
            var property = description.Parent.Parent as JObject;
            property.Remove("description");
        }
    }
}
﻿namespace BildMlue.Application.DTO.Aed;

public record AedOutDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required string HtmlDescription { get; init; }
    public required string Address { get; init; }
    public required string? ImageUrl { get; init; }
    public required bool IsMobile { get; init; }
}
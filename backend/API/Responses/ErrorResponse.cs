namespace BildMlue.API.Responses;

public record ErrorResponse(string Message, string? DisplayMessage, string? RequestId);
namespace BildMlue.API.Responses;

public record ValidationErrorResponse
(
    string RequestId,
    IReadOnlyDictionary<string, IReadOnlyList<string>> Errors
);
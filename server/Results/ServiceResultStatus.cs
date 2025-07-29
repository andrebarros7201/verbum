namespace Verbum.API.Results;

public enum ServiceResultStatus {
    Success,
    Error,
    Unauthorized,
    Forbidden,
    NotFound,
    Conflict,
    ValidationError
}
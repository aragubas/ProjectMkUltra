namespace MkApi.Application.Response;

public abstract class ResponseBase
{
    public bool Success { get; }
    public string? Message { get; }

    public ResponseBase(bool success, string? message = null)
    {
        Success = success;
        Message = message;
    }
}
namespace MkApi.Application.Response;

public class LoginResultResponse
{
    public bool Success { get; }
    public string Message { get; }


    public LoginResultResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
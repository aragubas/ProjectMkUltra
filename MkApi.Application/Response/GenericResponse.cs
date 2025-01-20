namespace MkApi.Application.Response;

public class GenericResponse : ResponseBase
{
    public GenericResponse(bool success, string? message) : base(success, message) { }
}
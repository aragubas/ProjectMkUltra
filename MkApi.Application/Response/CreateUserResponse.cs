using MkApi.Application.DTO;

namespace MkApi.Application.Response;

public class CreateUserResponse : ResponseBase
{
    public UserDTO? User { get; }
    
    public CreateUserResponse(bool success, UserDTO? user, string? message = null) : base(success, message)
    {
        User = user;
    }
}
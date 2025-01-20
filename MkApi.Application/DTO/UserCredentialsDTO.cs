namespace MkApi.Application.DTO;

public class UserCredentialsDTO
{
    public required string Username { get; set; }
    public required string PlainTextPassword { get; set; }
}
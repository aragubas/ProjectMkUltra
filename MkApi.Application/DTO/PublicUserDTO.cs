namespace MkApi.Application.DTO;

public class PublicUserDTO
{
    public Guid Id { get; }
    public string Username { get; }

    public PublicUserDTO(Guid id, string username)
    {
        Id = id;
        Username = username;
    }
}
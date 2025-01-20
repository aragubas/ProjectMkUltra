namespace MkApi.Application.DTO;

public class UserDTO
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required List<string> FavoriteCatFacts { get; set; }
}
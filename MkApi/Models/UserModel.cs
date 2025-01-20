namespace MkApi.Models;

public class UserModel
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required string HashedPassword { get; set; }
    public required List<string> FavoriteCatFacts { get; set; }
}
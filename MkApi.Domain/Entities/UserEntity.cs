using Microsoft.AspNetCore.Identity;
using MkApi.Domain.ValueObjects;

namespace MkApi.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string HashedPassword { get; private set; }
    public bool IsAdmin { get; private set; }
    public List<CatFact> FavoriteCatFacts { get; private set; }

    public UserEntity(Guid id, string username, string hashedPassword, 
        List<CatFact> favoriteCatFacts, bool isAdmin = false)
    {
        if (string.IsNullOrWhiteSpace(username)) 
            throw new ArgumentException("Username cannot be empty");
        
        Id = id;
        Username = username;
        IsAdmin = isAdmin;
        HashedPassword = hashedPassword;
        FavoriteCatFacts = favoriteCatFacts;
    }

    public void SetAdmin(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void AddCatFact(CatFact catFact)
    {
        FavoriteCatFacts.Add(catFact);
    }

    public void ChangeUsername(string newUsername)
    {
        if (string.IsNullOrWhiteSpace(newUsername))
            throw new ArgumentException("Username cannot be empty");
        Username = newUsername;
    }

    public bool ValidatePassword(string password, IPasswordHasher<UserEntity> hasher)
    {
        return hasher.VerifyHashedPassword(this, HashedPassword, password) == PasswordVerificationResult.Success;
    }

    public override bool Equals(object? obj)
    {
        if (obj is UserEntity entity)
        {
            return Id == entity.Id;
        }
        return false;
    }

    public override int GetHashCode() 
        => Id.GetHashCode();
}

using Microsoft.AspNetCore.Identity;
using MkApi.Domain.Exceptions;
using MkApi.Domain.ValueObjects;

namespace MkApi.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string HashedPassword { get; private set; }
    public List<string> FavoriteCatFacts { get; private set; }
    IPasswordHasher<UserEntity> m_PasswordHasher { get; }

    public UserEntity(Guid id, string username, string hashedPassword, 
        List<string> favoriteCatFacts)
    {
        if (string.IsNullOrWhiteSpace(username)) 
            throw new ArgumentException("Username cannot be empty");
        
        Id = id;
        Username = "";
        ChangeUsername(username);
        HashedPassword = hashedPassword;
        FavoriteCatFacts = favoriteCatFacts;

        m_PasswordHasher = new PasswordHasher<UserEntity>();
    }

    public void AddCatFact(string catFact)
    {
        if (string.IsNullOrWhiteSpace(catFact))
            throw new ArgumentException("Cat Fact cannot be empty");
        FavoriteCatFacts.Add(catFact);
    }

    public void ChangeUsername(string newUsername)
    {
        if (!ValidateUsername(newUsername))
            throw new ArgumentException("Username is invalid");
        Username = newUsername;
    }

    /// <summary>
    /// Change user password
    /// </summary>
    /// <param name="oldPlainTextPassword">Old "current" password</param>
    /// <param name="newPlainTextPassword">New password</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="UserAuthenticationException"></exception>
    public void ChangePassword(string oldPlainTextPassword, string newPlainTextPassword)
    {
        if (!ValidatePassword(oldPlainTextPassword) || !ValidatePassword(newPlainTextPassword))
            throw new ArgumentException("New password or old password format is invalid.");

        if (!CheckPassword(oldPlainTextPassword))
            throw new UserAuthenticationException("Old password is invalid");

        SetPassword(newPlainTextPassword);
    }

    public bool CheckPassword(string password)
        => m_PasswordHasher.VerifyHashedPassword(this, HashedPassword, password) == PasswordVerificationResult.Success;

    public void SetPassword(string plainTextPassword)
    {
        if (!ValidatePassword(plainTextPassword))
            throw new ArgumentException("Password is invalid");

        HashedPassword = m_PasswordHasher.HashPassword(this, plainTextPassword);
    }

    public bool ValidateUsername(string username)
        => !string.IsNullOrWhiteSpace(username);

    public bool ValidatePassword(string plainTextPassword)
        => !string.IsNullOrWhiteSpace(plainTextPassword);

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

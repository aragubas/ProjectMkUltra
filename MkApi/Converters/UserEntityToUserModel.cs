using MkApi.Domain.Entities;
using MkApi.Models;

namespace MkApi.Converters;

public class UserEntityToUserModel
{
    public UserModel UserModel { get; }
    public UserEntityToUserModel(UserEntity entity)
    {
        UserModel = new() {
            Id = entity.Id,
            HashedPassword = entity.HashedPassword,
            Username = entity.Username,
            FavoriteCatFacts = entity.FavoriteCatFacts
        };
    }
}
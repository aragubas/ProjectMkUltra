using MkApi.Domain.Entities;
using MkApi.Models;

namespace MkApi.Converters;

public class UserModelToUserEntity
{
    public UserEntity UserEntity { get; }

    public UserModelToUserEntity(UserModel model)
    {
        UserEntity = new(model.Id, model.Username, model.HashedPassword, model.FavoriteCatFacts);
    }
}
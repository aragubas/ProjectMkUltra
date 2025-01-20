using MkApi.Application.DTO;
using MkApi.Domain.Entities;
namespace MkApi.Application.Converters;

public class UserEntityToUserDTO
{
    public UserDTO UserDTO { get; }
    public UserEntityToUserDTO(UserEntity entity)
    {
        UserDTO = new() {
            Id = entity.Id,
            FavoriteCatFacts = entity.FavoriteCatFacts,
            Username = entity.Username
        };
    }
}
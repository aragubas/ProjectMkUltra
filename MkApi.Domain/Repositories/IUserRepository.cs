using MkApi.Domain.Entities;
using MkApi.Domain.ValueObjects;

namespace MkApi.Domain.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetById(Guid id);
    Task<UserEntity?> GetByUsername(string username);
    Task AddAsync(UserEntity user);
    Task DeleteAsync(Guid id);
}
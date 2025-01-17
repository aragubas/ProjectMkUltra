using MkApi.Domain.Entities;
using MkApi.Domain.Repositories;

namespace MkApi.Repositories;

public class UserRepository : IUserRepository
{
    public Task AddAsync(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity?> GetByUsername(string username)
    {
        throw new NotImplementedException();
    }
}
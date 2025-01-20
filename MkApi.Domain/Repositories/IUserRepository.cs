using MkApi.Domain.Entities;
using MkApi.Domain.Exceptions;

namespace MkApi.Domain.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Get user by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserEntity?> GetById(Guid id);
    Task<UserEntity?> GetByUsername(string username);
    Task AddAsync(UserEntity user);
    /// <summary>
    /// Deletes an user
    /// </summary>
    /// <param name="id">UserID</param>
    /// <exception cref="UserNotFoundException"/>
    Task DeleteAsync(Guid id);
    /// <summary>
    /// Updates an user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="newUser">New user data to replace old one with</param>
    /// <exception cref="UserNotFoundException"/>
    Task UpdateUser(Guid userId, UserEntity newUser);
    Task<List<UserEntity>> GetUsers();
}
using Microsoft.EntityFrameworkCore;
using MkApi.Converters;
using MkApi.Database;
using MkApi.Domain.Entities;
using MkApi.Domain.Exceptions;
using MkApi.Domain.Repositories;
using MkApi.Models;

namespace MkApi.Repositories;

public class UserRepository : IUserRepository
{
    MkApiDatabase m_Database { get; }

    public UserRepository(MkApiDatabase database)
    {
        m_Database = database;
    }

    public async Task AddAsync(UserEntity user)
    {
        await m_Database.Users.AddAsync(new UserEntityToUserModel(user).UserModel);
        await m_Database.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        UserModel? entity = await m_Database.Users.FindAsync(id);

        if (entity == null)
            throw new UserNotFoundException($"User ID \"{id}\" not found");

        m_Database.Users.Remove(entity);
        await m_Database.SaveChangesAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        UserModel? model = await m_Database.Users.FindAsync(id);

        if (model == null)
            return null;

        return new UserModelToUserEntity(model).UserEntity;
    }

    public async Task<UserEntity?> GetByUsername(string username)
    {
        UserModel? model = await m_Database.Users.
            Where(user => user.Username == username).
            FirstOrDefaultAsync();

        if (model == null)
            return null;

        return new UserModelToUserEntity(model).UserEntity;
    }

    public async Task<List<UserEntity>> GetUsers()
    {
        int count = await m_Database.Users.CountAsync();
        
        List<UserModel> users = await m_Database.Users
            .Take(count)
            .OrderBy(model => model.Username)
            .ToListAsync();
        
        List<UserEntity> userEntities = new();
        foreach(UserModel model in users)
        {
            userEntities.Add(new UserModelToUserEntity(model).UserEntity);
        }

        return userEntities;
    }

    public async Task UpdateUser(Guid userId, UserEntity newUser)
    {
        UserModel? user = await m_Database.Users.FindAsync(userId);

        if (user == null)
            throw new UserNotFoundException($"User ID \"{userId}\" not found");

        user.Username = newUser.Username;
        user.HashedPassword = newUser.HashedPassword;
        user.FavoriteCatFacts = newUser.FavoriteCatFacts;

        await m_Database.SaveChangesAsync();
    }
}
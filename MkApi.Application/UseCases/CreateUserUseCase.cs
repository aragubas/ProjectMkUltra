using Microsoft.AspNetCore.Identity;
using MkApi.Application.DTO;
using MkApi.Application.Response;
using MkApi.Domain.Entities;
using MkApi.Domain.Repositories;

namespace MkApi.Application.UseCases;

public class CreateUserUseCase
{
    IUserRepository m_UserRepository;

    public CreateUserUseCase(IUserRepository userRepository)
    {
        m_UserRepository = userRepository;
    }

    public async Task<CreateUserResponse> Execute(UserCredentialsDTO userCreation)
    {
        // Check if user already exists
        UserEntity? userExists = await m_UserRepository.GetByUsername(userCreation.Username);
        if (userExists != null)
            return new CreateUserResponse(false, null, "user_already_exists");
        
        UserEntity entity = new(Guid.NewGuid(), userCreation.Username, "", new() { "Cats are liquid" });
        entity.SetPassword(userCreation.PlainTextPassword);
        
        await m_UserRepository.AddAsync(entity);
        UserDTO newUser = new() 
        {
            Id = entity.Id,
            Username = entity.Username,
            FavoriteCatFacts = entity.FavoriteCatFacts
        };

        return new CreateUserResponse(true, newUser);
    }
}
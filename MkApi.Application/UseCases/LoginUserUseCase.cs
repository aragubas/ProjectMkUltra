using Microsoft.AspNetCore.Identity;
using MkApi.Application.DTO;
using MkApi.Application.Response;
using MkApi.Domain.Entities;
using MkApi.Domain.Repositories;

namespace MkApi.Application.UseCases;

public class LoginUserCase
{
    IUserRepository m_UserRepository { get; }
    IPasswordHasher<UserEntity> m_PasswordHasher { get; }

    public LoginUserCase(IUserRepository userRepository)
    {
        m_UserRepository = userRepository;
        m_PasswordHasher = new PasswordHasher<UserEntity>();
    }

    public async Task<GenericResponse> Execute(UserCredentialsDTO userCredentials)
    {
        UserEntity? user = await m_UserRepository.GetByUsername(userCredentials.Username);
        if (user == null)
            return new GenericResponse(false, "invalid_username_or_password");

        if (!user.CheckPassword(userCredentials.PlainTextPassword))
            return new GenericResponse(false, "invalid_username_or_password");

        return new GenericResponse(true, "login_successful");
    }
}
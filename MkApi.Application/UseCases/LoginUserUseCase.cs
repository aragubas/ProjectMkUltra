using Microsoft.AspNetCore.Identity;
using MkApi.Application.Response;
using MkApi.Domain.Entities;
using MkApi.Domain.Repositories;

namespace MkApi.Application.UseCases;

public class LoginUserCase
{
    readonly IUserRepository userRepository;
    readonly IPasswordHasher<UserEntity> passwordHasher;

    public LoginUserCase(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
        passwordHasher = new PasswordHasher<UserEntity>();
    }

    public async Task<LoginResultResponse> Execute(string username, string password)
    {
        UserEntity? user = await userRepository.GetByUsername(username);
        if (user == null)
            return new LoginResultResponse(false, "invalid_username_or_password");

        if (passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password) != PasswordVerificationResult.Success)
            return new LoginResultResponse(false, "invalid_username_or_password");

        return new LoginResultResponse(true, "login_successful");
    }
}
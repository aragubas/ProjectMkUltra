using MkApi.Application.DTO;
using MkApi.Application.Response;
using MkApi.Domain.Entities;
using MkApi.Domain.Exceptions;
using MkApi.Domain.Repositories;

namespace MkApi.Application.UseCases;

public class PatchUserUseCase
{
    IUserRepository m_UserRepository;

    public PatchUserUseCase(IUserRepository userRepository)
    {
        m_UserRepository = userRepository;
    }

    public async Task<GenericResponse> Execute(UserPatchDTO userPatch)
    {
        UserEntity? user = await m_UserRepository.GetByUsername(userPatch.CurrentUsername);

        if (user == null)
            return new GenericResponse(false, "user_not_found");

        // Authenticate user
        if (!user.CheckPassword(userPatch.CurrentPlainTextPassword))
            return new GenericResponse(false, "invalid_password");

        // What should I change about user?
        bool usernameIsEmpty = string.IsNullOrWhiteSpace(userPatch.NewUsername);
        bool passwordIsEmpty = string.IsNullOrWhiteSpace(userPatch.NewPlainTextPassword);

        // One if them needs to have something for us to change anything
        if (usernameIsEmpty && passwordIsEmpty)
            return new GenericResponse(false, "nothing_to_change");

        try
        {
            if (!usernameIsEmpty)
                user.ChangeUsername(userPatch.NewUsername);

            if (!passwordIsEmpty)
                user.ChangePassword(userPatch.CurrentPlainTextPassword, userPatch.NewPlainTextPassword);
        
        } catch (ArgumentException)
        {
            return new GenericResponse(false, "new_information_format_invalid");
        
        } catch (UserAuthenticationException)
        {
            return new GenericResponse(false, "invalid_password");
        }

        // Try actually updating the user
        try
        {
            await m_UserRepository.UpdateUser(user.Id, user);

        } catch (UserNotFoundException)
        {
            return new GenericResponse(false, "user_not_found");
        }

        return new GenericResponse(true, null);
    }
}
using MkApi.Application.DTO;
using MkApi.Application.Response;
using MkApi.Domain.Entities;
using MkApi.Domain.Exceptions;
using MkApi.Domain.Repositories;

namespace MkApi.Application.UseCases;

public class DeleteUserUseCase
{
    IUserRepository m_UserRepository;
    LoginUserCase m_LoginUserUseCase;
    
    public DeleteUserUseCase(IUserRepository userRepository, LoginUserCase loginUserUseCase)
    {
        m_UserRepository = userRepository;
        m_LoginUserUseCase = loginUserUseCase;
    }

    public async Task<GenericResponse> Execute(UserCredentialsDTO userCredentials)
    {
        GenericResponse response = await m_LoginUserUseCase.Execute(userCredentials);

        if (!response.Success)
            return new GenericResponse(false, response.Message);

        UserEntity? user = await m_UserRepository.GetByUsername(userCredentials.Username);

        if (user == null)
            return new GenericResponse(false, "user_not_found");

        try
        {
            await m_UserRepository.DeleteAsync(user.Id);

            return new GenericResponse(true, null);
            
        } catch (UserNotFoundException)
        {
            return new GenericResponse(false, "user_not_found");
        }

    }
}
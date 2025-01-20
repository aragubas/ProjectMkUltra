using MkApi.Application.Converters;
using MkApi.Application.Response;
using MkApi.Domain.Entities;
using MkApi.Domain.Repositories;

namespace MkApi.Application.UseCases;

public class GetUsersUseCase
{
    IUserRepository m_UserRepository { get; }

    public GetUsersUseCase(IUserRepository userRepository)
    {
        m_UserRepository = userRepository;
    }

    public async Task<GetUsersResponse> Execute()
    {
        GetUsersResponse response = new(true, new());
        
        List<UserEntity> users = await m_UserRepository.GetUsers();

        foreach(UserEntity user in users)
        {
            response.Users.Add(new UserEntityToUserDTO(user).UserDTO);
        }

        return response;
    }

}
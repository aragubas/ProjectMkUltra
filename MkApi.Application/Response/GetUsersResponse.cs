using MkApi.Application.DTO;

namespace MkApi.Application.Response;

public class GetUsersResponse : ResponseBase
{
    public List<UserDTO> Users { get; }

    public GetUsersResponse(bool success, List<UserDTO> users) : base(success)
    {
        Users = users;
    }
}
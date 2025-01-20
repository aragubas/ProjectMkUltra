using Microsoft.AspNetCore.Mvc;
using MkApi.Application.DTO;
using MkApi.Application.Response;
using MkApi.Application.UseCases;

namespace MkApi.Controllers;

[Route("/users")]
public class UserController : ControllerBase
{
    LoginUserCase m_LoginUseCase { get; }
    GetUsersUseCase m_GetUsersUseCase { get; }
    CreateUserUseCase m_CreateUserUseCase { get; }
    DeleteUserUseCase m_DeleteUserUseCase { get; }
    PatchUserUseCase m_PatchUserUseCase { get; }

    public UserController(LoginUserCase loginUseCase, 
        GetUsersUseCase getUsersUseCase, CreateUserUseCase createUserUseCase,
        DeleteUserUseCase deleteUserUseCase, PatchUserUseCase patchUserUseCase)
    {
        m_LoginUseCase = loginUseCase;
        m_GetUsersUseCase = getUsersUseCase;
        m_CreateUserUseCase = createUserUseCase;
        m_DeleteUserUseCase = deleteUserUseCase;
        m_PatchUserUseCase = patchUserUseCase;
    }

    [HttpGet]
    public async Task<ActionResult<List<GetUsersResponse>>> Get()
    {
        return Ok(await m_GetUsersUseCase.Execute());
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> Post([FromBody] UserCredentialsDTO userCredentials)
    {
        CreateUserResponse response = await m_CreateUserUseCase.Execute(userCredentials);

        if (!response.Success)
            return BadRequest(response);
        
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<GenericResponse>> Delete([FromBody] UserCredentialsDTO userCredentials)
    {
        GenericResponse deleteUserResponse = await m_DeleteUserUseCase.Execute(userCredentials);

        if (!deleteUserResponse.Success)
            return BadRequest(deleteUserResponse);

        return Ok(deleteUserResponse);
    }

    [HttpPatch]
    public async Task<ActionResult<GenericResponse>> Patch([FromBody] UserPatchDTO userPatch)
    {
        GenericResponse patchUserResponse = await m_PatchUserUseCase.Execute(userPatch);

        if (!patchUserResponse.Success)
            return BadRequest(patchUserResponse);
        
        return Ok(patchUserResponse);
    }
}
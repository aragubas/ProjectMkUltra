namespace MkApi.Application.DTO;

public class UserPatchDTO
{
    public required string CurrentUsername { get; set; }
    public required string CurrentPlainTextPassword { get; set; }
    public required string NewUsername { get; set; }
    public required string NewPlainTextPassword { get; set; }
}
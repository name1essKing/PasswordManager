namespace PasswordManager.Dto.DTOs;

public class CreatePasswordDto
{
    public string Title { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
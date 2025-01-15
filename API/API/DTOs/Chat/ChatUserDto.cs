using API.Models;

namespace API.DTOs.Chat;

public class ChatUserDto
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public UserRole Role { get; set; }

}

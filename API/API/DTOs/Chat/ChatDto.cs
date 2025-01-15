using API.DTOs.Message;
using API.Models;

namespace API.DTOs.Chat;

public class ChatDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<MessageDto> Messages { get; set; } = [];
    public ICollection<ChatUserDto> Users { get; set; } = [];
    public ChatType Type { get; set; }
}

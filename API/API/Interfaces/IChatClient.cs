using API.DTOs.Message;

namespace API.Interfaces;

public interface IChatClient
{
    public Task ReceiveMessage(MessageDto messageDto);
}

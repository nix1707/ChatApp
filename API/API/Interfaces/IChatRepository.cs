using API.Models;

namespace API.Interfaces;

public interface IChatRepository
{
    Task<Chat> GetChatAsync(int id);
    Task<Chat> CreateRoomAsync(string name, string userId);
    Task JoinRoomAsync(int chatId, string userId);
    Task<IEnumerable<Chat>> GetChatsAsync(string userId);
    Task<Chat> CreatePrivateRoomAsync(string roomId, string targetId);
    Task<IEnumerable<Chat>> GetAvailableRoomsAsync(string userId);
    Task<Message> CreateMessage(int chatId, string message, SentimentAnalysis analysis, string userId);
}

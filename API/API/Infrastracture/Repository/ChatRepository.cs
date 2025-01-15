using API.Database;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastracture.Repository;

public class ChatRepository(AppDbContext context) : IChatRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Message> CreateMessage(int chatId, string message, SentimentAnalysis analysis, string userId)
    {
        var newMessage = new Message
        {
            ChatId = chatId,
            Text = message,
            Name = userId,
            Analysis = analysis,
            Timestamp = DateTime.UtcNow
        };

        await _context.Messages.AddAsync(newMessage);
        await _context.SaveChangesAsync();

        return newMessage;
    }

    public async Task<Chat> CreatePrivateRoomAsync(string rootId, string targetId)
    {
        var existingChat = await _context.Chats
             .Include(c => c.Users)
             .Where(c => c.Type == ChatType.Private &&
                    c.Users.Any(u => u.UserId == rootId) &&
                    c.Users.Any(u => u.UserId == targetId))
             .FirstOrDefaultAsync();

        if (existingChat != null)
        {
            return existingChat;
        }

        var chat = new Chat
        {
            Type = ChatType.Private,
            Users =
            [
                new() { UserId = targetId },
                new() { UserId = rootId }
            ]
        };

        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();

        return chat;
    }

    public async Task<Chat> CreateRoomAsync(string name, string userId)
    {
        var chat = new Chat
        {
            Name = name,
            Type = ChatType.Room,
            Users =
            [
                new()
                {
                    UserId = userId,
                    Role = UserRole.Admin
                }
            ]
        };

        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();

        return chat;
    }

    public async Task<IEnumerable<Chat>> GetAvailableRoomsAsync(string userId)
    {
        return await _context.Chats
             .AsNoTracking()
             .Include(x => x.Messages)
             .Include(x => x.Users)
                 .ThenInclude(cu => cu.User)
             .Where(x => x.Type == ChatType.Room && !x.Users.Any(y => y.UserId == userId))
             .ToListAsync();
    }


    public async Task<Chat> GetChatAsync(int id)
    {
        return await _context.Chats
            .AsNoTracking()
            .Include(x => x.Messages)
            .Include(x => x.Users)
                .ThenInclude(u => u.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Chat>> GetChatsAsync(string userId)
    {
        return await _context.Chats
            .AsNoTracking()
            .Include(x => x.Users)
                .ThenInclude(cu => cu.User)
            .Include(x => x.Messages)
            .Where(x => x.Users.Any(y => y.UserId == userId))
            .ToListAsync();
    }

    public async Task JoinRoomAsync(int chatId, string userId)
    {
        var existingMembership = await _context.ChatUsers
            .AnyAsync(cu => cu.ChatId == chatId && cu.UserId == userId);

        if (!existingMembership)
        {
            var chatUser = new ChatUser
            {
                ChatId = chatId,
                UserId = userId,
                Role = UserRole.Member
            };

            await _context.ChatUsers.AddAsync(chatUser);
            await _context.SaveChangesAsync();
        }
    }
}
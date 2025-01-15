using API.DTOs.Message;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class ChatHub(IChatRepository repository, IMapper mapper, ILogger<ChatHub> logger, SentimentAnalysisService sentimentAnalysis) : Hub<IChatClient>
{
    private readonly IChatRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ChatHub> _logger = logger;
    private readonly SentimentAnalysisService _sentimentAnalysis = sentimentAnalysis;

    public async Task SendMessage(int roomId, string message)
    {
        var analysis = await _sentimentAnalysis.DetectSentimentAsync(message);
        var Message = await _repository.CreateMessage(roomId, message, analysis, Context.User.Identity.Name);

        _logger.LogInformation("This messages has {sentiment} context with scrore {score}", analysis.Sentiment, analysis.Score);

        await Clients.Group(roomId.ToString())
            .ReceiveMessage(_mapper.Map<MessageDto>(Message));
    }

    public override async Task OnConnectedAsync()
    {
        var context = Context.GetHttpContext();
        var roomId = context.Request.Query["roomId"];
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

        await base.OnConnectedAsync();
    }

    public Task LeaveRoom(string roomId)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
    }
}

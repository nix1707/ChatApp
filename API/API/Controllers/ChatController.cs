using API.Database;
using API.DTOs.Chat;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ChatController(IChatRepository repository, IMapper mapper) : BaseApiController
{
    private readonly IChatRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetChats()
    {
        var chats = await _repository.GetChatsAsync(GetUserId());
        return Ok(_mapper.Map<List<ChatDto>>(chats));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetChatById(int id)
    {
        var chat = await _repository.GetChatAsync(id);
        return Ok(_mapper.Map<ChatDto>(chat));
    }


    [HttpPost]
    public async Task<ActionResult<ChatDto>> CreateRoom([FromQuery(Name = "name")] string name)
    {
        var chat = await _repository.CreateRoomAsync(name, GetUserId());
        return Ok(_mapper.Map<ChatDto>(chat));
    }


    [HttpPost("join/{id}")]
    public async Task<IActionResult> JoinRoom(int id)
    {
        await _repository.JoinRoomAsync(id, GetUserId());
        return Ok();
    }

    [HttpGet("available-rooms")]
    public async Task<IActionResult> GetAvailableRooms()
    {
        var rooms = await _repository.GetAvailableRoomsAsync(GetUserId());
        return Ok(_mapper.Map<List<ChatDto>>(rooms));
    }


    [HttpPost("private")]
    public async Task<ActionResult<int>> CreatePrivateRoom([FromQuery(Name = "targetId")] string targetId)
    {
        var chat = await _repository.CreatePrivateRoomAsync(GetUserId(), targetId);
        return Ok(_mapper.Map<ChatDto>(chat));
    }

    [HttpGet("available-users")]
    public async Task<IActionResult> GetUsers([FromServices] AppDbContext ctx)
    {
        var users = await ctx.Users
            .Where(x => x.Id != GetUserId())
            .ToListAsync();

        return Ok(users.Select(u => new {  u.UserName, UserId = u.Id }));
    }
}

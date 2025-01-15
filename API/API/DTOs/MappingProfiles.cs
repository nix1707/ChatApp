using API.DTOs.Chat;
using API.DTOs.Message;
using API.Models;
using AutoMapper;

namespace API.DTOs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Models.Message, MessageDto>();
        CreateMap<Models.Chat, ChatDto>();

        CreateMap<ChatUser, ChatUserDto>()
            .ForMember(d => d.Username, o => o.MapFrom(s => s.User.UserName));
    }
}

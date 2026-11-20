using AutoMapper;
using PasswordManager.Dto.DTOs;
using PasswordManager.Server.Models;

namespace PasswordManager.Server.Mappings;

public class PasswordMappingProfile : Profile
{
    public PasswordMappingProfile()
    {
        CreateMap<PasswordItem, PasswordDto>();

        CreateMap<CreatePasswordDto, PasswordItem>()
            .ForMember(d => d.Id, o => o.Ignore()) 
            .ForMember(d => d.CreatedAt, o => o.MapFrom(x => DateTime.UtcNow)); 
    }
}
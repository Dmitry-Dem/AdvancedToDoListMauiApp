using AdvancedToDoListMauiApp.Models;
using AutoMapper;

namespace AdvancedToDoListMauiApp.Mappers;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Punishment, PunishmentChanges>()
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Description))
            .ForMember(x => x.PunishmentId, opt => opt.MapFrom(src => src.Id));
    }
}


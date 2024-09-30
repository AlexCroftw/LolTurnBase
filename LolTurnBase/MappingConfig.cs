using AutoMapper;
using LolTurnBase.Models;
using LolTurnBase.Models.DTO;

namespace LolTurnBase
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Champion,ChampionDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
        }
    }
}

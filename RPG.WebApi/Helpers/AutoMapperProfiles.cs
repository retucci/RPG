using AutoMapper;
using RPG.Domain.Entities;
using RPG.WebApi.Dto;

namespace RPG.WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Character,CharacterDto>().ReverseMap();
            CreateMap<Weapon,WeaponDto>().ReverseMap();
            CreateMap<Move,MoveDto>().ReverseMap();
         }
    }
}
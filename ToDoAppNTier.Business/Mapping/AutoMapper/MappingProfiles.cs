using AutoMapper;
using ToDoAppNTier.Dtos.Dtos;
using ToDoAppNTier.Entities.Domains;

namespace ToDoAppNTier.Business.Mapping.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Work, WorkDto>().ReverseMap();
            CreateMap<Work, WorkCreateDto>().ReverseMap();
        }
    }
}

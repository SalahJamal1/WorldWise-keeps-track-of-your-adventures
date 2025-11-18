using AutoMapper;
using MappyApplication.Data;
using MappyApplication.Models.cities;
using MappyApplication.Models.user;
using MappyApplication.Models.workouts;

namespace MappyApplication.Configuration;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, AuthRegister>().ReverseMap();
        CreateMap<Workouts, WorkoutsUpdate>().ReverseMap();
        CreateMap<Workouts, WorkoutsDto>().ReverseMap();
        CreateMap<Cities, CitiesUpdate>().ReverseMap();
        CreateMap<Cities, CitiesDto>().ReverseMap();
    }
}
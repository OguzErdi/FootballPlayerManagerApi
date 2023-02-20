using AutoMapper;
using FootballPlayerManagerApi.Contracts.Request;
using FootballPlayerManagerApi.Entities;

namespace FootballPlayerManagerApi.Helpers;

public class FootballPlayerManagerApiMapper : Profile
{
    public FootballPlayerManagerApiMapper()
    {
        CreateMap<PlayerUpdateRequest, Player>();
    }
}
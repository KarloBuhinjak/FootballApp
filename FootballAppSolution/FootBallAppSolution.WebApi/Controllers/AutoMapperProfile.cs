using AutoMapper;
using System;
using FootballAppSolution.Model;

namespace FootballAppSolution.WebApi.Controllers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PlayerRequest, Player>(); 
        CreateMap<Player, PlayerRequest>();
        CreateMap<Player, PlayerResponse>();
    }
    
}
using AutoMapper;
using FootballAppSolution.Model;
using Microsoft.AspNetCore.Mvc;
using FootballAppSolution.Common;
using FootballAppSolution.Service.Common;

namespace FootballAppSolution.WebApi.Controllers;

public class FootballClubController: ControllerBase 
{
    private readonly IFootballAppService playerService;
    private readonly IMapper mapper;
}
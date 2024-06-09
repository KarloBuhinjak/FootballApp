using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FootballAppSolution.Repository;
using FootballAppSolution.Repository.Common;
using FootballAppSolution.Service;
using FootballAppSolution.Service.Common;
using FootballAppSolution.WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder
        .RegisterType<PlayerService>()
        .As<IFootballAppService>()
        .InstancePerLifetimeScope();
    containerBuilder
        .RegisterType<PlayerRepository>()
        .As<IFootballAppRepository>()
        .InstancePerLifetimeScope();
});



var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
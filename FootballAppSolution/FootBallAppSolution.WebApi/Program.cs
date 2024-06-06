using Autofac;
using Autofac.Extensions.DependencyInjection;
using FootballAppSolution.Repository;
using FootballAppSolution.Repository.Common;
using FootballAppSolution.Service;
using FootballAppSolution.Service.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddScoped<IFootballAppService, PlayerService>();
// builder.Services.AddScoped<IFootballAppRepository, PlayerRepository>();

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
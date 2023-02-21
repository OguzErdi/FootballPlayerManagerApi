using AutoMapper;
using FootballPlayerManagerApi.ConfigOptions;
using FootballPlayerManagerApi.Couchbase.Providers.Implementations;
using FootballPlayerManagerApi.Couchbase.Providers.Interfaces;
using FootballPlayerManagerApi.Helpers;
using FootballPlayerManagerApi.Repositories.Implementations;
using FootballPlayerManagerApi.Repositories.Interfaces;
using FootballPlayerManagerApi.Services.Implementations;
using FootballPlayerManagerApi.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.EnableAnnotations();
});
builder.Services.Configure<CouchbaseOptions>(builder.Configuration.GetSection("CouchbaseOptions"));


// Add Application Service
builder.Services.AddSingleton<ICouchbaseProvider, CouchbaseProvider>();
builder.Services.AddSingleton<IFootballProvider, FootballProvider>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();

// AutoMapper
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new FootballPlayerManagerApiMapper()); });
var mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Serilog
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serilog Request Logging
app.UseSerilogRequestLogging(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
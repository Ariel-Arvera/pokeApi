using PokeApi.Application.Interfaces;
using PokeApi.Application.Services;
using PokeApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "PokeAPI",
        Version = "v1",
        Description = "API REST de Pokémon - Calcula daños, gestiona Pokémon y movimientos"
    });
});

builder.Services.AddScoped<IMoveRepository, MoveRepository>();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

builder.Services.AddScoped<MoveService>();
builder.Services.AddScoped<PokemonService>();
builder.Services.AddScoped<BattleService>();
builder.Services.AddScoped<DamageCalculator>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokeAPI v1");
    c.RoutePrefix = "";
});

app.MapControllers();

app.Run();
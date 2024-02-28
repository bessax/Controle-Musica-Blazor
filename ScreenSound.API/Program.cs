using ScreenSound.API.Endpoints;
using ScreenSound.API.Services;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Context;
using ScreenSound.Shared.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ScreenSoundContext>();
builder.Services.AddTransient(typeof(EntityDAL<Artista>));
builder.Services.AddTransient(typeof(EntityDAL<Musica>));
builder.Services.AddTransient(typeof(EntityDAL<Genero>));
builder.Services.AddTransient(typeof(ArtistaConverter));
builder.Services.AddTransient(typeof(MusicaConverter));
builder.Services.AddTransient(typeof(GeneroConverter));
builder.Services.AddCors();
var app = builder.Build();
app.UseCors(options =>
{
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();

});
 app.UseStaticFiles();
// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseHttpsRedirection();

app.AddEndPointArtistas();

app.AddEndPointMusicas();

app.AddEndPointGeneros();

app.UseSwaggerUI();

app.Run();



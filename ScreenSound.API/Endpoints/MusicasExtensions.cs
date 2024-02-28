using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.DTO;
using ScreenSound.API.Services;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Modelos;

namespace ScreenSound.API.Endpoints;

public static class MusicasExtensions
{
    public static void AddEndPointMusicas(this WebApplication app)
    {
    
            app.MapPost("/Musicas", ([FromServices] MusicaConverter converter,[FromServices]EntityDAL<Musica> entityDAL,[FromBody] MusicaRequest musicaReq) =>
            {
                entityDAL.Adicionar(converter.RequestToEntity(musicaReq));
            }).WithTags("Música").WithSummary("Adiciona uma nova música.").WithOpenApi();

        app.MapGet("/Musicas", ([FromServices] MusicaConverter converter,[FromServices] EntityDAL<Musica> entityDAL) =>
            {
                return converter.EntityListToResponseList(entityDAL.Listar());
            }).WithTags("Música").WithSummary("Listagem de músicas cadastradas.").WithOpenApi();

        app.MapGet("/Musicas/{nome}", ([FromServices] MusicaConverter converter, [FromServices]EntityDAL<Musica> entityDAL,string nome) =>
            {
                var musica = entityDAL.RecuperarPor(a => a.Nome == nome);
                if (musica is not null)
                {
                    var response = converter.EntityToResponse(musica!);
                    return Results.Ok(response);
                }
                return Results.NotFound("Música não encontrado.");
                
            }).WithTags("Música").WithSummary("Retorna uma música pelo nome.").WithOpenApi();

        app.MapDelete("/Musicas/{id}", ([FromServices]EntityDAL<Musica> entityDAL,int id) =>
            {
                var musica = entityDAL.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound("Música para exclusão não encontrada.");
                }
                entityDAL.Deletar(musica);
                return Results.NoContent();
            }).WithTags("Música").WithSummary("Exclui uma música.").WithOpenApi();

        app.MapPut("/Musicas", ([FromServices] MusicaConverter converter,[FromServices]EntityDAL<Musica> entityDAL,[FromBody] MusicaRequestEdit musicaRequestEdit) =>
            {
                entityDAL.Atualizar(converter.RequestToEntityEdit(musicaRequestEdit));
            }).WithTags("Música").WithSummary("Atualiza o restro de uma música.").WithOpenApi();
    }   
}

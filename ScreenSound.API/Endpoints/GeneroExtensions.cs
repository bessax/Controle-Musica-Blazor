using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.DTO;
using ScreenSound.API.Services;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints;

public static class GeneroExtensions
{

    public static void AddEndPointGeneros(this WebApplication app)
    {
        app.MapPost("/Generos", ([FromServices] GeneroConverter converter,[FromServices] EntityDAL<Genero> entityDAL,[FromBody] GeneroRequest generoReq) =>
        {
            entityDAL.Adicionar(converter.RequestToEntity(generoReq));
        }).WithTags("Gênero").WithSummary("Adiciona um novo gênero").WithOpenApi();


        app.MapGet("/Generos", ([FromServices] GeneroConverter converter, [FromServices] EntityDAL<Genero> entityDAL) =>
        {
            return converter.EntityListToResponseList(entityDAL.Listar());
        }).WithTags("Gênero").WithSummary("Listagem de gêneros cadastrados.").WithOpenApi();

        app.MapGet("/Generos/{nome}", ([FromServices] GeneroConverter converter,[FromServices] EntityDAL<Genero> entityDAL,string nome) =>
        {
            var genero = entityDAL.RecuperarPor(a => a.Nome == nome);
            if (genero is not null)
            {
                var response = converter.EntityToResponse(genero!);
                return Results.Ok(response);
            }
            return Results.NotFound("Gênero não encontrado.");
        }).WithTags("Gênero").WithSummary("Busca um gênero por nome.").WithOpenApi();

        app.MapDelete("/Generos/{id}", ([FromServices] EntityDAL<Genero> entityDAL,int id) =>
        {
            var genero = entityDAL.RecuperarPor(a => a.Id == id);
            if (genero is null)
            {
                return Results.NotFound("Gênero para exclusão não encontrado.");
            }
            entityDAL.Deletar(genero);
            return Results.NoContent();
        }).WithTags("Gênero").WithSummary("Exclui um gênero cadastrado.").WithOpenApi();


    }
}

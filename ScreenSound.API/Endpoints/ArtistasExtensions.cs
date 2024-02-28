using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.DTO;
using ScreenSound.API.Services;
using ScreenSound.Shared.Banco;
using ScreenSound.Shared.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{

    public static void AddEndPointArtistas(this WebApplication app)
    {
        app.MapPost("/Artistas", async([FromServices]IHostEnvironment env,[FromServices] ArtistaConverter converter,[FromServices] EntityDAL<Artista> entityDAL,[FromBody] ArtistaRequest artistaReq) =>
        {
            var nome = artistaReq.Nome.Trim();
            var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhss") + "." + nome + ".jpg";

            var path = Path.Combine(env.ContentRootPath,
                "wwwroot", "FotosPerfil", imagemArtista);

            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaReq.FotoPerfil));
            using FileStream fs = new(path, FileMode.Create);
            await ms.CopyToAsync(fs);

            var artista = converter.RequestToEntity(artistaReq);

            artista.FotoPerfil = $"/FotosPerfil/{imagemArtista}";

            entityDAL.Adicionar(artista);

        }).WithTags("Artista").WithSummary("Adiciona um novo artista").WithOpenApi();


        app.MapGet("/Artistas", ([FromServices] ArtistaConverter converter, [FromServices] EntityDAL<Artista> entityDAL) =>
        {
            return converter.EntityListToResponseList(entityDAL.Listar());
        }).WithTags("Artista").WithSummary("Listagem de artistas cadastrados.").WithOpenApi();

        app.MapGet("/Artistas/{nome}", ([FromServices] ArtistaConverter converter,[FromServices] EntityDAL<Artista> entityDAL,string nome) =>
        {
            var artista = entityDAL.RecuperarPor(a => a.Nome == nome);
            if (artista is not null)
            {
                var response = converter.EntityToResponse(artista!);
                return Results.Ok(response);
            }
            return Results.NotFound("Artista não encontrado.");
        }).WithTags("Artista").WithSummary("Busca um artista por nome.").WithOpenApi();

        app.MapDelete("/Artistas/{id}", ([FromServices] EntityDAL<Artista> entityDAL,int id) =>
        {
            var artista = entityDAL.RecuperarPor(a => a.Id == id);
            if (artista is null)
            {
                return Results.NotFound("Artista para exclusão não encontrado.");
            }
            entityDAL.Deletar(artista);
            return Results.NoContent();
        }).WithTags("Artista").WithSummary("Exclui um artista cadastrado.").WithOpenApi();

        app.MapPut("/Artistas", ([FromServices] ArtistaConverter converter,[FromServices] EntityDAL<Artista> entityDAL, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
        {
            entityDAL.Atualizar(converter.RequestToEntityEdit(artistaRequestEdit));
        }).WithTags("Artista").WithSummary("Atualiza o registro de um artista.").WithOpenApi();
    }
}

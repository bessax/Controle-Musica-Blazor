using ScreenSound.API.DTO;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Services;


public class GeneroConverter
{
    public Genero RequestToEntity(GeneroRequest generoRequest)
    {
        return new Genero(){Nome=generoRequest.Nome,Descricao=generoRequest.Descricao};
    }       

    public GeneroResponse EntityToResponse(Genero genero)
    {
        return new GeneroResponse(genero.Nome!, genero.Descricao!);
    }
    public ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> generos)
    {
        return generos.Select(a => EntityToResponse(a)).ToList();
    }

    public ICollection<Genero> RequestListToEntityList(IEnumerable<GeneroRequest> generos)
    {
        return generos.Select(a => RequestToEntity(a)).ToList();
    }
}

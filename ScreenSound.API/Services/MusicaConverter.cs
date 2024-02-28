using ScreenSound.API.DTO;
using ScreenSound.Shared.Modelos;

namespace ScreenSound.API.Services;

public class MusicaConverter
{
    private readonly GeneroConverter generoConverter;
    public MusicaConverter(GeneroConverter generoConverter)
    {
        this.generoConverter = generoConverter;
    }
    public Musica RequestToEntity(MusicaRequest musicaRequest)
    {
        var result = musicaRequest.Generos is null? new List<GeneroRequest>():
            musicaRequest.Generos;

        return new Musica(musicaRequest.Nome) { ArtistaId= musicaRequest.ArtistaId, Generos = 
            generoConverter.RequestListToEntityList(result)
        };
    }

    public Musica RequestToEntityEdit(MusicaRequestEdit musicaRequestEdit)
    {        
        return new Musica(musicaRequestEdit.Nome)
        { Id = musicaRequestEdit.Id};
    }

    public MusicaResponse EntityToResponse(Musica musica)
    {       
       return new MusicaResponse(musica.Id,musica.Nome!,musica.Artista!.Id,musica.Artista.Nome, generoConverter.EntityListToResponseList(musica.Generos));
    }

    public ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicas)
    {
        return musicas.Select(a => EntityToResponse(a)).ToList();
    }
}

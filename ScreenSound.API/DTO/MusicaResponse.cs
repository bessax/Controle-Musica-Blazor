namespace ScreenSound.API.DTO;

public record MusicaResponse(int Id, string Nome, int ArtistaId, string NomeArtista,ICollection<GeneroResponse>? Genero=null);


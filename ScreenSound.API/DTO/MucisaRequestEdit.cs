using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.DTO;
public record MusicaRequestEdit([Required] int Id,[Required] string Nome,[Required] int ArtistaId)
    :MusicaRequest(Nome,ArtistaId);


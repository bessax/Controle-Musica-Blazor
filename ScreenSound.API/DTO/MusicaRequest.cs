using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.DTO;
public record MusicaRequest([Required] string Nome, [Required] int ArtistaId,ICollection<GeneroRequest>? Generos=null);



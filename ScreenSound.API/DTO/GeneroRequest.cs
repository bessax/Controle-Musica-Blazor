using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.DTO;

public record GeneroRequest([Required] string Nome, [Required] string Descricao);

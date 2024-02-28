using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.Shared.Modelos;
public class Musica
{
    public Musica(string nome)
    {
        Nome = nome;
       
    }

    public string? Nome { get; set; }
    public int Id { get; set; }=0;
    public virtual ICollection<Genero> Generos { get; set; }
    public int? ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }
    public void ExibirFichaTecnica()
    {
        Console.WriteLine($@"Nome: {Nome} - Gênero(s):");
        foreach (var item in Generos)
        {
            Console.WriteLine($"{item.ToString()}");
        }

    }
}
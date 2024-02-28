using ScreenSound.Shared.Modelos.Modelos;
using System.Net.Http.Json;

namespace ScreenSound.BlazorApp.Services;

public class GeneroAPI
{
    private readonly HttpClient _httpClient;
    public GeneroAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Genero>?> GetGenerosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Genero>>("generos");
    }
    public async Task<Genero?> GetGeneroPorNomeAsync(string nome)
    {
        return await _httpClient.GetFromJsonAsync<Genero>($"generos/{nome}");
    }
    public async Task AddGeneroAsync(Genero genero)
    {
        await _httpClient.PostAsJsonAsync("generos", genero);
    }
    public async Task UpdateGeneroAsync(Genero genero)
    {
        await _httpClient.PutAsJsonAsync($"generos", genero);
    }
    public async Task DeleteGeneroAsync(int id)
    {
        await _httpClient.DeleteAsync($"generos/{id}");
    }

}

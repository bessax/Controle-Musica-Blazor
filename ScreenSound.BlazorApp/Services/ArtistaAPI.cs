using ScreenSound.Shared.Modelos;
using System.Net.Http.Json;

namespace ScreenSound.BlazorApp.Services;

public class ArtistaAPI
{
    private readonly HttpClient _httpClient;
    public ArtistaAPI(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Artista>?> GetArtistasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Artista>>("artistas");
    }
    public async Task<Artista?> GetArtistaPorNomeAsync(string nome)
    {   var result = await _httpClient.GetFromJsonAsync<Artista>($"artistas/{nome}");
        return result;
    }
    public async Task AddArtistaAsync(Artista artista)
    {
        await _httpClient.PostAsJsonAsync("artistas", artista);
    }
    public async Task UpdateArtistaAsync(Artista artista)
    {
        await _httpClient.PutAsJsonAsync($"artistas", artista);
    }
    public async Task DeleteArtistaAsync(int id)
    {
        await _httpClient.DeleteAsync($"artistas/{id}");
    }

}

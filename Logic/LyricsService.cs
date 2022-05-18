using MyLyrics.Logic.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLyrics.Logic
{
    public class LyricsService : ILyricsService
    {
        private readonly IMyLyricsHttpClient _httpClient;
        private readonly string _apiKey = "0dc22d0dd3538c3523c16886d42f88e4";
        private readonly string _baseUrl = "https://api.vagalume.com.br/search.";

        public LyricsService(IMyLyricsHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SearchDocument>> SearchSongsAsync(string searchTerm)
        {
            var url = $"{_baseUrl}excerpt?apikey={_apiKey}&q={searchTerm}";
            var response = await GetAsync<SearchResult>(url);
            return response?.Response?.Docs;
        }

        public async Task<byte[]> GeneratePdfDocument(string songId)
        {
            var url = $"{_baseUrl}php?apikey={_apiKey}&musid={songId}";
            var response = await GetAsync<GetResult>(url);
            Song song = response?.Mus?.FirstOrDefault();
            if (song is null) return null;
            song.Band = response.Art?.Name;
            song.Text = TextHelper.RemoveRepeatedSections(song.Text);
            var pdfCreator = new PdfDocumentCreator();
            return pdfCreator.GenerateByteArrayForSong(song);
        }

        private async Task<T> GetAsync<T>(string url) where T : class
        {
            var result = await _httpClient.GetAsync(url);
            if (result.IsSuccessStatusCode is false) return null;
            var strResponse = result.Content;
            var response = JsonConvert.DeserializeObject<T>(strResponse);
            return response;
        }
    }
}

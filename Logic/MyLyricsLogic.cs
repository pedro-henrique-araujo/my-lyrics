using MyLyrics.Logic.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLyrics.Logic
{
    public class MyLyricsLogic
    {
        private readonly VagalumeApi _api;
        private readonly PdfDocumentCreator _pdfCreator;

        public MyLyricsLogic(string path = "")
        {
            _api = new VagalumeApi();
            _pdfCreator = new PdfDocumentCreator(path);
        }

        public async Task<List<SearchDocument>> SearchSongsAsync(string searchTerm)
        {
            SearchResult result = await _api.SearchAsync(searchTerm);
            return result?.Response?.Docs;
        }

        public async Task<Song> GetLyricsAsync(SearchDocument document)
        {
            Song output = await GetLyricsAsync(document.Id);
            output.Band = document.Band;
            output.Name = document.Title;
            return output;
        }

        public async Task<Song> GetLyricsAsync(string id)
        {

            GetResult result = await _api.GetById(id);

            Song output = result?.Mus.FirstOrDefault();
            output.Band = result.Art.Name;
            return output;
        }

        public string  GenerateDocument(Song song)
        {
            return _pdfCreator.CreateDocumentForSong(song);

        }
    }
}

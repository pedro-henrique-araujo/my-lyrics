using MyLyrics.Logic.Data;
using System.Threading.Tasks;

namespace MyLyrics.Logic
{
    public class VagalumeApi
    {
        private readonly HttpInterface _http;
        private readonly string _apiKey = "0dc22d0dd3538c3523c16886d42f88e4";

        public VagalumeApi()
        {
            _http = new HttpInterface();
        }

        public async Task<SearchResult> SearchAsync(string searchTerm)
        {
            string path = $"/search.excerpt?apikey={_apiKey}&q=" + searchTerm;
            SearchResult response = await _http.GetAsync<SearchResult>(path);
            return response;
        }

        public async Task<GetResult> GetById(string id)
        {
            string path = $"/search.php?apikey={_apiKey}&musid=" + id;
            GetResult response = await _http.GetAsync<GetResult>(path);
            return response;
        }
    }
}
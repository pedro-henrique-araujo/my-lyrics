using System.Net.Http;
using System.Threading.Tasks;

namespace MyLyrics.Logic
{
    public class MyLyricsHttpClient : IMyLyricsHttpClient
    {
        public async Task<IMyLyricsHttpResponseMessage> GetAsync(string requestUri)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestUri);
            var output = new MyLyricsHttpResponseMessage();
            output.IsSuccessStatusCode = response.IsSuccessStatusCode;
            output.Content = await response.Content.ReadAsStringAsync();
            return output;
            
        }
    }
}

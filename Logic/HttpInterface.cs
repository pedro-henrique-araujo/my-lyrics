using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyLyrics.Logic
{
    public class HttpInterface
    {
        static HttpClient _client = new HttpClient();
        private readonly string _dns = "https://api.vagalume.com.br";

        public HttpInterface()
        {

        }

        public async Task<T> GetAsync<T>(string path) where T : class
        {
            HttpResponseMessage result = await _client.GetAsync(_dns + path);
            if (!result.IsSuccessStatusCode) return null;
            string strResponse = await result.Content.ReadAsStringAsync();
            T response = JsonConvert.DeserializeObject<T>(strResponse);
            return response;
        }
    }
}
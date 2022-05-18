using System.Net.Http;
using System.Threading.Tasks;

namespace MyLyrics.Logic
{
    public interface IMyLyricsHttpClient
    {
        Task<IMyLyricsHttpResponseMessage> GetAsync(string requestUri);
    }
}

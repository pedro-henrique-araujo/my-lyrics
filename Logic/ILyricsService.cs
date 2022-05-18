using MyLyrics.Logic.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLyrics.Logic
{
    public interface ILyricsService
    {
        Task<byte[]> GeneratePdfDocument(string songId);
        Task<List<SearchDocument>> SearchSongsAsync(string searchTerm);
    }
}
using Microsoft.AspNetCore.Mvc;
using MyLyrics.Logic;
using MyLyrics.Logic.Data;
using System.Threading.Tasks;

namespace MyLyrics.WebApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        private ILyricsService _logic;

        public DefaultController(ILyricsService logic)
        {
            _logic = logic;
        }

        [Route("/search")]
        public async Task<ActionResult> Get(string searchTerm)
        {

            var searchResult = await _logic.SearchSongsAsync(searchTerm);
            return Ok(searchResult);
        }

        [Route("/generate")]
        public async Task<ActionResult> Generate(string songId)
        {
            var document = await _logic.GeneratePdfDocument(songId);
            return File(document, "application/pdf");
        }
    }
}

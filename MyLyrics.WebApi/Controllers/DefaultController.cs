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
        private MyLyricsLogic _logic;

        public DefaultController()
        {
            _logic = new MyLyricsLogic("wwwroot/pdf/");
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
            Song song = await _logic.GetLyricsAsync(songId);
            string documentName = _logic.GenerateDocument(song);
            return Ok(new 
            { 
                message = "Document generated successfully", 
                documentName = documentName 
            });
        }

    }
}

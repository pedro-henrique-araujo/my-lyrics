using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace MyLyrics.Logic.UnitTests
{
    [TestClass]
    public class LyricsServiceTests
    {
        private Mock<IMyLyricsHttpClient> _httpClientMock;
        private Mock<IMyLyricsHttpResponseMessage> _httpResponseMock;
        private LyricsService _lyricsService;

        [TestInitialize]
        public void TestInitialize()
        {
            _httpClientMock = new Mock<IMyLyricsHttpClient>();
            _httpResponseMock = new Mock<IMyLyricsHttpResponseMessage>();
            _lyricsService = new LyricsService(_httpClientMock.Object);
        }

        [TestMethod]
        [DataRow("{\"response\": { \"docs\": [{}]}}")]
        public async Task SearchSongsAsync_ReturnsListOfDocuments(string returnJson)
        {
            _httpResponseMock.Setup(r => r.IsSuccessStatusCode).Returns(true);
            _httpResponseMock.Setup(r => r.Content).Returns(returnJson);
            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(_httpResponseMock.Object);

            var result = await _lyricsService.SearchSongsAsync(It.IsAny<string>());
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task SearchSongsAsync_UnsuccessfulStatus_ReturnsNull()
        {
            _httpResponseMock.Setup(r => r.IsSuccessStatusCode).Returns(false);
            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                 .ReturnsAsync(_httpResponseMock.Object);
            var result = await _lyricsService.SearchSongsAsync(It.IsAny<string>());
            Assert.IsNull(result);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("{}")]
        [DataRow("{ \"response\": {}}")]
        public async Task SearchSongsAsync_UncompleteJson_ReturnsNull(string returnJson)
        {
            _httpResponseMock.Setup(r => r.IsSuccessStatusCode).Returns(true);
            _httpResponseMock.Setup(r => r.Content).Returns(returnJson);
            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(_httpResponseMock.Object);

            var result = await _lyricsService.SearchSongsAsync(It.IsAny<string>());
            Assert.IsNull(result);
        }

        [TestMethod]
        [DataRow(@"
        {
            ""mus"": [
                {
                    ""name"": ""Random Name"", 
                    ""band"": ""Random Band"", 
                    ""text"": ""Random Text""
                }
             ], 
            ""art"": {""name"": ""Random Artist""}
        }")]
        public async Task GeneratePdfDocument_ReturnsByteArray(string returnJson)
        {
            _httpResponseMock.Setup(r => r.IsSuccessStatusCode).Returns(true);
            _httpResponseMock.Setup(r => r.Content).Returns(returnJson);
            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(_httpResponseMock.Object);
            var result = await _lyricsService.GeneratePdfDocument(It.IsAny<string>());
            Assert.IsTrue(result?.Length > 0);
        }

        [TestMethod]
        public async Task GeneratePdfDocument_UnsuccessfulStatus_ReturnsNull()
        {
            _httpResponseMock.Setup(r => r.IsSuccessStatusCode).Returns(false);
            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(_httpResponseMock.Object);
            var result = await _lyricsService.GeneratePdfDocument(It.IsAny<string>());
            Assert.IsNull(result);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("{}")]
        [DataRow("{\"mus\": []}")]
        public async Task GeneratePdfDocument_UncompleteJson_ReturnsNull(string returnJson)
        {
            _httpResponseMock.Setup(r => r.IsSuccessStatusCode).Returns(true);
            _httpResponseMock.Setup(r => r.Content).Returns(returnJson);
            _httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(_httpResponseMock.Object);
            var result = await _lyricsService.GeneratePdfDocument(It.IsAny<string>());
            Assert.IsNull(result);
        }
    }
}

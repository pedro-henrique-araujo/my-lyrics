namespace MyLyrics.Logic
{
    public class MyLyricsHttpResponseMessage : IMyLyricsHttpResponseMessage
    {
        public bool IsSuccessStatusCode { get; set; }
        public string Content { get; set; }
    }
}

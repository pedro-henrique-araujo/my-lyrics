namespace MyLyrics.Logic
{
    public interface IMyLyricsHttpResponseMessage
    {
        bool IsSuccessStatusCode { get; set; }
        string Content { get; set; }
    }
}
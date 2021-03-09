namespace FfsScheduleParser.Infrastructure
{
    public interface IWebClientWrapper
    {
        string DownloadString(string address);
    }
}

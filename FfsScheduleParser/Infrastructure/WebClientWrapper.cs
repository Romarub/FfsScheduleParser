using System.Net;

namespace FfsScheduleParser.Infrastructure
{
    public class WebClientWrapper : IWebClientWrapper
    {
        public string DownloadString(string address)
        {
            using var webClient = new WebClient();
            return webClient.DownloadString(address);
        }
    }
}

using CheckerService.Models;
using System.Net.Http.Json;

namespace CheckerService
{
    internal class ConsularClient
    {
        private readonly string url = Constants.URL_MIDPASS;
        private readonly string applicationNumber;
        private readonly HttpClient client;

        public ConsularClient(string applicationNumber)
        {
            this.applicationNumber = applicationNumber;
            url += applicationNumber;
            client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            SetHeaders();
        }

        public async Task<ReadinessResponce> CheckUpdates()
        {
            return await client.GetFromJsonAsync<ReadinessResponce>(url);
        }

        private void SetHeaders()
        {
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("authority", "info.midpass.ru");
        }
    }
}

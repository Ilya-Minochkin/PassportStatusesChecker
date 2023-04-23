using CheckerService.Models;
using System.Net.Http.Json;

namespace CheckerService
{
    public class ConsularClient
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
        }

        public async Task<ReadinessResponce> CheckUpdates()
        {
            return await client.GetFromJsonAsync<ReadinessResponce>(url);
        }
    }
}

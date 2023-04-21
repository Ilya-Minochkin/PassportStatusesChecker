using CheckerService.Models;
using CheckerService.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            var options = new JsonSerializerOptions()
            {
                Converters = { new DrawningJsonConverter() }
            };
            var responce = await client.GetFromJsonAsync<ReadinessResponce>(url,options);
            return new ReadinessResponce();
        }
    }
}

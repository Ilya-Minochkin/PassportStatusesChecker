using System.Text.Json.Serialization;

namespace CheckerService.Models
{
    public class ReadinessResponce
    {
        public string Uid { get; set; }
        public DateTime ReceptionDate { get; set; }
        [JsonPropertyName("passportStatus")]
        public PublicStatus PublicStatus { get; set; }
        public InternalStatus InternalStatus { get; set; }
    }
}

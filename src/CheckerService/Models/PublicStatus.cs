using CheckerService.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace CheckerService.Models
{
    public class PublicStatus
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color Color { get; set; }
        public bool Subscription { get; set; }
    }
}

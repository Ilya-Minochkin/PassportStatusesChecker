using CheckerService.Abstractions;
using System.Text.Json.Serialization;

namespace CheckerService.Models
{
    public class ReadinessResponce : IUserMessage
    {
        public string Uid { get; set; }
        public DateTime ReceptionDate { get; set; }
        [JsonPropertyName("passportStatus")]
        public PublicStatus PublicStatus { get; set; }
        public InternalStatus InternalStatus { get; set; }

        public string ToMessage()
        {
            return $"Uid={Uid}\n"
                + $"Дата приема документов={ReceptionDate:dd.MM.yyyy}\n"
                + $"Публичный статус:\n{PublicStatus.ToMessage()}\n"
                + $"Внутренний статус:\n{InternalStatus.ToMessage()}";
        }
    }
}

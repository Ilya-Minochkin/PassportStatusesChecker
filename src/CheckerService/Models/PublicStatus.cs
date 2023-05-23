using CheckerService.Abstractions;
using CheckerService.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace CheckerService.Models
{
    public class PublicStatus : IUserMessage
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        [JsonConverter(typeof(ColorJsonConverter))]
        public Color Color { get; set; }
        public bool Subscription { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PublicStatus status &&
                   Id == status.Id &&
                   Description == status.Description &&
                   Name == status.Name &&
                   Color.Equals(status.Color) &&
                   Subscription == status.Subscription;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Description, Color, Subscription);
        }

        public string ToMessage()
        {
            return $"Название={Name}";
        }

        public override string? ToString()
        {
            return $"Публичный статус:\n" +
                $"Id={Id}\n" +
                $"Name={Name}\n" +
                $"Description={Description}\n" +
                $"Color={Color.Name}\n" +
                $"Subscription={Subscription}";
        }
    }
}

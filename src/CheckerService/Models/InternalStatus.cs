namespace CheckerService.Models
{
    public class InternalStatus
    {
        public string Name { get; set; }
        public int Percent { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is InternalStatus status &&
                   Name == status.Name &&
                   Percent == status.Percent;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Percent);
        }

        public override string? ToString()
        {
            return $"Внутренний статус:\n" +
                $"Name={Name}\n" +
                $"Percent={Percent}";
        }
    }
}

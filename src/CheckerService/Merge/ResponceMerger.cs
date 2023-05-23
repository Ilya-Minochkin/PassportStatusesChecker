using CheckerService.Abstractions;

namespace CheckerService.Merge
{
    internal static class ResponceMerger
    {
        public static List<Difference> Merge<T>(T? left, T right)
        {
            var result = new List<Difference>();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var leftValue = property.GetValue(left);
                var rightValue = property.GetValue(right);

                if (!leftValue?.Equals(rightValue) ?? true)
                    result.Add(new Difference(GetObjectString(leftValue), GetObjectString(rightValue)));
            }
            return result;
        }

        private static string? GetObjectString(object? obj)
        {
            if (obj is IUserMessage)
                return ((IUserMessage) obj).ToMessage();
            return obj?.ToString();
        }
    }

    public class Difference
    {
        public string? LeftValue { get; set; }
        public string RightValue { get; set; }

        public Difference(string? leftValue, string rightValue)
        {
            LeftValue = leftValue;
            RightValue = rightValue;
        }
    }
}

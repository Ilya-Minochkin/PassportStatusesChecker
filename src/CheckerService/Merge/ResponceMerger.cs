using CheckerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    result.Add(new Difference(leftValue?.ToString(), rightValue.ToString()));
            }
            return result;
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

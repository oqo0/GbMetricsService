using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class DotNetMetric
    {
        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}

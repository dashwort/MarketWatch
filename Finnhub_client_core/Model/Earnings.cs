using Newtonsoft.Json;
using System;

namespace Finnhub.ClientCore.Model
{
    public class Earnings
    {
        [JsonProperty("actual")]
        public decimal Actual { get; set; }

        [JsonProperty("estimate")]
        public decimal Estimate { get; set; }

        [JsonProperty("period")]
        public DateTime Period { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}

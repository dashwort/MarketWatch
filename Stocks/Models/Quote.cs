using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Stocks.Models
{
    public class Quote
    {
        [JsonPropertyName("Current Price")]
        public double Close { get; set; }

        [JsonPropertyName("High Price")]
        public double High { get; set; }

        [JsonPropertyName("Low Price")]
        public double Low { get; set; }

        [JsonPropertyName("Open price of day")]
        public double Open { get; set; }

        [JsonPropertyName("Previous Close")]
        public double Pc { get; set; }

        [JsonPropertyName("Unix Timemilliseconds")]
        public int UnixTime { get; set; }

        public long Latency { get; set; }

        public string Symbol { get; set; }

        public DateTime Time
        {
            get
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                return dtDateTime.AddSeconds(this.UnixTime).ToLocalTime();
            }
        }

        public String PreviousClose
        {
            get
            {
                return $"Closing Price {this.Pc}";
            }
        }


    }
}

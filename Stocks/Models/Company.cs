using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Stocks.Models
{
    public class Company
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("finnhubIndustry")]
        public string FinnhubIndustry { get; set; }

        [JsonPropertyName("ipo")]
        public string Ipo { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("marketCapitalization")]
        public long MarketCapitalization { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("shareOutstanding")]
        public double ShareOutstanding { get; set; }

        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

        [JsonPropertyName("weburl")]
        public string Weburl { get; set; }

        public Quote Quote { get; set; }

    }

}

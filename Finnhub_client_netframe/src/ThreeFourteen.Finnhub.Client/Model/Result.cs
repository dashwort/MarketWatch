using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ThreeFourteen.Finnhub.Client.Model
{
    public class Result
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("displaySymbol")]
        public string DisplaySymbol { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}

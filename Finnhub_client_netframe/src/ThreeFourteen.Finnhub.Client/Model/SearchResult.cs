using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ThreeFourteen.Finnhub.Client.Model
{ 
    public class SearchResult
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("result")]
        public List<Result> Result { get; set; }

        public long Latency { get; set; }
    }
}

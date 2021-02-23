using Newtonsoft.Json;
using System.Collections.Generic;

namespace Finnhub.ClientCore.Model
{
    public class SupportResistance
    {
        [JsonProperty("levels")]
        public List<double> Levels { get; set; }
    }
}

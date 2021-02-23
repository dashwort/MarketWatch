using Newtonsoft.Json;
using System.Drawing;

namespace ThreeFourteen.Finnhub.Client.Model
{
    public class Quote
    {
        [JsonProperty("c")]
        public decimal Current { get; set; }

        [JsonProperty("h")]
        public decimal High { get; set; }

        [JsonProperty("l")]
        public decimal Low { get; set; }

        [JsonProperty("o")]
        public decimal Open { get; set; }

        [JsonProperty("pc")]
        public decimal PreviousClose { get; set; }

        public string Symbol { get; set; }

        public string PreviousCloseFormatted { get { return $"Previous Close: ${PreviousClose}"; } }

        public decimal PercentChange
        {
            get 
            {
                if (this.Current == 0 || this.Open == 0)
                    return 0;

                return ((this.Current - this.Open) / Open) * 100;
            }
        }

        public SolidColorBrush ChangeIndicator
        {
            get
            {
                if (this.PercentChange == 0)
                    return ;

                if (PercentChange > 0)
                    return Color.Green;
                else
                    return Color.Red;
            }
        }

    }
}

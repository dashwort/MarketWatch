using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Media;

namespace Finnhub.ClientCore.Model
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

        public string PricePollFormatted 
        { 
            // TODO dynamic polling using socket
            get 
            { 
                    return $"Price: ${Current}";
            } 
        }

        public string PercentChangeFormatted 
        { 
            get 
            { 
                return $"({ String.Format("{0:0.00}", PercentChange)}%)"; 
            } 
        }

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
                    return Brushes.Black;

                if (PercentChange > 0)
                    return Brushes.Green;
                else
                    return Brushes.Red;
            }
        }

    }
}

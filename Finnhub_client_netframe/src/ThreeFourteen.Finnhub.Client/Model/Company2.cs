using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThreeFourteen.Finnhub.Client.Model
{
    public class Company2
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("finnhubIndustry")]
        public string FinnhubIndustry { get; set; }

        [JsonProperty("ipo")]
        public string Ipo { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("marketCapitalization")]
        public long MarketCapitalization { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("shareOutstanding")]
        public double ShareOutstanding { get; set; }

        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [JsonProperty("weburl")]
        public string Weburl { get; set; }

        public Quote Quote { get; set; }

        private string _logoLocation;

        public string CachedLogo
        {
            get 
            {
                if (string.IsNullOrEmpty(this.Logo))
                    return string.Empty;

                try
                {
                    var tempPath = Path.GetTempPath() + "\\MarketWatch";
                    var tempFilePath = tempPath + $"\\{this.Ticker}.png";

                    if (!Directory.Exists(tempPath))
                        Directory.CreateDirectory(tempPath);

                    if (File.Exists(tempFilePath))
                    {
                        _logoLocation = tempFilePath;
                    }
                    else
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(new Uri(Logo), tempFilePath);
                            _logoLocation = tempFilePath;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logoLocation = string.Empty;
                }
                
                return _logoLocation; 
            }
            set 
            { 
                _logoLocation = value; 
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Stocks.Models
{
    public class Quote
    {
        public double c { get; set; }
        public double h { get; set; }
        public double l { get; set; }
        public double o { get; set; }
        public double pc { get; set; }
        public int t { get; set; }
        public long Latency { get; set; }
        public string Symbol { get; set; }

        public DateTime Time
        {
            get
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                return dtDateTime.AddSeconds(this.t).ToLocalTime();
            }
        }

 
    }
}

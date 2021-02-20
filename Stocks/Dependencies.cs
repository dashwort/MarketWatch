using System;
using System.Collections.Generic;
using System.Text;

namespace Stocks
{
    public class Dependencies
    {
        public Dependencies()
        {
            Manager = new StockManager(stonks);
        }

        public StockManager Manager;
        public string[] stonks = new String[] { "TSLA", "IBM", "AAPL", "A" };

    }
}

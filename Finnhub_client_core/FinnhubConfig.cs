﻿using System;

namespace Finnhub.ClientCore
{
    public class FinnhubConfig
    {
        public Uri BaseUri { get; set; } = new Uri("https://finnhub.io/api/v1");

        public string ApiKey { get; set; }
    }
}

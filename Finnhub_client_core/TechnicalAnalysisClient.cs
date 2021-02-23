﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Finnhub.ClientCore.Model;
using Finnhub.ClientCore.Serialisation;

namespace Finnhub.ClientCore
{
    public class TechnicalAnalysisClient
    {
        private readonly FinnhubClient _finnhubClient;

        internal TechnicalAnalysisClient(FinnhubClient finnhubClient)
        {
            _finnhubClient = finnhubClient;
        }

        public Task<SupportResistance> GetSupportResistance(string symbol, Resolution resolution)
        {
            if (string.IsNullOrWhiteSpace(symbol)) throw new ArgumentException(nameof(symbol));

            return _finnhubClient.SendAsync<SupportResistance>("/scan/support-resistance", JsonDeserialiser.Default,
                new Field(FieldKeys.Symbol, symbol),
                new Field(FieldKeys.Resolution, resolution.GetFieldValue()));
        }
    }
}

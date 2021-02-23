using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Clients
{
    public class FinnhubClient
    {
        public FinnhubClient()
        {

        }
        public StockClient Stock { get; }

        public Task<string> GetRawDataAsync(string operation, params Field[] fields)
        {
            return SendAsync(operation, fields, content => content.ReadAsStringAsync());
        }

        internal Task<T> SendAsync<T>(string operation, IDeserialiser deserialiser, params Field[] fields)
        {
            return SendAsync(operation, fields, deserialiser.Deserialize<T>);
        }

        private async Task<T> SendAsync<T>(string operation, Field[] fields, Func<HttpContent, Task<T>> deserialise)
        {
            var parameters = CreateParameters(fields);

            var uri = new Uri(_config.BaseUri, $"/api/v1/{operation}?{parameters}");

            using (var responseMessage = await _httpClient.GetAsync(uri).ConfigureAwait(false))
            {
                if (!responseMessage.IsSuccessStatusCode)
                    throw new FinnhubException((int)responseMessage.StatusCode, responseMessage.ReasonPhrase);

                return await deserialise(responseMessage.Content).ConfigureAwait(false);
            }
        }

        private string CreateParameters(Field[] fields)
        {
            if (string.IsNullOrEmpty(_config.ApiKey)) throw new InvalidOperationException("ApiKey not set");

            var parameters = string.Join("&", fields.Select(f => $"{f.Key}={WebUtility.UrlEncode(f.Value)}"));
            parameters = string.IsNullOrEmpty(parameters) ?
                string.Empty : "&" + parameters;

            return $"token={_config.ApiKey}{parameters}";
        }
    }
}

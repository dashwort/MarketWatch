using System.Net.Http;
using System.Threading.Tasks;

namespace Stocks.Clients.Serialisation
{
    public interface IDeserialiser
    {
        Task<TResponse> Deserialize<TResponse>(HttpContent responseContent);
    }
}
using System.Net.Http;
using System.Threading.Tasks;

namespace Finnhub.ClientCore.Serialisation
{
    public interface IDeserialiser
    {
        Task<TResponse> Deserialize<TResponse>(HttpContent responseContent);
    }
}
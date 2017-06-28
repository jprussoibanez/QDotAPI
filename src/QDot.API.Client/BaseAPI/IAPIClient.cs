using QDot.API.Client.BaseAPI.Request;
using System.Threading.Tasks;

namespace QDot.API.Client.BaseAPI
{
    public interface IAPIClient
    {
        Task<T> Execute<T>(IAPIRequest<T> request) where T : class;
    }
}

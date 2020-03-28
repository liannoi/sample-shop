using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Application.Core.Helpers.Tools
{
    public interface IApiTools
    {
        Task<TEntity> FetchAsync<TEntity>(string uri) where TEntity : class, new();
        Task<TEntity> PostAsync<TEntity>(string uri, ByteArrayContent content) where TEntity : class, new();
        Task<TEntity> PostAsync<TEntity>(string uri, object value) where TEntity : class, new();

        Task<TEntity> RequestToken<TEntity>(string uri, IEnumerable<KeyValuePair<string, string>> value)
            where TEntity : class, new();
    }
}
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Application.Core.Helpers.Tools
{
    public interface IApiTools
    {
        Task<IEnumerable<TEntity>> FetchAsync<TEntity>(string uri) where TEntity : class, new();
        Task<TEntity> PostAsync<TEntity>(string uri, ByteArrayContent content) where TEntity : class, new();
        Task<TEntity> PostAsync<TEntity>(string uri, object value) where TEntity : class, new();
    }
}
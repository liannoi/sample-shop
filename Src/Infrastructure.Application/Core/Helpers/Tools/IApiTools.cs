using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Application.Core.Helpers.Tools
{
    public interface IApiTools
    {
        Task<IEnumerable<TEntity>> FetchAsync<TEntity>(string uri, CancellationToken token)
            where TEntity : class, new();

        Task<TEntity> PostAsync<TEntity>(string uri, ByteArrayContent content, CancellationToken token)
            where TEntity : class, new();
    }
}
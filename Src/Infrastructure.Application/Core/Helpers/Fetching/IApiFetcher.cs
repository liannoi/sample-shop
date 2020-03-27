using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Application.Core.Helpers.Fetching
{
    public interface IApiFetcher
    {
        Task<IEnumerable<TEntity>> FetchAsync<TEntity>(string uri) where TEntity : class, new();
    }
}
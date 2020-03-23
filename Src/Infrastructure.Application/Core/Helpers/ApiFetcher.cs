using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Application.Core.Helpers
{
    public class ApiFetcher : IApiFetcher
    {
        public async Task<IEnumerable<TEntity>> FetchAsync<TEntity>(string uri) where TEntity : class, new()
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            IEnumerable<TEntity> result = null;
            await new HttpClient()
                .GetAsync(uri)
                .ContinueWith(async taskResponse =>
                    result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(
                        await (await taskResponse).Content.ReadAsStringAsync()));
            return result;
        }
    }
}
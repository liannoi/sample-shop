using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Application.Core.Exceptions.Domain;
using Newtonsoft.Json;

namespace Infrastructure.Application.Core.Helpers.Fetching
{
    public class ApiFetcher : IApiFetcher
    {
        public async Task<IEnumerable<TEntity>> FetchAsync<TEntity>(string uri) where TEntity : class, new()
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            IEnumerable<TEntity> result = null;
            HttpResponseMessage awaitedTaskResponse = null;
            await new HttpClient()
                .GetAsync(uri)
                .ContinueWith(async taskResponse =>
                {
                    awaitedTaskResponse = await taskResponse;
                    result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(
                        await (await taskResponse).Content.ReadAsStringAsync());
                });

            return result ?? throw new BadStatusCodeException(awaitedTaskResponse.ReasonPhrase);
        }
    }
}
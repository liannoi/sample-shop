using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Core.Exceptions.Domain;
using Newtonsoft.Json;

namespace Infrastructure.Application.Core.Helpers.Tools
{
    public class ApiTools : IApiTools
    {
        public async Task<IEnumerable<TEntity>> FetchAsync<TEntity>(string uri, CancellationToken token)
            where TEntity : class, new()
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            try
            {
                return await ContinueWithDeserializeAsync<List<TEntity>>(new HttpClient().GetAsync(uri, token), token);
            }
            catch (BadStatusCodeException e)
            {
                throw new UnauthorizedAccessException(e.Message);
            }
        }

        public async Task<TEntity> PostAsync<TEntity>(string uri, ByteArrayContent content, CancellationToken token)
            where TEntity : class, new()
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (content == null) throw new ArgumentNullException(nameof(content));

            try
            {
                return await ContinueWithDeserializeAsync<TEntity>(new HttpClient().PostAsync(uri, content, token),
                    token);
            }
            catch (BadStatusCodeException e)
            {
                throw new BadRequestException(e.Message);
            }
        }

        #region Helpers

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once MemberCanBeMadeStatic.Global
        protected async Task<TResult> ContinueWithDeserializeAsync<TResult>(Task<HttpResponseMessage> task,
            CancellationToken token) where TResult : class, new()
        {
            TResult result = null;
            await task.ContinueWith(async taskResponse =>
            {
                token.ThrowIfCancellationRequested();
                if ((await taskResponse).IsSuccessStatusCode)
                    result = JsonConvert.DeserializeObject<TResult>(
                        await (await taskResponse).Content.ReadAsStringAsync());
            }, token);
            return result;
        }

        #endregion
    }
}
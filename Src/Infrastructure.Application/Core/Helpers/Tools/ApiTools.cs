using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Infrastructure.Application.Core.Helpers.Tools
{
    public class ApiTools : IApiTools
    {
        public async Task<IEnumerable<TEntity>> FetchAsync<TEntity>(string uri)
            where TEntity : class, new()
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            return await ContinueWithDeserializeAsync<List<TEntity>>(new HttpClient().GetAsync(uri));
        }

        public async Task<TEntity> PostAsync<TEntity>(string uri, ByteArrayContent content) where TEntity : class, new()
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (content == null) throw new ArgumentNullException(nameof(content));

            return await ContinueWithDeserializeAsync<TEntity>(new HttpClient().PostAsync(uri, content));
        }

        public async Task<TEntity> PostAsync<TEntity>(string uri, object value) where TEntity : class, new()
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await ContinueWithDeserializeAsync<TEntity>(new HttpClient().PostAsync(uri, byteContent));
        }

        #region Helpers

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once MemberCanBeMadeStatic.Global
        protected async Task<TEntity> ContinueWithDeserializeAsync<TEntity>(Task<HttpResponseMessage> task)
            where TEntity : class, new()
        {
            TEntity result = null;
            await task.ContinueWith(async taskResponse =>
                result = JsonConvert.DeserializeObject<TEntity>(await (await taskResponse).Content
                    .ReadAsStringAsync()));
            return result;
        }

        #endregion
    }
}
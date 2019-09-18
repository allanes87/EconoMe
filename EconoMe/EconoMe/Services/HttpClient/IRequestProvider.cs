using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EconoMe.Services.HttpClient
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "", CancellationToken? cancellationToken = null);
        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "", CancellationToken? cancellationToken = null);

        Task DeleteAsync(string uri, string token = "");
    }
}

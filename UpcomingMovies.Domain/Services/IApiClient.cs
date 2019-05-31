using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Domain.Services
{
    public interface IApiClient<TResult> : IDisposable
    {
        Task<TResult> GetAsync(string apiRoute, Action<TResult> callback = null, params Parameter[] parameters);
        IApiClient<TResult> UseSufix(string urlSufix);
        Task<TResult> PostResultAsync(string apiRoute, object body = null, Action<TResult> callback = null);
        Task<TResult> PostAsync(string apiRoute, object body = null, Action<TResult> callback = null);
    }
}

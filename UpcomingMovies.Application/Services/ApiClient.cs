using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UpcomingMovies.Domain.Services;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Application.Services
{
    public abstract class ApiClient<TResult> : IApiClient<TResult>
    {
        private HttpClient _restClient;
        private string _apiUrlBase = "https://api.themoviedb.org/3/movie";
        protected const string _APIKEY = "1f54bd990f1cdfb230adb312546d765d";

        public async Task<TResult> GetAsync(string apiRoute, Action<TResult> callback = null, params Parameter []parameters)
        {
            try
            {
                var json = await GetAsync(apiRoute, parameters);
                var data = JsonConvert.DeserializeObject<TResult>(json, GetConverter());

                callback?.Invoke(data);

                return data;
            }
            catch
            {
                return default(TResult);
            }
        }

        private async Task<string> GetAsync(string apiRoute, params Parameter[] parameters)
        {
            var url = _apiUrlBase + "/" + apiRoute;

            if (parameters != null)
            {
                url += "?";
                for (int i = 0; i < parameters.Length; i++)
                {
                    url += parameters[i].Name + "=" + parameters[i].Value + "&";
                }
            }

            url += "api_key=" + _APIKEY;

            _restClient = new HttpClient();
            _restClient.BaseAddress = new Uri(url);

            ClearReponseHeaders();
            AddReponseHeaders();

            var response = await _restClient.GetAsync(_restClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            var data = response.Content.ReadAsStringAsync().Result;

            return data;
        }

        public async Task<TResult> PostResultAsync(string apiRoute, object body = null, Action<TResult> callback = null)
        {
            try
            {
                var json = await PostAsync(apiRoute, body);
                var data = JsonConvert.DeserializeObject<TResult>(json, GetConverter());

                callback?.Invoke(data);

                return data;
            }
            catch
            {
                return default(TResult);
            }
        }

        public async Task<TResult> PostAsync(string apiRoute, object body = null, Action<TResult> callback = null)
        {
            try
            {
                var data = await PostAsync(apiRoute, body);
                var result = JsonConvert.DeserializeObject<TResult>(data, GetConverter());

                callback?.Invoke(result);

                return result;
            }
            catch
            {
                return default(TResult);
            }
        }

        private async Task<string> PostAsync(string apiRoute, object body)
        {
            var url = _apiUrlBase + "/" + apiRoute;

            _restClient = new HttpClient();
            _restClient.BaseAddress = new Uri(url);

            ClearReponseHeaders();
            AddReponseHeaders();

            var bodySerialize = JsonConvert.SerializeObject(body);
            StringContent content = new StringContent(bodySerialize, Encoding.UTF8, "application/json");

            var response = await _restClient.PostAsync(_restClient.BaseAddress, content);
            response.EnsureSuccessStatusCode();
            var data = response.Content.ReadAsStringAsync().Result;

            return data;
        }

        private IsoDateTimeConverter GetConverter()
        {
            return new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" };
        }

        public IApiClient<TResult> UseSufix(string urlSufix)
        {
            if (!_apiUrlBase.EndsWith(urlSufix))
            {
                _apiUrlBase = _apiUrlBase + "/" + urlSufix;
            }

            return this;
        }

        private void AddReponseHeaders()
        {

        }

        private void ClearReponseHeaders()
        {
            _restClient.DefaultRequestHeaders.Clear();
        }

        public void Dispose()
        {

        }

    }
}

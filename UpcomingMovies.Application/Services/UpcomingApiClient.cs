using System.Threading.Tasks;
using UpcomingMovies.Domain.Services;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Application.Services
{
    public class UpcomingApiClient : ApiClient<UpcomigResult>, IUpcomingApiClient
    {
        public async Task<UpcomigResult> GetUpcomingPage(string language, int page)
        {
            Parameter[] parameters =
                {
                    new Parameter { Name = "language", Value = language },
                    new Parameter { Name = "page", Value = page.ToString() },
                };

            return await GetAsync("upcoming", null, parameters);
        }
    }
}

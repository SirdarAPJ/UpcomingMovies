using System.Threading.Tasks;
using UpcomingMovies.Domain.Services;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Application.Services
{
    public class UpcomingApiClient : ApiClient<Upcoming>, IUpcomingApiClient
    {
        public async Task<Upcoming> GetUpcomingPage(string language, int page)
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

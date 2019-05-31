using System.Threading.Tasks;
using UpcomingMovies.Domain.Services;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Application.Services
{
    public class MovieDetailApiClient : ApiClient<MovieDetail>, IMovieDetailApiClient
    {
        public async Task<MovieDetail> GetMovieDetail(int id, string language)
        {
            Parameter parameter = new Parameter
            {
                Name = "language",
                Value = language
            };
            return await GetAsync(id.ToString(), null, parameter);
        }
    }
}

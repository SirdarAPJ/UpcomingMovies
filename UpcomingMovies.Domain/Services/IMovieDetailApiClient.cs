using System.Threading.Tasks;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Domain.Services
{
    public interface IMovieDetailApiClient
    {
        Task<MovieDetail> GetMovieDetail(int id, string language);
    }
}

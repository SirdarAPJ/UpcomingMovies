using System.Threading.Tasks;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Domain.Services
{
    public interface IUpcomingApiClient
    {
        Task<Upcoming> GetUpcomingPage(string language, int page);
    }
}

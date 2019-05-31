using System.Threading.Tasks;
using UpcomingMovies.Infrastucture.DataTransfer;

namespace UpcomingMovies.Domain.Services
{
    public interface IUpcomingApiClient
    {
        Task<UpcomigResult> GetUpcomingPage(string language, int page);
    }
}

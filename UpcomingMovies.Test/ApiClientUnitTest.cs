using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using UpcomingMovies.Application.Services;
using UpcomingMovies.Domain.Services;
using UpcomingMovies.Infrastucture.Constants;
using System.Linq;

namespace UpcomingMovies.Test
{
    [TestClass]
    public class ApiClientUnitTest
    {
        IUnityContainer _container = new UnityContainer();

        public ApiClientUnitTest()
        {           
            _container.RegisterType<IUpcomingApiClient, UpcomingApiClient>();
            _container.RegisterType<IMovieDetailApiClient, MovieDetailApiClient>();
        }

        [TestMethod]
        public void GetUpcomigFirstPage ()
        {
            var upcomingApiClient = _container.Resolve<IUpcomingApiClient>();
            var upcoming = upcomingApiClient.GetUpcomingPage(Localization.PT_BR, 1).Result;

            Assert.IsNotNull(upcoming, "Returned some data");
            Assert.AreEqual(1, upcoming.page, "Returned first page");
        }

        [TestMethod]
        public void GetFirstMovieDetail()
        {
            try
            {
                var upcomingApiClient = _container.Resolve<IUpcomingApiClient>();
                var upcoming = upcomingApiClient.GetUpcomingPage(Localization.PT_BR, 1).Result;

                var movieDetailApiClient = _container.Resolve<IMovieDetailApiClient>();
                var movieDetail = movieDetailApiClient.GetMovieDetail(upcoming.results.FirstOrDefault().id, Localization.PT_BR).Result;

                Assert.IsNotNull(movieDetail, "Returned some detail of first movie");
            }
            catch (System.Exception)
            {
                Assert.Fail("First movie detail failed");
            }

        }

    }
}

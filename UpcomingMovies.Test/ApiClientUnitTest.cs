using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using UpcomingMovies.Application.Services;
using UpcomingMovies.Domain.Services;
using UpcomingMovies.Infrastucture.Constants;

namespace UpcomingMovies.Test
{
    [TestClass]
    public class ApiClientUnitTest
    {
        IUnityContainer _container = new UnityContainer();

        public ApiClientUnitTest()
        {           
            _container.RegisterType<IUpcomingApiClient, UpcomingApiClient>();            
        }

        [TestMethod]
        public async void GetUpcomigFirstPage ()
        {
            var upcomingApiClient = _container.Resolve<IUpcomingApiClient>();
            var upcoming = await upcomingApiClient.GetUpcomingPage(Localization.PT_BR, 1);

            Assert.IsNotNull(upcoming);

        }
    }
}

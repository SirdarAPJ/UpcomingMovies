using Prism;
using Prism.Ioc;
using UpcomingMovies.App.AppResources;
using UpcomingMovies.App.ViewModels;
using UpcomingMovies.App.Views;
using UpcomingMovies.Application.Services;
using UpcomingMovies.Domain.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UpcomingMovies.App
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null)
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                // determine the correct, supported .NET culture
                var cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Localization.Resource.Culture = cultureInfo; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(cultureInfo); // set the Thread for locale-aware methods
            }
        }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            //Services
            containerRegistry.Register<IUpcomingApiClient, UpcomingApiClient>();
            containerRegistry.Register<IMovieDetailApiClient, MovieDetailApiClient>();
        }
    }
}

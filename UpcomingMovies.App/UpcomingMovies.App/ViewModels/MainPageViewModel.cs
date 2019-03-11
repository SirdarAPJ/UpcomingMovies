using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UpcomingMovies.App.Views;
using UpcomingMovies.Domain.Services;
using UpcomingMovies.Infrastucture.DataTransfer;
using Xamarin.Forms;

namespace UpcomingMovies.App.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<Upcoming> _wholePageMovies;
        private ObservableCollection<Upcoming> _upcomingMovies;
        private ICommand _searchCommand;
        private ICommand _previousCommand;
        private ICommand _nextCommand;
        private ICommand _itemSelectedCommand;

        private int _currentPage = 1;
        private int _totalPages;
        private string _pagination;

        private readonly IUpcomingApiClient _upcomingApiClient;

        public int CurrentPage
        {
            get => _currentPage;
            set { SetProperty(ref _currentPage, value); }
        }
        public string Pagination
        {
            get { return _pagination; }
            set { SetProperty(ref _pagination, value); }
        }

        public ObservableCollection<Upcoming> UpcomingMovies
        {
            get { return _upcomingMovies; }
            set { SetProperty(ref _upcomingMovies, value); }
        }      

        public MainPageViewModel(INavigationService navigationService,
                                 IUpcomingApiClient upcomingApiClient)
            : base(navigationService)
        {
            _upcomingApiClient = upcomingApiClient;
        }

        private async Task LoadListItems()
        {
            var upcoming = await _upcomingApiClient.GetUpcomingPage(CultureInfo.CurrentCulture.Name, _currentPage);

            if (upcoming != null)
            {
                _totalPages = upcoming.total_pages;
                _wholePageMovies = new ObservableCollection<Upcoming>(upcoming.results);

                Pagination = _currentPage + "/" + _totalPages;

                UpcomingMovies = _wholePageMovies;
            }
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            await LoadListItems();

            base.OnNavigatingTo(parameters);
        }

        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command<string>((text) =>
        {
            if (text?.Length >= 1)
            {
                UpcomingMovies = new ObservableCollection<Upcoming>(_wholePageMovies.Where(w => w.title.ToUpper().Contains(text.ToUpper())));
            }
            else
            {
                UpcomingMovies = _wholePageMovies;
            }

        }));

        public ICommand PreviousCommand => _previousCommand ?? (_previousCommand = new Command<string>(async (text) =>
        {
            if (_currentPage > 1)
            {
                CurrentPage--;
                await LoadListItems();
            }

        }));

        public ICommand NextCommand => _nextCommand ?? (_nextCommand = new Command<string>(async (text) =>
        {
            if (_currentPage < _totalPages)
            {
                CurrentPage++;
                await LoadListItems();
            }

        }));

        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new Command<ItemTappedEventArgs>(async (parameter) =>
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("movieId", ((Upcoming)parameter.Item).id);

            await NavigationService.NavigateAsync(nameof(MovieDetailPage), navParameters);

        }));


    }
}

using Prism.Navigation;
using System;
using System.Linq;
using System.Globalization;
using UpcomingMovies.Domain.Services;

namespace UpcomingMovies.App.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {

        private readonly IMovieDetailApiClient _movieDetailApiClient;
        private string _uriBackdropImage;
        private string _overview;
        private string _genres;

        public string UriBackdropImage
        {
            get { return _uriBackdropImage; }
            set { SetProperty(ref _uriBackdropImage, value); }
        }
        public string Overview
        {
            get { return _overview; }
            set { SetProperty(ref _overview, value); }
        }

        public string Genres
        {
            get { return _genres; }
            set { SetProperty(ref _genres, value); }
        }

        public MovieDetailViewModel(INavigationService navigationService,
                                    IMovieDetailApiClient movieDetailApiClient) : 
            base(navigationService)
        {
            _movieDetailApiClient = movieDetailApiClient;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var moveDetails = await _movieDetailApiClient.GetMovieDetail(Convert.ToInt32(parameters["movieId"].ToString()), CultureInfo.CurrentCulture.Name);

            if (moveDetails != null)
            {
                UriBackdropImage = "https://image.tmdb.org/t/p/w780" + moveDetails.backdrop_path;
                Overview = moveDetails.overview;
                Genres = string.Join(", ", moveDetails.genres.Select(s => s.name));
            }

        }
    }
}

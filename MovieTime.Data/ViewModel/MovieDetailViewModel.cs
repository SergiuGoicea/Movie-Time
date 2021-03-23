using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTime.Data.Interfaces;
using MovieTime.Data.Model;

namespace MovieTime.Data.ViewModel
{
    public class MovieDetailViewModel : BaseViewModel
    {
        #region Fields
        private readonly IMovieService _movieService;
        #endregion

        private List<string> _genres = new List<string>();
        public List<string> GenreListForMovie { get => _genres; private set => SetProperty(ref _genres, value); }

        private List<Movie> _relatedMovie;
        public List<Movie> RelatedMovie { get => _relatedMovie; private set => SetProperty(ref _relatedMovie, value); }

        private Movie _movie;
        public Movie Movie { get => _movie; private set => SetProperty(ref _movie, value); }

        public int MovieId { get; set; }

        
        

        public MovieDetailViewModel(IConnectivityService connectivityService, IMovieService movieService) : base(connectivityService)
        {
            _movieService = movieService;
        }

        public async void GetMovie()
        {
            IsBusy = true;
            Movie = await _movieService.GetMovieForMovieDetail(MovieId);
            GenreListForMovie = _movieService.GetGenreListForMovie(_movie);
            RelatedMovie = await _movieService.GetRelatedMovieForMovieDetail(MovieId);
            IsBusy = false;
        }

        public string GetImageUrl(string posterPath)
        {
            return _movieService.GetImageUrl(posterPath);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
             GetMovie();
        }

    }
}

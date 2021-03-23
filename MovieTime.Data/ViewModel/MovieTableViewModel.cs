using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MovieTime.Data.Interfaces;
using MovieTime.Data.Model;

namespace MovieTime.Data.ViewModel
{
    public class MovieTableViewModel : BaseViewModel
    {
        #region Fields
        private readonly IMovieService _movieService;
        #endregion

        private List<Movie> _movies;
        public List<Movie> MovieList { get => _movies; private set => SetProperty(ref _movies, value); }


        private GenreList _genres;
        public GenreList GenreList { get => _genres; private set => SetProperty(ref _genres, value); }

        private List<Movie> MoviesByPopularityHolder { get; set; }

        private CancellationTokenSource ts = new CancellationTokenSource();

        private string _searchTerm;

        public string GetImageUrl(string posterPath)
        {
            return _movieService.GetImageUrl(posterPath);
        }

        public List<string> GenreName(List<Movie> movies, int row, GenreList genreList)
        {
            return _movieService.GenreName(movies, row, genreList);
        }

        public MovieTableViewModel(IConnectivityService connectivityService, IMovieService movieService) : base(connectivityService)
        {
            _movieService = movieService;
        }

        public async Task GetPopularMoviesAndGenres()
        {
            MoviesByPopularityHolder = await _movieService.GetPopularMoviesAsync();
            //var movie = await movieService.GetMovieForMovieDetail(793723);
            GenreList = await _movieService.GetGenresAsync();
            MovieList = MoviesByPopularityHolder;
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            await GetPopularMoviesAndGenres();
        }
        public string SearchTerm { get => _searchTerm; set => SetProperty(ref _searchTerm, value, onChanged: OnSearchTermChanged); }

        private async void OnSearchTermChanged()
        {
            if (!IsConnectedToInternet)
                return;

            try
            {
                ts?.Cancel();
                ts = new CancellationTokenSource();

                if (string.IsNullOrWhiteSpace(SearchTerm))
                {
                    MovieList = MoviesByPopularityHolder;
                }
                else
                {
                    IsBusy = true;
                    Interlocked.Exchange(ref this.ts, new CancellationTokenSource()).Cancel();
                    await Task.Delay(TimeSpan.FromMilliseconds(500), this.ts.Token) // if no keystroke occurs, carry on after 500ms
                         .ContinueWith(
                             async delegate { MovieList = await _movieService.SearchMovies(ts.Token, SearchTerm); }, // Pass the changed text to the PerformSearch function
                             CancellationToken.None,
                             TaskContinuationOptions.OnlyOnRanToCompletion,
                             TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
            catch (Exception ex)
            {
                bool bp = true;
            }


            IsBusy = false;
        }
        protected override void InternalOnConnectivityChanged(bool isConnected)
        {
            base.InternalOnConnectivityChanged(isConnected);
            if (isConnected)
                Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => OnSearchTermChanged());
        }


    }
}

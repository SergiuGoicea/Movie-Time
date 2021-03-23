using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MovieTime.Data.Interfaces;
using MovieTime.Data.Model;
using Refit;

namespace MovieTime.Data.Services
{
    public class MovieService : IMovieService
    {
        static IRefitMovieTimeService _manager = RestService.For<IRefitMovieTimeService>("https://api.themoviedb.org");
        const string key = "e9e4e4b144f5468cd2b2aa222f12f699";

        private List<Movie> moviesByPopularity;
        private List<Movie> moviesByQuery;
        private Movie movie;
        private List<Movie> relatedMovie;
        private string genreListAsString;
        private List<string> genreListForMovie;

        public async Task<List<Movie>> SearchMovies(CancellationToken token, string text)
        {
            var responseForQuery = await _manager.GetMoviesByQuery(token, key, text);
            moviesByQuery = new List<Movie>();
            if (responseForQuery != null)
            {
                moviesByQuery.Clear();
                moviesByQuery.AddRange(
                    new List<Movie>(responseForQuery.Results)
                );
            }

            return moviesByQuery;
        }


        public async Task<List<Movie>> GetPopularMoviesAsync()
        {
            var response = await _manager.GetPopular(key);
            if (response != null)
            {
                moviesByPopularity = new List<Movie>(response.Results);
            }
            return moviesByPopularity;
        }

        public async Task<GenreList> GetGenresAsync()
        {
            return await _manager.GetMovieGenre(key);
        }

        public List<string> GenreName(List<Movie> movies, int row, GenreList genreList)
        {
            var moviesGenreIds = movies[row].Genre_Ids;
            var genreName = (from genreId in moviesGenreIds
                             select genreList.genres
                                                     .Where(g => g.Id == genreId)
                                                     .Select(g => g.Name)
                                                     .SingleOrDefault()).ToList();
            genreName.Sort();

            return genreName;
        }

        public string GetImageUrl(string posterPath)
        {
            return posterPath != null ? ("https://image.tmdb.org/t/p/w500" + posterPath) : "https://in.bmscdn.com/iedb/movies/images/website/poster/large/dragon-warriors--tamil--et00017721-24-03-2017-18-45-53.jpg";
        }


        //Methods for DetailView
        public async Task<Movie> GetMovieForMovieDetail(int MovieId)
        {
            var movie = await _manager.GetMovieById(key, MovieId);
            return movie;
        }

        public async Task<List<Movie>> GetRelatedMovieForMovieDetail(int MovieId)
        {
            var response = await _manager.GetRelatedMovies(key, MovieId);
            if (response != null)
            {
                relatedMovie = new List<Movie>(response.Results);
            }
            return relatedMovie;
        }

        public List<string> GetGenreListForMovie(Movie movie)
        {
            foreach (var genre in movie.Genres)
            {
                genreListAsString += genre.Name;
                genreListAsString += ",";
            }
            genreListForMovie = new List<string>(genreListAsString.Split(','));
            genreListForMovie.RemoveAt(genreListForMovie.Count() - 1);
            genreListForMovie.Sort();
            genreListAsString = null;

            return genreListForMovie;
        }
    }
}

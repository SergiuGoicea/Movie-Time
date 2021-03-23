using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MovieTime.Data.Model;

namespace MovieTime.Data.Interfaces
{

    public interface IMovieService
    {
        Task<List<Movie>> GetPopularMoviesAsync();
        Task<GenreList> GetGenresAsync();
        List<string> GenreName(List<Movie> movies, int row, GenreList genreList);
        Task<List<Movie>> SearchMovies(CancellationToken token, string text);
        string GetImageUrl(string posterPath);

        Task<Movie> GetMovieForMovieDetail(int MovieId);
        Task<List<Movie>> GetRelatedMovieForMovieDetail(int MovieId);
        List<string> GetGenreListForMovie(Movie movie);
    }

}

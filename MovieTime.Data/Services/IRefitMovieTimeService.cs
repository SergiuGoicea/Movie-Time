using System;
using System.Threading.Tasks;
using Refit;
using MovieTime.Data.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace MovieTime.Data.Services
{

    public interface IRefitMovieTimeService
    {
        [Get("/3/search/movie?api_key={api_key}&query={name}")]
        Task<MovieList> GetMoviesByQuery(CancellationToken token, string api_key, string name);

        [Get("/3/genre/movie/list?api_key={api_key}&language=en-US")]
        Task<GenreList> GetMovieGenre(string api_key);

        [Get("/3/movie/popular?api_key={api_key}&language=en-US&page=1")]
        Task<MovieList> GetPopular(string api_key);

        [Get("/3/discover/movie?api_key={api_key}")]
        Task<MovieList> GetMovies(CancellationToken token, string api_key);

        [Get("/3/movie/{movie_id}?api_key={api_key}")]
        Task<Movie> GetMovieById(string api_key, int movie_id);

        [Get("/3/movie/{movie_id}/similar?api_key={api_key}&language=en-US&page=1")]
        Task<MovieList> GetRelatedMovies(string api_key, int movie_id);

        [Post("/3/authentication/session/new?api_key={api_key}")]
        Task CreateSesion(string api_key);

    }

    public interface IAuthService
    {
        [Get("/3/authentication/token/new?api_key={api_key}")]
        Task<string> GetTokenAsync(string api_key);
    }
}

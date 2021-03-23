using System;
using System.Threading.Tasks;
using MovieTime.Data.Model;
using MovieTime.Data.Services;
using Refit;

namespace MovieTime.Test
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            const string key = "e9e4e4b144f5468cd2b2aa222f12f699";



            IRefitMovieTimeService manager = RestService.For<IRefitMovieTimeService>("https://api.themoviedb.org");

            //IRefitMovieTimeService imageManager = RestService.For<IRefitMovieTimeService>("https://image.tmdb.org/t/p/w500");

            //afisare genuri filme
            //var movieCollection = await manager.GetMovieGenre(key);


            //foreach (var movie in movieCollection.Genres)
            //{
            //    Console.WriteLine(movie.Name);
            //}

            //afisare titluri filme dupa nume
            //var movieCollection = await manager.GetMovie(key,"iron man");

            //foreach (var movie in movieCollection.Results)
            //{
            //    Console.WriteLine(movie.Title);
            //}


            //afisare top filme populare
            //var movieCollection = await manager.GetPopular(key);

            //foreach (var movie in movieCollection.Results)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            //afisare top filme populare
            //var movieCollection = await manager.GetMovies(key);
            //foreach (var movie in movieCollection.Results)
            //{
            //    Console.WriteLine(movie.Title);
            //}

            var movieById = await manager.GetMovieById(key, "1726");

            //var image = await imageManager.GetImage(movieById.poster_path.Replace("/", ""));

            string imageUrl = "https://image.tmdb.org/t/p/w500" + movieById.Poster_path;

            Console.WriteLine(imageUrl);
        }
    }
}

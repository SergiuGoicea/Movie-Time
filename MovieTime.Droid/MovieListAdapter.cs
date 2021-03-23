using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using MovieTime.Data.Model;
using MovieTime.Data.Services;
using MovieTime.Data.ViewModel;
using Square.Picasso;

namespace MovieTime.Droid
{
    public class MovieListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;

        #region Fields

        private List<Movie> _movies;
        private GenreList _genres;
        private MovieTableViewModel _viewModel;

        #endregion

        #region Constructors

        public MovieListAdapter(List<Movie> movies, GenreList genres, MovieTableViewModel viewModel)
        {
            _movies = movies;
            _genres = genres;
            _viewModel = viewModel;
        }

        #endregion
        public override int ItemCount => _movies.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MovieListViewHolder viewHolder = holder as MovieListViewHolder;
            var genresForMovie = _viewModel.GenreName(_movies, position, _genres);


            Picasso.Get().Load(_viewModel.GetImageUrl(_movies[position].Poster_path)).Into(viewHolder.MoviePoster);

            viewHolder.MovieTitle.Text = _movies[position].Title;
            viewHolder.MovieGenres.Text = string.Join(", ", genresForMovie);

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);
            var view = inflater.Inflate(Resource.Layout.MovieList, parent, false);
            return new MovieListViewHolder(view, OnViewItemClick);
        }

        void OnViewItemClick(int position)
        {
            ItemClick?.Invoke(this, _movies[position].Id);

        }
    }
}

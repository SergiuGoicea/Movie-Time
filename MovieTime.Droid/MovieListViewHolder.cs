using System;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;

namespace MovieTime.Droid
{
    public class MovieListViewHolder : RecyclerView.ViewHolder
    {
        #region Fields

        Action<int> _action;

        #endregion

        #region Constructors

        public MovieListViewHolder(View view, Action<int> action) : base(view)
        {
            MoviePoster = view.FindViewById<ImageView>(Resource.Id.moviePoster);
            MovieTitle = view.FindViewById<TextView>(Resource.Id.movieTitle);
            MovieGenres = view.FindViewById<TextView>(Resource.Id.movieGenres);

            if (action != null)
            {
                view.Click += View_Click;
                _action = action;
            }
        }

        #endregion

        private void View_Click(object sender, EventArgs e)
        {
            if (AdapterPosition != RecyclerView.NoPosition)
                _action.Invoke(AdapterPosition);
        }

        public ImageView MoviePoster { get; private set; }
        public TextView MovieTitle { get; private set; }
        public TextView MovieGenres { get; private set; }
    }
}

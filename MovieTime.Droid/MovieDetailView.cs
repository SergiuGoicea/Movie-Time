using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.ConstraintLayout.Widget;
using MovieTime.Data.Model;
using MovieTime.Data.ViewModel;
using Square.Picasso;

namespace MovieTime.Droid
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class MovieDetailView : AppCompatActivity
    {
        #region Fields

        private ImageView _movieImage, _firstRelated, _secondRelated, _thirdRelated;
        private TextView _name, _tagline, _genres, _overview, _budget, _revenue, _relatedMoviesBox, _firstRelatedName, _secondRelatedName, _thirdRelatedName;
        private ConstraintLayout _budgetLayout, _revenueLayout;
        public MovieDetailViewModel _detailViewModel;
        bool movie = false, related = false;

        #endregion

        #region Constructors

        public MovieDetailView()
        {
            _detailViewModel = ViewModelLocator.Instance.GetInstance<MovieDetailViewModel>();
        }

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MovieDetail);
            var movieId = Intent.GetIntExtra("MovieId", -1);

            _detailViewModel.MovieId = movieId;

            _detailViewModel.GetMovie();

            _detailViewModel.PropertyChanged += Service_PropertyChanged;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _detailViewModel.PropertyChanged -= Service_PropertyChanged;
        }



        private void Service_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MovieDetailViewModel.Movie))
            {
                movie = true;
                if (related)
                    PopulateMovieDetails();
            }
            if (e.PropertyName == nameof(MovieDetailViewModel.RelatedMovie))
            {
                related = true;
                if (movie)
                    PopulateMovieDetails();
            }

        }



        #region Methods
        private void PopulateMovieDetails()
        {

            GenerateFields();

            Picasso.Get().Load(_detailViewModel.GetImageUrl(_detailViewModel.Movie.Backdrop_Path)).Into(_movieImage);

            _name.Text = _detailViewModel.Movie.Title;
            _tagline.Text = _detailViewModel.Movie.Tagline;
            _genres.Text = string.Join(", ", _detailViewModel.GenreListForMovie);
            _overview.Text = _detailViewModel.Movie.Overview;
            SetBudgetOrRevenue(_detailViewModel.Movie.Budget, _budgetLayout, _budget);
            SetBudgetOrRevenue(_detailViewModel.Movie.Revenue, _revenueLayout, _revenue);

            PopulateRelatedMovie();
        }

        private void GenerateFields()
        {
            _movieImage = FindViewById<ImageView>(Resource.Id.moviePoster);
            _name = FindViewById<TextView>(Resource.Id.movieTitle);
            _tagline = FindViewById<TextView>(Resource.Id.tagline);
            _genres = FindViewById<TextView>(Resource.Id.movieGenres);

            _overview = FindViewById<TextView>(Resource.Id.overview);

            _budget = FindViewById<TextView>(Resource.Id.budget);
            _budgetLayout = FindViewById<ConstraintLayout>(Resource.Id.budgetLayout);

            _revenue = FindViewById<TextView>(Resource.Id.revenue);
            _revenueLayout = FindViewById<ConstraintLayout>(Resource.Id.revenueLayout);

            _relatedMoviesBox = FindViewById<TextView>(Resource.Id.related_box);

            _firstRelated = FindViewById<ImageView>(Resource.Id.firstRelated);
            _secondRelated = FindViewById<ImageView>(Resource.Id.secondRelated);
            _thirdRelated = FindViewById<ImageView>(Resource.Id.thirdRelated);

            _firstRelatedName = FindViewById<TextView>(Resource.Id.firstRelatedName);
            _secondRelatedName = FindViewById<TextView>(Resource.Id.secondRelatedName);
            _thirdRelatedName = FindViewById<TextView>(Resource.Id.thirdRelatedName);
        }

        private void SetBudgetOrRevenue(long moneyType, ConstraintLayout linearLayoutToSet, TextView textViewToSet)
        {
            if (moneyType == 0 && linearLayoutToSet.Height != 0)
            {
                linearLayoutToSet.SetMinimumHeight(0);
            }
            else if (moneyType > 0)
            {
                textViewToSet.Text = moneyType.ToString("C0");
                if (linearLayoutToSet.Height == 0)
                {
                    linearLayoutToSet.SetMinimumHeight(50);
                }
                //textViewToSet.Text = moneyType.ToString("C0");
            }
        }

        private void PopulateRelatedMovie()
        {
            if (_detailViewModel.RelatedMovie != null && _detailViewModel.RelatedMovie.Count > 0)
            {
                if (_detailViewModel.RelatedMovie[0].Poster_path != null)
                {
                    Picasso.Get().Load(_detailViewModel.GetImageUrl(_detailViewModel.RelatedMovie[0].Poster_path)).Into(_firstRelated);

                    //cell.FirstRelatedImage
                    //    .AddGestureRecognizer(new UITapGestureRecognizer(Tap) { NumberOfTapsRequired = 1 });
                    //var intent = new Intent(this, typeof(MovieDetailView));
                    //intent.PutExtra("MovieId", _detailViewModel.RelatedMovie[0].Id);
                    //StartActivity(intent);

                }

                if (_detailViewModel.RelatedMovie[1].Poster_path != null)
                {
                    Picasso.Get().Load(_detailViewModel.GetImageUrl(_detailViewModel.RelatedMovie[1].Poster_path)).Into(_secondRelated);
                    //var intent = new Intent(this, typeof(MovieDetailView));
                    //intent.PutExtra("MovieId", _detailViewModel.RelatedMovie[1].Id);
                    //StartActivity(intent);
                }
                if (_detailViewModel.RelatedMovie[2].Poster_path != null)
                {
                    Picasso.Get().Load(_detailViewModel.GetImageUrl(_detailViewModel.RelatedMovie[2].Poster_path)).Into(_thirdRelated);

                    //var intent = new Intent(this, typeof(MovieDetailView));
                    //intent.PutExtra("MovieId", _detailViewModel.RelatedMovie[2].Id);
                    //StartActivity(intent);
                }
                _relatedMoviesBox.Text = "Related Movies:";
                _firstRelatedName.Text = _detailViewModel.RelatedMovie[0].Title;
                _secondRelatedName.Text = _detailViewModel.RelatedMovie[1].Title;
                _thirdRelatedName.Text = _detailViewModel.RelatedMovie[2].Title;

            }
            else
            {
                _relatedMoviesBox.Text = "";
                _firstRelatedName.Text = "";
                _secondRelatedName.Text = "";
                _thirdRelatedName.Text = "";
            }
        }
        #endregion
    }
}

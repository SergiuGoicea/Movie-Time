using System;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using MovieTime.Data.Services;
using MovieTime.Data.ViewModel;

namespace MovieTime.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private MovieTableViewModel viewModel;
        private MovieDetailViewModel detailViewModel;
        MovieListAdapter adapter;
        RecyclerView recycler;
        public MainActivity()
        {
            viewModel = ViewModelLocator.Instance.GetInstance<MovieTableViewModel>();
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            recycler = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            var lm = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);

            recycler.SetLayoutManager(lm);

            viewModel.PropertyChanged += Service_PropertyChanged;
            viewModel.OnAppearing();

        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            viewModel.PropertyChanged -= Service_PropertyChanged;
            viewModel.OnDisappearing();
        }
        private void Service_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MovieTableViewModel.MovieList))
            {
                adapter = new MovieListAdapter(viewModel.MovieList, viewModel.GenreList, viewModel);
                recycler.SetAdapter(adapter);
                adapter.ItemClick += Adapter_ItemClick;
                adapter.NotifyDataSetChanged();
            }
            //else if (e.PropertyName == nameof(MovieTableViewModel.IsBusy))
            //{
            //    UpdateBusyUI();
            //}
            //else if (e.PropertyName == nameof(MovieTableViewModel.IsConnectedToInternet))
            //{
            //    UpdateInternetConnectionUI();
            //}
        }

        private void Adapter_ItemClick(object sender, int movieId)
        {
            System.Diagnostics.Debug.WriteLine($"Clicked position {movieId}");

            var intent = new Intent(this, typeof(MovieDetailView));
            intent.PutExtra("MovieId",movieId);
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

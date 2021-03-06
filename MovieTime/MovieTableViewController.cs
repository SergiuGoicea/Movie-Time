// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using MovieTime.Data.Interfaces;
using MovieTime.Data.Model;
using MovieTime.Data.Services;
using MovieTime.Data.ViewModel;
using Refit;
using SDWebImage;
using UIKit;


namespace MovieTime
{
    public partial class MovieTableViewController : UITableViewController, IUISearchResultsUpdating
    {
        UISearchController searchController;

        private MovieTableViewModel viewModel;
        private UIView isLoadingView;

        public MovieTableViewController(IntPtr handle) : base(handle)
        {
            viewModel = ViewModelLocator.Instance.GetInstance<MovieTableViewModel>();
            isLoadingView = CreateLoadingView();
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            searchController = new UISearchController(searchResultsController: null)
            {
                HidesNavigationBarDuringPresentation = false,
                ObscuresBackgroundDuringPresentation = false,
                SearchResultsUpdater = this,
            };

            DefinesPresentationContext = true;
            TableView.TableHeaderView = searchController.SearchBar;
            searchController.SearchBar.Placeholder = "Search Movies";
            searchController.SearchBar.SizeToFit();

            viewModel.PropertyChanged += Service_PropertyChanged;
            viewModel.OnAppearing();

            await viewModel.GetPopularMoviesAndGenres();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UpdateBusyUI();
            UpdateInternetConnectionUI();
        }
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            viewModel.PropertyChanged -= Service_PropertyChanged;
            viewModel.OnDisappearing();

        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            var cell = tableView.DequeueReusableCell(nameof(SearchViewCell)) as SearchViewCell;
            var movies = viewModel.MovieList;
            var genresForMovie = viewModel.GenreName(movies, indexPath.Row, viewModel.GenreList);
            PopulateCell(cell, movies, indexPath.Row, genresForMovie);

            return cell;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            if (segue.Identifier == "ToMovieDetails")
            {

                var destination = segue.DestinationViewController as MovieViewController;
                destination.detailViewModel.MovieId = ((SearchViewCell)sender).MovieId;
            }
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return viewModel.MovieList?.Count ?? 0;
        }


        #region Methods

        public void PopulateCell(SearchViewCell cell, List<Movie> movies, int row, List<string> genreName)
        {

            cell.MovieTitle.Text = movies[row].Title;
            cell.MovieGenres.Text = string.Join(", ", genreName);
            cell.MovieImage.SetImage(new NSUrl(viewModel.GetImageUrl(movies[row].Poster_path)),
                                     null,
                                     SDWebImageOptions.RefreshCached);
            cell.MovieId = movies[row].Id;
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            viewModel.SearchTerm = searchController.SearchBar.Text;
        }

        //TODO move loading label to searchbar
        private UIView CreateLoadingView()
        {
            var loadingView = new UIView();
            var width = 120;
            var height = 30;

            nfloat x = (TableView.Frame.Width / 2) - (width / 2);
            nfloat y = (TableView.Frame.Height / 2) - (height / 2) - (NavigationController != null ? NavigationController.NavigationBar.Frame.Height : 0.0f);
            //loadingView.Frame = new CGRect(x, (nfloat)y, width, height);

            // sets loading text
            var loadingLabel = new UILabel();
            loadingLabel.TextColor = UIColor.Gray;
            loadingLabel.TextAlignment = UITextAlignment.Center;
            loadingLabel.Text = "Loading...";
            //loadingLabel.Frame = new CGRect(0, 0, 10, 10);


            var loadingSpinner = new UIActivityIndicatorView();

            // sets the spinner
            loadingSpinner.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            loadingSpinner.Frame = new CGRect(0, 0, 30, 30);
            loadingSpinner.StartAnimating();
            nfloat xS = searchBar.Frame.Width / 2 - width / 2;
            nfloat yS = searchBar.Frame.Height / 2 - height / 2 - (SearchDisplayController != null ? SearchDisplayController.SearchBar.Frame.Height : 0.0f);

            //loadingView.Frame = new CGRect(xS, yS, 30, 30);
            loadingView.Frame = new CGRect(10, 10, 30, 30);


            // adds text and spinner to the view
            loadingView.AddSubviews(new UIView[] { loadingSpinner });

            return loadingView;

            // adds the loading view to the table view
            // TableView.AddSubview(_loadingView);
        }

        private void Service_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MovieTableViewModel.MovieList))
            {
                TableView.ReloadData();
            }
            else if (e.PropertyName == nameof(MovieTableViewModel.IsBusy))
            {
                UpdateBusyUI();
            }
            else if (e.PropertyName == nameof(MovieTableViewModel.IsConnectedToInternet))
            {
                UpdateInternetConnectionUI();
            }
        }

        private void UpdateBusyUI()
        {
            if (viewModel.IsBusy)
                TableView.AddSubview(isLoadingView);
            else
                isLoadingView.RemoveFromSuperview();
        }

        void UpdateInternetConnectionUI()
        {
            var isConnected = viewModel.IsConnectedToInternet;
        }
        #endregion
    }
}

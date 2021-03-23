// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using MovieTime.Data.Model;
using MovieTime.Data.Services;
using MovieTime.Data.ViewModel;
using Refit;
using SDWebImage;
using UIKit;

namespace MovieTime
{
    public partial class MovieViewController : UITableViewController
    {
        string ImagePath;
        public MovieDetailViewModel detailViewModel;
        private UIView isLoadingView;

        public MovieViewController(IntPtr handle) : base(handle)
        {
            detailViewModel = ViewModelLocator.Instance.GetInstance<MovieDetailViewModel>();
            //isLoadingView = CreateLoadingView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //detailViewModel.PropertyChanged += Service_PropertyChanged;
            detailViewModel.OnAppearing();
            CheckStatusBarOnStartup();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            //UpdateBusyUI();
            //UpdateInternetConnectionUI();
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            //detailViewModel.PropertyChanged -= Service_PropertyChanged;
            detailViewModel.OnDisappearing();
        }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = TableView.DequeueReusableCell(nameof(MovieDetailCell)) as MovieDetailCell;

            PopulateCell(cell);

            return cell;

        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return detailViewModel.Movie == null ? 0 : 1;
        }

        public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillRotate(toInterfaceOrientation, duration);
            SetImagePathByOrientation();

        }

        private void SetImagePathByOrientation()
        {
            if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
            {
                ImagePath = detailViewModel.Movie.Poster_path;
                TableView.ReloadData();
            }

            else
            {
                ImagePath = detailViewModel.Movie.Backdrop_Path;
                TableView.ReloadData();
            }
        }

        #region Methods

        public void PopulateCell(MovieDetailCell cell)
        {

            cell.FirstRelatedImage.UserInteractionEnabled = true;
            cell.SecondRelatedImage.UserInteractionEnabled = true;
            cell.ThirdRelatedImage.UserInteractionEnabled = true;

            cell.MoviePoster.SetImage(new NSUrl(detailViewModel.GetImageUrl(ImagePath)),
                         null,
                         SDWebImageOptions.RefreshCached);
            cell.MovieTitle.Text = detailViewModel.Movie.Title;
            cell.TaglineText.Text = detailViewModel.Movie.Tagline;

            SetBudgetOrRevenue(cell, detailViewModel.Movie.Budget, cell.BudgetStack, cell.BudgetText); ;
            SetBudgetOrRevenue(cell, detailViewModel.Movie.Revenue, cell.RevenueStack, cell.RevenueText); ;

            cell.GenreText.Text = null;

            cell.GenreText.Text = string.Join(", ", detailViewModel.GenreListForMovie);
            cell.OverviewText.Text = detailViewModel.Movie.Overview;

            PopulateRelatedMovie(cell);
        }



        private void CheckStatusBarOnStartup()
        {
            if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight)
            {
                ImagePath = detailViewModel.Movie.Poster_path;
                TableView.ReloadData();
            }

            else
            {
                ImagePath = detailViewModel.Movie.Backdrop_Path;
                TableView.ReloadData();
            }
            TableView.ReloadData();
        }

        private async void Tap(UITapGestureRecognizer tap1)
        {

            var cell = tap1.View.Superview.Superview as MovieDetailCell;

            if (tap1.View == cell.FirstRelatedImage)
            {
                detailViewModel.MovieId = detailViewModel.RelatedMovie[0].Id;

            }
            else if (tap1.View == cell.SecondRelatedImage)
            {
                detailViewModel.MovieId = detailViewModel.RelatedMovie[1].Id;
            }
            else
            {
                detailViewModel.MovieId = detailViewModel.RelatedMovie[2].Id;
            }
            await detailViewModel.GetMovie();
            SetImagePathByOrientation();
            TableView.ReloadData();
        }

        private void PopulateRelatedMovie(MovieDetailCell cell)
        {
            if (detailViewModel.RelatedMovie != null && detailViewModel.RelatedMovie.Count > 0)
            {
                if (detailViewModel.RelatedMovie[0].Poster_path != null)
                {
                    cell.FirstRelatedImage
                        .SetImage(new NSUrl(detailViewModel.GetImageUrl(detailViewModel.RelatedMovie[0].Poster_path)),
                                                null,
                                                SDWebImageOptions.RefreshCached);
                    cell.FirstRelatedImage
                        .AddGestureRecognizer(new UITapGestureRecognizer(Tap) { NumberOfTapsRequired = 1 });
                }

                if (detailViewModel.RelatedMovie[1].Poster_path != null)
                {
                    cell.SecondRelatedImage
                        .SetImage(new NSUrl(detailViewModel.GetImageUrl(detailViewModel.RelatedMovie[1].Poster_path)),
                                            null,
                                            SDWebImageOptions.RefreshCached);
                    cell.SecondRelatedImage
                        .AddGestureRecognizer(new UITapGestureRecognizer(Tap) { NumberOfTapsRequired = 1 });
                }
                if (detailViewModel.RelatedMovie[2].Poster_path != null)
                {
                    cell.ThirdRelatedImage
                        .SetImage(new NSUrl(detailViewModel.GetImageUrl(detailViewModel.RelatedMovie[2].Poster_path)),
                                            null,
                                            SDWebImageOptions.RefreshCached);

                    cell.ThirdRelatedImage
                        .AddGestureRecognizer(new UITapGestureRecognizer(Tap) { NumberOfTapsRequired = 1 });
                }
                cell.RelatedMovies.Text = "Related Movie:";
                cell.FirstRelatedMovie.Text = detailViewModel.RelatedMovie[0].Title;
                cell.SecondRelatedMovie.Text = detailViewModel.RelatedMovie[1].Title;
                cell.ThirdRelatedMovie.Text = detailViewModel.RelatedMovie[2].Title;

            }
            else
            {
                cell.RelatedMovies.Text = "";
                cell.FirstRelatedMovie.Text = "";
                cell.SecondRelatedMovie.Text = "";
                cell.ThirdRelatedMovie.Text = "";
            }
        }

        private void SetBudgetOrRevenue(MovieDetailCell cell, long moneyType, UIStackView stackViewToSet, UILabel uiLabelToSet)
        {
            if (moneyType == 0 && stackViewToSet.Bounds.Size.Height != 0)
            {

                if (stackViewToSet.Bounds.Size.Height == 49)
                {
                    stackViewToSet.RemoveConstraint(stackViewToSet.Constraints[1]);
                }
                stackViewToSet.HeightAnchor.ConstraintEqualTo(0).Active = true;
            }
            else if (moneyType > 0)
            {
                uiLabelToSet.Text = moneyType.ToString("C0");
                if (stackViewToSet.Bounds.Size.Height == 0)
                {
                    stackViewToSet.RemoveConstraint(stackViewToSet.Constraints[1]);
                    stackViewToSet.HeightAnchor.ConstraintEqualTo(49).Active = true;
                }
                uiLabelToSet.Text = moneyType.ToString("C0");
            }
        }

        private UIView CreateLoadingView()
        {
            var loadingView = new UIView();
            var width = 120;
            var height = 30;

            nfloat x = (TableView.Frame.Width / 2) - (width / 2);
            nfloat y = (TableView.Frame.Height / 2) - (height / 2) - (NavigationController != null ? NavigationController.NavigationBar.Frame.Height : 0.0f);
            loadingView.Frame = new CGRect(x, (nfloat)y, width, height);

            // sets loading text
            var loadingLabel = new UILabel();
            loadingLabel.TextColor = UIColor.Gray;
            loadingLabel.TextAlignment = UITextAlignment.Center;
            loadingLabel.Text = "Loading...";
            loadingLabel.Frame = new CGRect(0, 0, 140, 30);

            var loadingSpinner = new UIActivityIndicatorView();

            // sets the spinner
            loadingSpinner.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
            loadingSpinner.Frame = new CGRect(0, 0, 30, 30);
            loadingSpinner.StartAnimating();

            // adds text and spinner to the view
            loadingView.AddSubviews(new UIView[] { loadingLabel, loadingSpinner });

            return loadingView;

            // adds the loading view to the table view
            // TableView.AddSubview(_loadingView);
        }


        //private void Service_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    //if (e.PropertyName == nameof(MovieDetailViewModel.Movie))
        //    //{
        //    //    TableView.ReloadData();
        //    //}
        //    if (e.PropertyName == nameof(MovieDetailViewModel.IsBusy))
        //    {
        //        UpdateBusyUI();
        //    }
        //    else if (e.PropertyName == nameof(MovieDetailViewModel.IsConnectedToInternet))
        //    {
        //        UpdateInternetConnectionUI();
        //    }
        //}

        //private void UpdateBusyUI()
        //{
        //    if (detailViewModel.IsBusy)
        //        TableView.AddSubview(isLoadingView);
        //    else
        //        isLoadingView.RemoveFromSuperview();
        //}

        //void UpdateInternetConnectionUI()
        //{
        //    var isConnected = detailViewModel.IsConnectedToInternet;
        //}

        #endregion
    }
}

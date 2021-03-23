// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MovieTime
{
	[Register ("MovieDetailCell")]
	partial class MovieDetailCell
	{
		[Outlet]
		UIKit.UILabel budgetBox { get; set; }

		[Outlet]
		UIKit.UIStackView budgetStack { get; set; }

		[Outlet]
		UIKit.UILabel budgetText { get; set; }

		[Outlet]
		UIKit.UIImageView firstRelatedImage { get; set; }

		[Outlet]
		UIKit.UILabel firstRelatedMovie { get; set; }

		[Outlet]
		UIKit.UILabel genreText { get; set; }

		[Outlet]
		UIKit.UILabel movieName { get; set; }

		[Outlet]
		UIKit.UIImageView moviePoster { get; set; }

		[Outlet]
		UIKit.UILabel overviewText { get; set; }

		[Outlet]
		UIKit.UILabel relatedMovies { get; set; }

		[Outlet]
		UIKit.UILabel revenueBox { get; set; }

		[Outlet]
		UIKit.UIStackView revenueStack { get; set; }

		[Outlet]
		UIKit.UILabel revenueText { get; set; }

		[Outlet]
		UIKit.UIImageView secondRelatedImage { get; set; }

		[Outlet]
		UIKit.UILabel secondRelatedMovie { get; set; }

		[Outlet]
		UIKit.UILabel taglineText { get; set; }

		[Outlet]
		UIKit.UIImageView thirdRelatedImage { get; set; }

		[Outlet]
		UIKit.UILabel thirdRelatedMovie { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (revenueStack != null) {
				revenueStack.Dispose ();
				revenueStack = null;
			}

			if (budgetStack != null) {
				budgetStack.Dispose ();
				budgetStack = null;
			}

			if (budgetBox != null) {
				budgetBox.Dispose ();
				budgetBox = null;
			}

			if (budgetText != null) {
				budgetText.Dispose ();
				budgetText = null;
			}

			if (firstRelatedImage != null) {
				firstRelatedImage.Dispose ();
				firstRelatedImage = null;
			}

			if (firstRelatedMovie != null) {
				firstRelatedMovie.Dispose ();
				firstRelatedMovie = null;
			}

			if (genreText != null) {
				genreText.Dispose ();
				genreText = null;
			}

			if (movieName != null) {
				movieName.Dispose ();
				movieName = null;
			}

			if (moviePoster != null) {
				moviePoster.Dispose ();
				moviePoster = null;
			}

			if (overviewText != null) {
				overviewText.Dispose ();
				overviewText = null;
			}

			if (relatedMovies != null) {
				relatedMovies.Dispose ();
				relatedMovies = null;
			}

			if (revenueBox != null) {
				revenueBox.Dispose ();
				revenueBox = null;
			}

			if (revenueText != null) {
				revenueText.Dispose ();
				revenueText = null;
			}

			if (secondRelatedImage != null) {
				secondRelatedImage.Dispose ();
				secondRelatedImage = null;
			}

			if (secondRelatedMovie != null) {
				secondRelatedMovie.Dispose ();
				secondRelatedMovie = null;
			}

			if (taglineText != null) {
				taglineText.Dispose ();
				taglineText = null;
			}

			if (thirdRelatedImage != null) {
				thirdRelatedImage.Dispose ();
				thirdRelatedImage = null;
			}

			if (thirdRelatedMovie != null) {
				thirdRelatedMovie.Dispose ();
				thirdRelatedMovie = null;
			}
		}
	}
}

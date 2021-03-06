// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace MovieTime
{
	public partial class SearchViewCell : UITableViewCell
	{
		public SearchViewCell (IntPtr handle) : base (handle)
		{
		}

        public UIImageView MovieImage => movieImage;

        public UILabel MovieTitle => movieName;

        public UILabel MovieGenres => movieGenres;

		public int MovieId { get; set; }

	}
}

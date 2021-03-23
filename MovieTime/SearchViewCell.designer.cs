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
	[Register ("SearchViewCell")]
	partial class SearchViewCell
	{
		[Outlet]
		UIKit.UILabel movieGenres { get; set; }

		[Outlet]
		UIKit.UIImageView movieImage { get; set; }

		[Outlet]
		UIKit.UILabel movieName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (movieGenres != null) {
				movieGenres.Dispose ();
				movieGenres = null;
			}

			if (movieImage != null) {
				movieImage.Dispose ();
				movieImage = null;
			}

			if (movieName != null) {
				movieName.Dispose ();
				movieName = null;
			}
		}
	}
}

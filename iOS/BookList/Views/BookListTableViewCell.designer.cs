// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ProlificLibrary.iOS
{
	[Register ("BookListTableViewCell")]
	partial class BookListTableViewCell
	{
		[Outlet]
		UIKit.UILabel subtitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (subtitleLabel != null) {
				subtitleLabel.Dispose ();
				subtitleLabel = null;
			}
		}
	}
}

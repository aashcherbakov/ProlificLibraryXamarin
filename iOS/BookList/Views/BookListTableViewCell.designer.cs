// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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
            if (subtitleLabel != null) {
                subtitleLabel.Dispose ();
                subtitleLabel = null;
            }

            if (titleLabel != null) {
                titleLabel.Dispose ();
                titleLabel = null;
            }
        }
    }
}
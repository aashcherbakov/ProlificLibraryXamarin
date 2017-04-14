// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ProlificLibrary.iOS
{
    [Register ("EditBookViewController")]
    partial class EditBookViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField authorTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField categoriesTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField publisherTextField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton submitButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField titleTextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (authorTextField != null) {
                authorTextField.Dispose ();
                authorTextField = null;
            }

            if (categoriesTextField != null) {
                categoriesTextField.Dispose ();
                categoriesTextField = null;
            }

            if (publisherTextField != null) {
                publisherTextField.Dispose ();
                publisherTextField = null;
            }

            if (submitButton != null) {
                submitButton.Dispose ();
                submitButton = null;
            }

            if (titleTextField != null) {
                titleTextField.Dispose ();
                titleTextField = null;
            }
        }
    }
}
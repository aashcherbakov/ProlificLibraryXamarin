using System;

using UIKit;

namespace ProlificLibrary.iOS
{
    public partial class BookEditViewController : UIViewController
    {
        public BookEditViewModel viewModel;

        public BookEditViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}


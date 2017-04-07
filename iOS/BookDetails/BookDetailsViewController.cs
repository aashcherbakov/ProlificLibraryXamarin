using System;
using System.Threading.Tasks;
using UIKit;

namespace ProlificLibrary.iOS
{
    public partial class BookDetailsViewController : UIViewController
    {
        public BookDetailsViewController(IntPtr handle) : base(handle) { }
        public BookDetailsViewModel viewModel;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            SetupDesign();
        }

        // Actions

        partial void CheckoutButton_TouchUpInside(UIButton sender)
        {
            Task.Run(async () =>
            {
                await viewModel.CheckoutBook();
            });
        }

        // Private functions

        void SetupDesign() 
        {
            Title = "Book Details";
            titleLabel.Text = viewModel.Title;
            subtitleLabel.Text = viewModel.Author;
            publisherLabel.Text = viewModel.Publisher;
            categoriesLabel.Text = viewModel.Categories;

            checkoutButton.SetTitle("Checkout Book", UIControlState.Normal);
        }
    }
}


using System;
using System.Threading.Tasks;
using UIKit;

namespace ProlificLibrary.iOS
{
    public partial class BookDetailsViewController : UIViewController
    {
        public BookDetailsViewController(IntPtr handle) : base(handle) { }
        public BookDetailsViewModel viewModel;
        UITextField field;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            SetupDesign();
        }

        // Actions

        partial void CheckoutButton_TouchUpInside(UIButton sender)
        {
            AskForUsersName();
        }

        // Private functions

        void AskForUsersName() 
        {
            var alert = UIAlertController.Create("Who dares to check out this book?", "Name yourself", UIAlertControllerStyle.Alert);

            alert.AddTextField((textfield) => {
                field = textfield;

                field.Placeholder = "Uncle Bob";
                field.AutocorrectionType = UITextAutocorrectionType.No;
                field.KeyboardType = UIKeyboardType.Default;
                field.ReturnKeyType = UIReturnKeyType.Done;
                field.ClearButtonMode = UITextFieldViewMode.WhileEditing;
            });

            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => {
                string name = field.Text;
                Task.Run(async () =>
                {
                    await CheckoutBook(name);
                });
            }));

            PresentViewController(alert, true, null);
        }

        async Task CheckoutBook(string name)
        {
            try {
                await viewModel.CheckoutBook(name);
                UpdateCheckedOutBy();
            }

            catch (Exception e) {
                var alert = Alerter.PresentOKAlert("Oops", e.Message, this);
                PresentViewController(alert, true, null);
            }
        }

        void SetupDesign() 
        {
            Title = "Book Details";
            titleLabel.Text = viewModel.Title;
            subtitleLabel.Text = viewModel.Author;
            publisherLabel.Text = viewModel.Publisher;
            categoriesLabel.Text = viewModel.Categories;

            UpdateCheckedOutBy();

            checkoutButton.SetTitle("Checkout Book", UIControlState.Normal);
        }

        void UpdateCheckedOutBy()
        {
            InvokeOnMainThread(() =>
            {
                lastCheckedOutLabel.Text = $"Last checked out: {viewModel.LastCheckedOut}";
                lastCheckedOutByLabel.Text = $"By {viewModel.LastCheckedOutBy}";
            });
        }
    }
}


using System;
using System.Threading.Tasks;
using UIKit;

namespace ProlificLibrary.iOS
{
    public partial class BookDetailsViewController : UIViewController
    {
        public BookDetailsViewController(IntPtr handle) : base(handle) { }
        public BookDetailsViewModel viewModel;
        public event BookListUpdateDelegate didUpdateBook;

        UITextField field;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupDesign();

            didUpdateBook += (book) =>
            {
                viewModel.UpdateBook(book);
                UpdateLabels();
            };
        }

        // Actions

        partial void CheckoutButton_TouchUpInside(UIButton sender)
        {
            AskForUsersName();
        }

        // Private functions

        void AskForUsersName() 
        {
            var alert = UIAlertController.Create("Who dares to check out this book?", 
                                                 "Name yourself", 
                                                 UIAlertControllerStyle.Alert);
            alert.AddTextField(ConfigureTextField);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => {
                string name = field.Text;
                Task.Run(async () => await CheckoutBook(name));
            }));

            PresentViewController(alert, true, null);
        }

        async Task CheckoutBook(string name)
        {
            try {
                await viewModel.CheckoutBook(name);
                UpdateCheckedOutBy();
                didUpdateBook(viewModel.Book);
            }

            catch (Exception e) {
                Alerter.PresentOKAlert("Oops", e.Message, this);
            }
        }

        void SetupDesign() 
        {
            Title = "Book Details";

            UpdateLabels();
            UpdateCheckedOutBy();
            AddEditBarButton();

            checkoutButton.SetTitle("Checkout Book", UIControlState.Normal);
        }

        void UpdateLabels()
        {
            titleLabel.Text = viewModel.Title;
            subtitleLabel.Text = viewModel.Author;
            publisherLabel.Text = viewModel.Publisher;
            categoriesLabel.Text = viewModel.Categories;
        }

        void AddEditBarButton()
        {
            var editButton = new UIBarButtonItem(UIBarButtonSystemItem.Edit, (sender, e) => NavigateToEditBook());
            NavigationItem.SetRightBarButtonItem(editButton, false);
        }

        void UpdateCheckedOutBy()
        {
            InvokeOnMainThread(() =>
            {
                lastCheckedOutLabel.Text = viewModel.LastCheckedOut;
                lastCheckedOutByLabel.Text = viewModel.LastCheckedOutBy;
            });
        }

        void ConfigureTextField(UITextField textField)
        {
            field = textField;

            field.Placeholder = "Uncle Bob";
            field.AutocorrectionType = UITextAutocorrectionType.No;
            field.KeyboardType = UIKeyboardType.Default;
            field.ReturnKeyType = UIReturnKeyType.Done;
            field.ClearButtonMode = UITextFieldViewMode.WhileEditing;
        }

        // Navigation

        void NavigateToEditBook()
        {
            var editViewModel = new BookEditViewModel(viewModel.Book);
            var storyBoard = UIStoryboard.FromName("Main", null);
            var editViewController = storyBoard.InstantiateViewController("EditBookViewController") as EditBookViewController;
            editViewController.viewModel = editViewModel;
            editViewController.didUpdateBook = didUpdateBook;
            NavigationController.PushViewController(editViewController, true);
        }
    }
}


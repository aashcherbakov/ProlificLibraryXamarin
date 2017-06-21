using System;
using System.Threading.Tasks;
using ProlificLibrary.iOS.BookList;
using ProlificLibrary.iOS.Utility;
using ProlificLibrary.Routing;
using UIKit;

namespace ProlificLibrary.iOS
{
    public class BookDetailsPayload : ITransferable
    {
        public Book Book;

        public BookDetailsPayload(Book book)
        {
            Book = book;
        }
    }
    
    public partial class BookDetailsViewController : UIViewController, IPresenter
    {
        public BookDetailsViewController(IntPtr handle) : base(handle) { }
        public BookDetailsViewModel ViewModel;
        public event BookListUpdateDelegate DidUpdateBook;

        UITextField field;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupDesign();

            DidUpdateBook += (book) =>
            {
                ViewModel.UpdateBook(book);
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
                await ViewModel.CheckoutBook(name);
                UpdateCheckedOutBy();
                DidUpdateBook?.Invoke(ViewModel.Book);
            }

            catch (Exception e) {
                Alerter.PresentOkAlert("Oops", e.Message, this);
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
            titleLabel.Text = ViewModel.Title;
            subtitleLabel.Text = ViewModel.Author;
            publisherLabel.Text = ViewModel.Publisher;
            categoriesLabel.Text = ViewModel.Categories;
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
                lastCheckedOutLabel.Text = ViewModel.LastCheckedOut;
                lastCheckedOutByLabel.Text = ViewModel.LastCheckedOutBy;
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
            var editViewModel = new BookEditViewModel(ViewModel.Book);
            var storyBoard = UIStoryboard.FromName("Main", null);
            var editViewController = storyBoard.InstantiateViewController("EditBookViewController") as EditBookViewController;
            editViewController.viewModel = editViewModel;
            editViewController.didUpdateBook = DidUpdateBook;
            NavigationController.PushViewController(editViewController, true);
        }

        public void Present(IPresenter screen, NavigationType navigationType)
        {
            this.PresentScreen(screen, navigationType);
        }
    }
}


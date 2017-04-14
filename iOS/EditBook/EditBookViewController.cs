using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace ProlificLibrary.iOS
{
    public partial class EditBookViewController : UIViewController
    {
        public BookEditViewModel viewModel;
        public BookListUpdateDelegate didUpdateBook;

        public EditBookViewController (IntPtr handle) : base (handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            submitButton.TouchUpInside += async (sender, e) => await SubmitButtonTappedAsync();
        }

        async Task SubmitButtonTappedAsync()
        {
            UpdateViewModelValues();

            try { await AddBook(); } 
            catch (Exception e) { Alerter.PresentOKAlert("Oops", e.Message, this); }
        }

        void UpdateViewModelValues()
        {
            viewModel.Author = authorTextField.Text;
            viewModel.Title = titleTextField.Text;
            viewModel.Categories = categoriesTextField.Text;
            viewModel.Publisher = publisherTextField.Text;
        }

        async Task AddBook()
        {
            await viewModel.AddBook();
            didUpdateBook(viewModel.Book);
            PresentSuccessAlert();
        }

        void PresentSuccessAlert()
        {
            Alerter.PresentOKAlert("Success!", "Book was added to list", this, (action) =>
            {
                NavigationController.PopViewController(true);
            });
        }
    }
}
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace ProlificLibrary.iOS
{
    public partial class EditBookViewController : BaseViewController
    {
        public BookEditViewModel viewModel;
        public BookListUpdateDelegate didUpdateBook;

        public EditBookViewController (IntPtr handle) : base (handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupDesign();
            submitButton.TouchUpInside += async (sender, e) => await SubmitButtonTappedAsync();
        }

        // Private functions

        void SetupDesign() 
        {
            Title = viewModel.ScreenTitle;

            titleTextField.Text = viewModel.Title;
            authorTextField.Text = viewModel.Author;
            categoriesTextField.Text = viewModel.Categories;
            publisherTextField.Text = viewModel.Publisher;
        }

        async Task SubmitButtonTappedAsync()
        {
            UpdateViewModelValues();

            try { await SubmitBookChanges(); } 
            catch (Exception e) { Alerter.PresentOKAlert("Oops", e.Message, this); }
        }

        void UpdateViewModelValues()
        {
            viewModel.Author = authorTextField.Text;
            viewModel.Title = titleTextField.Text;
            viewModel.Categories = categoriesTextField.Text;
            viewModel.Publisher = publisherTextField.Text;
        }

        async Task SubmitBookChanges()
        {
            await viewModel.SubmitChanges();
            didUpdateBook(viewModel.Book);
            PresentSuccessAlert();
        }

        void PresentSuccessAlert()
        {
            Alerter.PresentOKAlert(Constants.SuccessMessages.Success, 
                                   Constants.SuccessMessages.BookAddedWithSuccess, this, (action) =>
            {
                NavigationController.PopViewController(true);
            });
        }
    }
}
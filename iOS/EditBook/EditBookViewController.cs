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
            viewModel.Author = authorTextField.Text;
            viewModel.Title = titleTextField.Text;
            viewModel.Categories = categoriesTextField.Text;
            viewModel.Publisher = publisherTextField.Text;

            try
            {
                await viewModel.AddBook();
                didUpdateBook(viewModel.Book);
                Alerter.PresentOKAlert("Success!", "Book was added to list", this, (action) => {
                    NavigationController.PopViewController(true);  
                });
            } 
            catch (Exception e) 
            {
                Alerter.PresentOKAlert("Oops", e.Message, this);
            }
        }
    }
}
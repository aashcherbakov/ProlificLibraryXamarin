using System;
using System.Threading.Tasks;
using UIKit;

namespace ProlificLibrary.iOS
{
    public delegate void BookListUpdateDelegate(Book book);

    public partial class BookListViewController : UIViewController
    {
        const float kEstimatedCellHeight = 40.0f;

        BookListViewModel viewModel;
        BookListTableSource tableSource;
        BookListEmptyView emptyView;

        public BookListViewController(IntPtr handle) : base(handle) { }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var factory = new BookListStateFactory(View, tableView, emptyView, activityIndicator);
            var resource = new RemoteResource();
            viewModel = new BookListViewModel(resource, factory);

            SetupDesign();
            await LoadData();
        }

        // Plus button tapped
        partial void UIBarButtonItemLRb1vgKg_Activated(UIBarButtonItem sender)
        {
            NavigateToEditBook();
        }

        void SetupDesign()
        {
            Title = viewModel.Title;
            tableSource = new BookListTableSource(viewModel) { selectionDelegate = NavigaeteToBookDetails };

            emptyView = null;
            activityIndicator.HidesWhenStopped = true;

            tableView.Hidden = true;
            tableView.RegisterNibForCellReuse(BookListTableViewCell.Nib, BookListTableViewCell.Key);
            tableView.EstimatedRowHeight = kEstimatedCellHeight;
            tableView.Source = tableSource;
        }

        async Task LoadData()
        {
            try { await viewModel.LoadBooks(); }
            catch (Exception exception) { Alerter.PresentOKAlert("Oops", exception.Message, this); }
        }

        void DidUpdateBook(Book book)
        {
            viewModel.UpdateBookList(book);
            tableView.ReloadData();
        }

        // Navigation

        void NavigateToEditBook()
        {
            var editViewModel = new BookEditViewModel();
            var storyBoard = UIStoryboard.FromName("Main", null);
            var editViewController = storyBoard.InstantiateViewController("EditBookViewController") as EditBookViewController;
            editViewController.viewModel = editViewModel;
            editViewController.didUpdateBook = DidUpdateBook;
            NavigationController.PushViewController(editViewController, true);
        }

        void NavigaeteToBookDetails(Book book)
        {
            var detailsViewModel = new BookDetailsViewModel(book);
            var storyBoard = UIStoryboard.FromName("Main", null);
            var detailsController = storyBoard.InstantiateViewController("BookDetailsViewController") as BookDetailsViewController;
            detailsController.viewModel = detailsViewModel;
            detailsController.didUpdateBook = DidUpdateBook;
            NavigationController.PushViewController(detailsController, true);
        }
    }
}


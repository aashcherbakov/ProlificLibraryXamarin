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
        BookListState state;

        public BookListViewController(IntPtr handle) : base(handle) { }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            viewModel = new BookListViewModel(new RemoteResource());

            SetupDesign();
            UpdateState(null);
            await LoadData();
        }

        // Plus button tapped
        partial void UIBarButtonItemLRb1vgKg_Activated(UIBarButtonItem sender)
        {
            NavigateToEditBook();
        }

        void SetupDesign()
        {
            tableSource = new BookListTableSource();
            tableSource.selectionDelegate = NavigaeteToBookDetails;
            emptyView = null;
            activityIndicator.HidesWhenStopped = true;

            tableView.Hidden = true;
            tableView.RegisterNibForCellReuse(BookListTableViewCell.Nib, BookListTableViewCell.Key);
            tableView.EstimatedRowHeight = kEstimatedCellHeight;
            tableView.Source = tableSource;
        }

        void UpdateState(Book[] books)
        {
            state = BookListStateFactory.Create(books);
            state.emptyView = emptyView;
            state.tableView = tableView;
            state.mainView = View;
            state.activityIndicator = activityIndicator;
            state.UpdateDesign();
        }

        async Task LoadData()
        {
            try
            {
                var books = await viewModel.LoadBooks();
                UpdateDesign(books);
            }
            catch (Exception exception)
            {
                PresentAlert(exception);
            }
        }

        void PresentAlert(Exception exception)
        {
            Alerter.PresentOKAlert("Oops", exception.Message, this);
            UpdateState(new Book[] { });
        }

        void UpdateDesign(Book[] books)
        {
            tableSource.UpdateWithBooks(books);
            tableView.ReloadData();
            UpdateState(books);
            state.UpdateDesign();
        }

        void DidUpdateBook(Book book)
        {
            viewModel.UpdateBookList(book);
            tableSource.UpdateWithBooks(viewModel.Books.ToArray());
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


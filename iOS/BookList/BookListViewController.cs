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
		UIRefreshControl refreshControl;

        public BookListViewController(IntPtr handle) : base(handle) {}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SetupRefreshControl();

			var factory = new BookListStateFactory(View, tableView, emptyView, activityIndicator, refreshControl);
			var resource = new RemoteResource();
			viewModel = new BookListViewModel(resource, factory);

			SetupDesign();
			await LoadData();
		}

        // Plus button tapped
        partial void UIBarButtonItemLRb1vgKg_Activated(UIBarButtonItem sender)
        {
			NavigateToAddBook();
        }

        void SetupDesign()
        {
            Title = viewModel.Title;
            tableSource = new BookListTableSource(viewModel) { 
				selectionDelegate = NavigaeteToBookDetails,
				editingDelegate = OnEditing,
				deletingDelegate = OnDeleting
			};
            emptyView = null;
            activityIndicator.HidesWhenStopped = true;

            tableView.Hidden = true;
            tableView.RegisterNibForCellReuse(BookListTableViewCell.Nib, BookListTableViewCell.Key);
            tableView.EstimatedRowHeight = kEstimatedCellHeight;
            tableView.Source = tableSource;
        }

		void SetupRefreshControl()
		{
			var control = new UIRefreshControl();
			control.ValueChanged += async (sender, e) => await viewModel.RefreshBooks();
			refreshControl = control;
			tableView.AddSubview(refreshControl);
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

		void OnEditing(Book book)
		{
			if (book != null)
				NavigateToAddBook(book);
		}

		void OnDeleting(Book book)
		{
			if (book != null) 
				Task.Run(async () => await DeleteBook(book));
		}

		async Task DeleteBook(Book book)
		{
			try {
				await viewModel.DeleteBook(book);
				InvokeOnMainThread(() => {
					Alerter.PresentOKAlert("Success!", "Book was deleted", this, null);
					tableView.ReloadData();
				});
			}

			catch (Exception e) {
				InvokeOnMainThread(() =>
				{
					Alerter.PresentOKAlert("Opps", e.Message, this, null);
				});
			}
		}

        // Navigation

		void NavigateToAddBook(Book book = null)
        {
			var editViewModel = new BookEditViewModel(book);
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
            detailsController.didUpdateBook += DidUpdateBook;
            NavigationController.PushViewController(detailsController, true);
        }
    }
}


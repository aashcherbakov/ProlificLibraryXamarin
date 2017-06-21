using System;
using System.Threading.Tasks;
using ProlificLibrary.iOS.Utility;
using ProlificLibrary.Networking;
using ProlificLibrary.Routing;
using ProlificLibrary.ViewModels;
using UIKit;

namespace ProlificLibrary.iOS.BookList
{
    public delegate void BookListUpdateDelegate(Book book);

    public partial class BookListViewController : UIViewController, IPresenter
    {
	    private const float EstimatedCellHeight = 40.0f;

	    private BookListViewModel viewModel;
	    private BookListTableSource tableSource;
	    private BookListEmptyView emptyView;
	    private UIRefreshControl refreshControl;

        public BookListViewController(IntPtr handle) : base(handle) {}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SetupRefreshControl();

			var factory = new BookListStateFactory(View, tableView, emptyView, activityIndicator, refreshControl);
			var resource = new RemoteResource();
			viewModel = new BookListViewModel(resource, factory, this, new Router());

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
            tableView.EstimatedRowHeight = EstimatedCellHeight;
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
            catch (Exception exception) { Alerter.PresentOkAlert("Oops", exception.Message, this); }
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
					Alerter.PresentOkAlert("Success!", "Book was deleted", this, null);
					tableView.ReloadData();
				});
			}

			catch (Exception e) {
				InvokeOnMainThread(() =>
				{
					Alerter.PresentOkAlert("Opps", e.Message, this, null);
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
	        viewModel.SelectBook(new BookDetailsPayload(book));
//            var detailsViewModel = new BookDetailsViewModel(book);
//            var storyBoard = UIStoryboard.FromName("Main", null);
//            var detailsController = storyBoard.InstantiateViewController("BookDetailsViewController") as BookDetailsViewController;
//            detailsController.ViewModel = detailsViewModel;
//            detailsController.DidUpdateBook += DidUpdateBook;
//            NavigationController.PushViewController(detailsController, true);
        }

	    public void Present(IPresenter screen, NavigationType navigationType)
	    {
		    this.PresentScreen(screen, navigationType);
	    }
    }
}


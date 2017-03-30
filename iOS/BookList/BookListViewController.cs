using System;
using System.Threading.Tasks;
using UIKit;

namespace ProlificLibrary.iOS
{
	public partial class BookListViewController : UIViewController
	{
		private const float kEstimatedCellHeight = 40.0f;

		private BookListViewModel viewModel;
		private BookListTableSource tableSource;
		private BookListEmptyView emptyView;
		private BookListState state;

		public BookListViewController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			SetupDesign();
			UpdateState(null);
			LoadData();
		}

		private void UpdateState(Book[] books)
		{
			state = BookListStateFactory.Create(books);
			state.emptyView = emptyView;
			state.tableView = tableView;
			state.mainView = View;
			state.activityIndicator = activityIndicator;
		}

		private void SetupDesign()
		{
			tableSource = new BookListTableSource();
			emptyView = null;

			activityIndicator.HidesWhenStopped = true;

			tableView.Hidden = true;
			tableView.RegisterNibForCellReuse(BookListTableViewCell.Nib, BookListTableViewCell.Key);
			tableView.EstimatedRowHeight = kEstimatedCellHeight;
			tableView.Source = tableSource;
		}

		private void LoadData()
		{
			viewModel = new BookListViewModel(new RemoteResource());

			Task.Run(async () =>
			{
				var books = await viewModel.LoadBooks();
				InvokeOnMainThread(() =>
				{
					tableSource.UpdateWithBooks(books);
					tableView.ReloadData();
					UpdateState(books);
					state.UpdateDesign();
				});
			});
		}
	}
}


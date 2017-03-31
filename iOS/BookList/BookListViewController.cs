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

		public async override void ViewDidLoad() 
		{
			base.ViewDidLoad();

			viewModel = new BookListViewModel(new RemoteResource());

			SetupDesign();
			UpdateState(null);
			await LoadData();
		}

		private void SetupDesign() 
		{
			tableSource = new BookListTableSource();
            tableSource.viewModel = viewModel;
			emptyView = null;
			activityIndicator.HidesWhenStopped = true;

			tableView.Hidden = true;
			tableView.RegisterNibForCellReuse(BookListTableViewCell.Nib, BookListTableViewCell.Key);
			tableView.EstimatedRowHeight = kEstimatedCellHeight;
			tableView.Source = tableSource;
		}

		private void UpdateState(Book[] books) 
		{
			state = BookListStateFactory.Create(books);
			state.emptyView = emptyView;
			state.tableView = tableView;
			state.mainView = View;
			state.activityIndicator = activityIndicator;
			state.UpdateDesign();
		}

		private async Task LoadData()
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

		private void PresentAlert(Exception exception)
		{
			var alert = Alerter.PresentOKAlert("Oops", exception.Message, this);
			PresentViewController(alert, true, () =>
			{
				UpdateState(new Book[] { });
			});
		}

		private void UpdateDesign(Book[] books) 
		{
			tableSource.UpdateWithBooks(books);
			tableView.ReloadData();
			UpdateState(books);
			state.UpdateDesign();
		}
	}
}


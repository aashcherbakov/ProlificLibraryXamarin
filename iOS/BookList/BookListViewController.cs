using System;

using UIKit;

namespace ProlificLibrary.iOS
{
	public partial class BookListViewController : UIViewController
	{
		private BookListViewModel viewModel;
		private BookListTableSource tableSource;

		public BookListViewController(IntPtr handle) : base(handle) {
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();

			SetupTableView();
			LoadData();
		}


		private void SetupTableView() {
			tableSource = new BookListTableSource();
			tableView.RegisterNibForCellReuse(BookListTableViewCell.Nib, BookListTableViewCell.Key);
			tableView.Source = tableSource;
		}

		private void LoadData() {
			viewModel = new BookListViewModel();
			var books = viewModel.LoadBooks();
			tableSource.UpdateWithBooks(books);
			tableView.ReloadData();
		}
	}
}


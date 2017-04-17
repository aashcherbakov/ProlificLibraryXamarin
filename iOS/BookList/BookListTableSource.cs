using System;
using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace ProlificLibrary.iOS
{

	public delegate void BookSelectionDelegate(Book book);

	public class BookListTableSource: UITableViewSource
	{

        public const string kCellIdentifier = "BookListTableViewCell";
        public BookSelectionDelegate selectionDelegate;
        readonly BookListViewModel viewModel;

        public BookListTableSource(BookListViewModel viewModel) {
            this.viewModel = viewModel;
        } 

        // Overridden functions 

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
			var cell = tableView.DequeueReusableCell(kCellIdentifier, indexPath) as BookListTableViewCell;
			SetupCell(cell, indexPath);
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section) {
            return viewModel.Books?.Count ?? 0;
		}

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var book = GetBookFromArray(indexPath);
            selectionDelegate(book);
        }

        // Private functions

        void SetupCell(BookListTableViewCell cell, NSIndexPath indexPath)
        {
            var book = GetBookFromArray(indexPath);
            cell.Setup(book);
        }

        Book GetBookFromArray(NSIndexPath indexPath)
        {
            if (viewModel.Books.Count < indexPath.Row)
                return null;
         
            return viewModel.Books[indexPath.Row];
        }
    }
}

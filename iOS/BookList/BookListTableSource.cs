using System;
using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace ProlificLibrary.iOS
{

	public delegate void BookSelectionDelegate(Book book);

	public class BookListTableSource: UITableViewSource
	{
        public BookListTableSource() { } // Constructor

        public const string kCellIdentifier = "BookListTableViewCell";
        public BookSelectionDelegate selectionDelegate;

        private Book[] books = new Book[] { };

		public void UpdateWithBooks(Book[] newBooks) {
			books = newBooks;
		}

        // Overridden functions 

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
			var cell = tableView.DequeueReusableCell(kCellIdentifier, indexPath) as BookListTableViewCell;
			SetupCell(cell, indexPath);
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section) {
			return books.Length;
		}

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var book = GetBookFromArray(indexPath);
            selectionDelegate(book);
        }

		// Private functions

		private void SetupCell(BookListTableViewCell cell, NSIndexPath indexPath) {
			var book = GetBookFromArray(indexPath);
			cell.Setup(book);
		}

        private Book GetBookFromArray(NSIndexPath indexPath) {
			if (books.Length < indexPath.Row) {
				return null;
			}

			return books[indexPath.Row];
		}
	}
}

using System;
using Foundation;
using UIKit;

namespace ProlificLibrary.iOS
{
	/// <summary>
	/// Could not create test file because of iOS specific apis 
	/// </summary>
	public class BookListTableSource: UITableViewSource
	{
		public const string kCellIdentifier = "BookListTableViewCell";

		private Book[] books = new Book[] { };

		public BookListTableSource() {
		}

		public void UpdateWithBooks(Book[] newBooks) {
			books = newBooks;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
			var cell = tableView.DequeueReusableCell(kCellIdentifier, indexPath) as BookListTableViewCell;


			SetupCell(cell, indexPath);
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section) {
			return books.Length;
		}

		private void SetupCell(BookListTableViewCell cell, NSIndexPath indexPath) {
			var book = GetBook(indexPath);
			cell.Setup(book);
		}

		private Book GetBook(NSIndexPath indexPath) {
			if (books.Length < indexPath.Row) {
				return null;
			}

			return books[indexPath.Row];
		}
	}
}

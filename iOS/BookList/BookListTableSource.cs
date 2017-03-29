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
		public const string kCellIdentifier = "BookListCell";

		private Book[] books = new Book[] { };

		public BookListTableSource() {
		}

		public void UpdateWithBooks(Book[] newBooks) {
			books = newBooks;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {
			var cell = tableView.DequeueReusableCell(kCellIdentifier, indexPath);
			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, kCellIdentifier);
			}

			SetupCell(cell, indexPath);
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section) {
			return books.Length;
		}

		private void SetupCell(UITableViewCell cell, NSIndexPath indexPath) {
			var book = GetBook(indexPath);
			if (book != null && cell != null) {
				cell.TextLabel.Text = book.title;
				cell.DetailTextLabel.Text = book.author;
			}
		}

		private Book GetBook(NSIndexPath indexPath) {
			if (books.Length < indexPath.Row) {
				return null;
			}

			return books[indexPath.Row];
		}
	}
}

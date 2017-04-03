using System;
using Foundation;
using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using System.Threading.Tasks;

namespace ProlificLibrary.iOS
{

	public class BookListTableSource : MvxSimpleTableViewSource
	{
		public const string kCellIdentifier = "BookListTableViewCell";

	    Book[] books;
        UITableView tableView;


        public BookListTableSource(UITableView tableView) 
            : base(tableView, kCellIdentifier, kCellIdentifier) {

            this.tableView = tableView;
            LoadFakeBooks();
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

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
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

        private void LoadFakeBooks() 
        {
            books = new Book[] {
                new Book("id", "Title", "Author")
            };
        }
	}
}

using System;
using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace ProlificLibrary.iOS
{

	public delegate Book OnCellSelectionDelegate();


	public class BookListTableSource: UITableViewSource
	{
		public const string kCellIdentifier = "BookListTableViewCell";

        public BookListViewModel viewModel;

        public void OnCellSelection(OnCellSelectionDelegate cellDelegete) 
        {
            
        }

		private Book[] books = new Book[] { };

		public BookListTableSource() { } // Constructor

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
            var book = GetBookFromArray(indexPath);

            try 
            {
                Task.Run(async () =>
                {
                    var retrievedBook = await viewModel.GetBook(book.id);
                    Console.WriteLine(retrievedBook.id);
                });
            } 
            catch (Exception exception) 
            {
                Console.WriteLine(exception.Message);
            }
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

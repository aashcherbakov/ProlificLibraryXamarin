using System;
using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace ProlificLibrary.iOS
{

	public delegate void BookSelectionDelegate(Book book);
	public delegate void BookEditingDelegate(Book book);
	public delegate void BookDeletingDelegate(Book book);

	public class BookListTableSource: UITableViewSource
	{
        public const string kCellIdentifier = "BookListTableViewCell";
        public BookSelectionDelegate selectionDelegate;
		public BookEditingDelegate editingDelegate;
		public BookDeletingDelegate deletingDelegate;

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

		public override UITableViewRowAction[] EditActionsForRow(UITableView tableView, NSIndexPath indexPath)		{			var editButton = UITableViewRowAction.Create(
				UITableViewRowActionStyle.Normal, 
				"Edit", 
				(UITableViewRowAction arg1, NSIndexPath arg2) => OnEditCell(arg2)); 

			var deleteButton = UITableViewRowAction.Create(
				UITableViewRowActionStyle.Destructive,
				"Delete", 
				(UITableViewRowAction arg1, NSIndexPath arg2) => OnDeletion(arg2));

			return new UITableViewRowAction[] { deleteButton, editButton };		}

        // Private functions

		void OnEditCell(NSIndexPath indexPath)
		{
			var book = GetBookFromArray(indexPath);
			editingDelegate(book);
		}

		void OnDeletion(NSIndexPath indexPath)
		{
			var book = GetBookFromArray(indexPath);
			deletingDelegate(book);
		}

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

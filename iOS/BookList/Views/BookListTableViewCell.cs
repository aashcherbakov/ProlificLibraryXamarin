using System;

using Foundation;
using UIKit;

namespace ProlificLibrary.iOS
{
	public partial class BookListTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("BookListTableViewCell");
		public static readonly UINib Nib;

		static BookListTableViewCell() {
			Nib = UINib.FromName("BookListTableViewCell", NSBundle.MainBundle);
		}

		protected BookListTableViewCell(IntPtr handle) : base(handle) {
			// Note: this .ctor should not contain any initialization logic.
		}

		public void Setup(Book book) {
			if (book != null) {
				titleLabel.Text = book.title;
				subtitleLabel.Text = book.author;
			}
		}
	}
}

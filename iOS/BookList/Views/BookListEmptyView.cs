using Foundation;
using System;
using UIKit;
using ObjCRuntime;

namespace ProlificLibrary.iOS
{
    public partial class BookListEmptyView : UIView
    {
        public BookListEmptyView (IntPtr handle) : base (handle) { }

		public static BookListEmptyView Create()
		{
			var arr = NSBundle.MainBundle.LoadNib("BookListEmptyView", null, null);
			var v = Runtime.GetNSObject<BookListEmptyView>(arr.ValueAt(0));
			return v;
		}

		public override void AwakeFromNib()
		{
			titleLabel.Text = "Looks like your library is empty :(";
		}
    }
}
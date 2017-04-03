using System;
using System.Collections.Generic;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace ProlificLibrary.iOS
{
    public partial class BookListViewController : MvxViewController<BookListViewModel>
    {
        public BookListViewController() : base("BookListViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var source = new BookListTableSource(tableView);
            this.AddBindings(new Dictionary<object, string> {
                { source, "ItemSource Books"}
            });

            tableView.Source = source;
            tableView.ReloadData();
        }

    }
}


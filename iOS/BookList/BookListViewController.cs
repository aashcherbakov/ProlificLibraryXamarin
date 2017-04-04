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

            // works
            this.AddBindings(new Dictionary<object, string> {
                { source, "ItemSource Books"}
            });

            // Reqires Binding.BindingContext
            var bindingSet = this.CreateBindingSet<BookListViewController, BookListViewModel>();
            bindingSet.Bind(countLabel).To(vm => vm.BooksCount);
            bindingSet.Apply();

            ViewModel.LoadLibrary();

            tableView.Source = source;
			ViewModel.LoadLibrary();

        }

    }
}


using System;
using UIKit;
namespace ProlificLibrary.iOS
{
    public class BookListStateFactory : IBookListStateFactory
    {
        readonly UIView mainView;
        readonly UITableView tableView;
        readonly BookListEmptyView emptyView;
        readonly UIActivityIndicatorView activityIndicator;

        public BookListStateFactory(UIView mainView,
                                    UITableView tableView,
                                    BookListEmptyView emptyView,
                                    UIActivityIndicatorView activityIndicator)
        {
            this.emptyView = emptyView;
            this.mainView = mainView;
            this.tableView = tableView;
            this.activityIndicator = activityIndicator;
        }

        public IBookListState Create(BookListStateOption option)
        {
            BookListState state;
            switch (option)
            {
                case BookListStateOption.Empty:
                    state = new BookListEmptyState(); break;
                case BookListStateOption.Default:
                    state = new BookListDataState(); break;
                case BookListStateOption.Loading:
                    state = new BookListLoadingState(); break;
                default:
                    state = new BookListEmptyState(); break;
            }
            return InjectProperties(state);
        }

        // Private 

        IBookListState InjectProperties(BookListState state)
        {
            state.mainView = mainView;
            state.activityIndicator = activityIndicator;
            state.tableView = tableView;
            state.emptyView = emptyView;
            return state;
        }
    }
}

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
		readonly UIRefreshControl refreshControl;

        public BookListStateFactory(UIView mainView,									UITableView tableView,									BookListEmptyView emptyView,									UIActivityIndicatorView activityIndicator,
		                            UIRefreshControl refreshControl)
        {
            this.emptyView = emptyView;
            this.mainView = mainView;
            this.tableView = tableView;
            this.activityIndicator = activityIndicator;
			this.refreshControl = refreshControl;
        }

        public IBookListState Create(BookListViewModel.State option)
        {
            BookListState state;
            switch (option)
            {
                case BookListViewModel.State.Empty:
                    state = new BookListEmptyState(); break;
                case BookListViewModel.State.Default:
                    state = new BookListDataState(); break;
                case BookListViewModel.State.Loading:
                    state = new BookListLoadingState(); break;
				case BookListViewModel.State.Refreshing:
					state = new BookListRefreshState(); break;
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
			state.refreshControl = refreshControl;
            return state;
        }
    }
}

using System;
using UIKit;

namespace ProlificLibrary.iOS
{
    public abstract class BookListState: IBookListState
	{
		public UIView mainView;
		public UITableView tableView;
		public BookListEmptyView emptyView;
		public UIActivityIndicatorView activityIndicator;

		/// Manipulates visibility of key views and controlls activity indicator.
		public abstract void UpdateDesign();

        public static BookListState Create(BookListStateOption option)
        {
            switch (option)
            {
                case BookListStateOption.Empty:
                    return new BookListEmptyState();
                case BookListStateOption.Default:
                    return new BookListDataState();
                case BookListStateOption.Loading:
                    return new BookListLoadingState();
                default:
                    return new BookListEmptyState();
            }
        }
    }

    public class BookListEmptyState : BookListState
	{
		public override void UpdateDesign() {
			tableView.Hidden = true;

			emptyView = BookListEmptyView.Create();
			emptyView.Frame = mainView.Frame;
			mainView.AddSubview(emptyView);

			activityIndicator.StopAnimating();
		}
	}

	public class BookListDataState : BookListState
	{
        const float kEstimatedCellHeight = 40.0f;

        public override void UpdateDesign() {
			tableView.Hidden = false;

			emptyView?.RemoveFromSuperview();
			emptyView = null;

			activityIndicator.StopAnimating();
            tableView.ReloadData();
		}
	}

	public class BookListLoadingState : BookListState
	{
		public override void UpdateDesign()	{
			tableView.Hidden = true;
			activityIndicator.StartAnimating();
		}
	}
}

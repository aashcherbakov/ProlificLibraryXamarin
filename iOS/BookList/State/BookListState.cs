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
		public UIRefreshControl refreshControl;

		/// Manipulates visibility of key views and controlls activity indicator.
		public abstract void UpdateDesign();
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

			refreshControl.EndRefreshing();
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

	public class BookListRefreshState : BookListState
	{
		public override void UpdateDesign()		{			refreshControl.BeginRefreshing();		}
	}
}

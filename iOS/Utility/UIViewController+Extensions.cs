using System;
using ProlificLibrary.Routing;
using UIKit;

namespace ProlificLibrary.iOS.Utility
{
    public static class UiViewControllerExtensions
    {
        public static void PresentScreen(
            this UIViewController controller,
            IPresenter screen,
            NavigationType navigationType)
        {
            var destinationController = screen as UIViewController;
            if (destinationController == null) return;
            switch (navigationType)
            {
                case NavigationType.Push:
                    controller.NavigationController?.PushViewController(destinationController, true);
                    break;
                case NavigationType.Modal:
                    controller.PresentViewController(destinationController, true, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(navigationType), navigationType, null);
            }
        }
    }
}
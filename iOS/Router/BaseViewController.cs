using System;
using UIKit;

namespace ProlificLibrary.iOS
{
    public class BaseViewController : UIViewController, IPresenter
    {
        public BaseViewController(IntPtr handle)
        {
            Handle = handle;
        }

        public void Present(IPresenter screen, NavigationType navigationType)
        {
            switch (navigationType)
            {
                case NavigationType.Modal:
                    PresentViewController(screen as UIViewController, true, null);
                    break;
                case NavigationType.Push:
                    NavigationController?.PushViewController(screen as UIViewController, true);
                    break;
                default:
                    return;
            };
        }
    }
}

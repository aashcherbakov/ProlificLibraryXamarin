using System;
using ProlificLibrary.Routing;
using UIKit;

namespace ProlificLibrary.iOS.Utility
{
    public class Router: IRouter
    {
        public Router() { }
        
        private readonly ControllerFactory factory = new ControllerFactory();

        public void NavigateTo(Destination destination, IPresenter presenter, ITransferable parameters, NavigationType navigationType)
        {
            var destinationController = factory.CreateController(destination, parameters);
            presenter.Present((IPresenter)destinationController, navigationType);
        }
    }
}

using System;
using System.Linq;
using UIKit;

namespace ProlificLibrary.iOS
{
    public class Router : IRouter
    {
        ControllerFactory factory = new ControllerFactory();

        public Router() { }

        public void NavigateTo(Destination destination, 
                               IPresenter presenter,
                               IDestinationParameters parameters,
                               NavigationType navigationType)
        {
            // create controller
            var controller = ControllerForDestination(destination);

            // inject properties
            var builder = BuilderForDestination(destination);
            builder.Build(controller, parameters);

            // present
            presenter.Present(controller, navigationType);
        }

        // Private functions 

        BaseViewController ControllerForDestination(Destination destination)
        {
            switch (destination)
            {
                case Destination.BookDetails:
                    return factory.Create<BookDetailsViewController>(destination);
                case Destination.BookEdit:
                    return factory.Create<EditBookViewController>(destination);
                case Destination.BookList:
                    return factory.Create<BookListViewController>(destination);
                default:
                    return null;
            }
        }

        IBuilder BuilderForDestination(Destination destination)
        {
            switch (destination)
            {
                case Destination.BookDetails:
                    return new BookDetailsBuilder();
                case Destination.BookEdit:
                    return new EditBookBuilder();
                case Destination.BookList:
                    return new BookListBuilder();
                default:
                    return null;
            }
        }
    }



    public class BookDetailsBuilder : IBuilder
    {
        public void Build<T>(T controller, IDestinationParameters parameters) where T : BaseViewController
        {
            // Unwrapping 
            var bookDetailsController = controller as BookDetailsViewController;
            var detailParameters = parameters as BookDetailParameters;

            // Parameter setup
            var viewModel = new BookDetailsViewModel(detailParameters.Book);

            // General setup
            var router = new Router();
            var resource = new RemoteResource();
            viewModel.InjectDependencies(router, resource);

            bookDetailsController.viewModel = viewModel;
        }
    }

    public class EditBookBuilder : IBuilder
    {
        public void Build<T>(T controller, IDestinationParameters parameters) where T : BaseViewController
        {
            // TODO
        }
    }

    public class BookListBuilder : IBuilder
    {
        public void Build<T>(T controller, IDestinationParameters parameters) where T : BaseViewController
        {
            // TODO
        }
    }




}

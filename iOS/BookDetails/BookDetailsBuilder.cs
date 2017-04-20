using System;
namespace ProlificLibrary.iOS
{
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
}

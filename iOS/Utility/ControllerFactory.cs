using System;
using ProlificLibrary.Routing;
using UIKit;

namespace ProlificLibrary.iOS.Utility
{
    public class ControllerFactory
    {
        public UIViewController CreateController(Destination destination, ITransferable parameters)
        {
            switch (destination)
            {
                case Destination.BookList:
                    break;
                case Destination.BookDetails:
                    return CreateBookDetailsController(parameters);
                case Destination.BookEdit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(destination), destination, null);
            }

            return null;
        }

        private BookDetailsViewController CreateBookDetailsController(ITransferable parameters)
        {
            var storyboard = UIStoryboard.FromName("Main", null);
            var controller =
                storyboard.InstantiateViewController("BookDetailsViewController") as
                    BookDetailsViewController;

            var setupObject = parameters as BookDetailsPayload;
            if (setupObject != null && controller != null)
            {
                var viewModel = new BookDetailsViewModel(setupObject.Book);
                controller.ViewModel = viewModel;
            }

            return controller;
        }
    }
}
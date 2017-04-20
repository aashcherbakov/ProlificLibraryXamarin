using System;
namespace ProlificLibrary
{
    public interface IRouter
    {
        void NavigateTo(
            Destination destination, 
            IPresenter presenter,
            IDestinationParameters parameters,
            NavigationType navigationType);
    }

    public enum Destination
    {
        BookList,
        BookDetails,
        BookEdit
    }

    public enum NavigationType
    {
        Push,
        Modal
    }

    public interface IPresenter
    {
        void Present(IPresenter screen, NavigationType navigationType);
    }

    // Empty interface to unify parameter objects
    public interface IDestinationParameters { }

    public class BookDetailParameters : IDestinationParameters
    {
        public Book Book { get; }

        public BookDetailParameters(Book book)
        {
            Book = book;
        }

    }
}

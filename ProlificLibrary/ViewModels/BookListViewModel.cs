using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProlificLibrary.Networking;
using ProlificLibrary.Networking.Endpoints;
using ProlificLibrary.Routing;

namespace ProlificLibrary.ViewModels
{
    public interface IBookListState
    {
        void UpdateDesign();
    }

    public interface IBookListStateFactory
    {
        IBookListState Create(BookListViewModel.State option);
    }

	public class BookListViewModel
	{
        public enum State
        {
            Default,
            Empty,
            Loading,
			Refreshing
        }

		private readonly IResource resource;
		private readonly IBookListStateFactory stateFactory;
		private IBookListState state;
		private readonly IPresenter presenter;
		private readonly IRouter router;

        public List<Book> Books { get; private set; }
        public readonly string Title = "Library";

        public BookListViewModel(IResource resource, IBookListStateFactory stateFactory, IPresenter presenter, IRouter router) {
			this.resource = resource;
            this.stateFactory = stateFactory;
	        this.presenter = presenter;
	        this.router = router;
        }

		public async Task LoadBooks() 
        {
            UpdateState(State.Loading);
            try 
            {
                var booksArray = await resource.ExecuteRequest<Book[]>(endpoint: new GetBooksEndpoint());
                Books = new List<Book>(booksArray);
                UpdateState(State.Default);
            } 
			catch 
            {
				UpdateState(State.Empty);
				throw new Exception("We could not load books, sorry :(");
            }
		}

		public async Task RefreshBooks()
		{
			UpdateState(State.Refreshing);
			try
			{
                var booksArray = await resource.ExecuteRequest<Book[]>(endpoint: new GetBooksEndpoint());
                Books = new List<Book>(booksArray);
				UpdateState(State.Default);
			}
			catch
			{
				UpdateState(State.Default);
				throw new Exception("We could not load books, sorry :(");
			}
		}

        public async Task<Book> GetBook(string id) 
        {
            return await resource.GetBook(id);
        }

        public void UpdateBookList(Book book)
        {
            var index = Books.FindIndex(x => x.id == book.id);
            if (index != -1)
                Books[index] = book;
            else
                Books.Add(book);
        }

		public async Task DeleteBook(Book book)
		{
            await resource.DeleteBook(book); 
            DeleteBookLocally(book);
		}

		public void SelectBook(ITransferable payload)
		{
			router.NavigateTo(Destination.BookDetails, presenter, payload, NavigationType.Push);
		}

		// Private functions

		private void DeleteBookLocally(Book book)
		{
			var index = Books.FindIndex(x => x.id == book.id);
			if (index != -1)
				Books.Remove(book);
		}

		private void UpdateState(State newState)
        {
            state = stateFactory.Create(newState);
            state.UpdateDesign();
        }
    }
}

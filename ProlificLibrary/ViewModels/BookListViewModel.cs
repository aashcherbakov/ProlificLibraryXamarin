using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProlificLibrary
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

        readonly IResource resource;
        readonly IBookListStateFactory stateFactory;
        IBookListState state;

        public List<Book> Books { get; private set; }
        public readonly string Title = "Library";

        public BookListViewModel(IResource resource, IBookListStateFactory stateFactory) {
			this.resource = resource;
            this.stateFactory = stateFactory;
		}

		public async Task LoadBooks() 
        {
            UpdateState(State.Loading);
            try 
            {
                var booksArray = await resource.GetBooks();
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
				var booksArray = await resource.GetBooks();
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

        // Private functions

        void UpdateState(State newState)
        {
            state = stateFactory.Create(newState);
            state.UpdateDesign();
        }

    }


}

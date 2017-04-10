using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProlificLibrary
{
    public enum BookListStateOption
    {
        Default,
        Empty,
        Loading
    }

    public interface IBookListState
    {
        void UpdateDesign();
    }

    public interface IBookListStateFactory
    {
        IBookListState Create(BookListStateOption option);
    }

	public class BookListViewModel
	{
        readonly IResource resource;
        readonly IBookListStateFactory stateFactory;

        public List<Book> Books { get; private set; }
        public IBookListState State { get; private set; }

        public readonly string Title = "Library";

        public BookListViewModel(IResource resource, IBookListStateFactory stateFactory) {
			this.resource = resource;
            this.stateFactory = stateFactory;
		}

		public async Task<Book[]> LoadBooks() 
        {
            UpdateState(BookListStateOption.Loading);

            var booksArray = await resource.GetBooks();
            Books = new List<Book>(booksArray);

            UpdateState(BookListStateOption.Default);
            return booksArray;
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

        void UpdateState(BookListStateOption option)
        {
            State = stateFactory.Create(option);
            State.UpdateDesign();
        }
	}


}

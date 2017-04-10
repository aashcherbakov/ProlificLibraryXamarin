using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProlificLibrary
{
	public class BookListViewModel
	{
        readonly IResource resource;
        public List<Book> Books { get; private set; }

		public BookListViewModel(IResource resource) {
			this.resource = resource;
		}

		public async Task<Book[]> LoadBooks() 
        {
            var booksArray = await resource.GetBooks();
            Books = new List<Book>(booksArray);
            return booksArray;
		}

        public async Task<Book> GetBook(string id) 
        {
            return await resource.GetBook(id);
        }

        public void UpdateBookList(Book book)
        {
            var index = Books.IndexOf(book);
            if (index != -1)
                Books[index] = book;
            else
                Books.Add(book);
        }
	}
}

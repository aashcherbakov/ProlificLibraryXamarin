using System;
namespace ProlificLibrary
{
	public class BookListViewModel
	{
		public BookListViewModel()
		{
		}

		public Book[] LoadBooks() {
			var books = new Book[] {
				new Book("My Title", "Some Author")
			};

			return books;
		}
	}
}

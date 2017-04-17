using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
	public interface IResource
	{
		Task<Book[]> GetBooks();

        Task<Book> GetBook(string id);

        Task<Book> CheckOutBook(string id, string name);

        Task<Book> AddBook(Book book);

        Task<Book> UpdateBook(Book book);

		Task<Book> DeleteBook(Book book);
	}
}

using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
	public interface IBookService
	{
		Task<Book[]> GetBooks();
        Task<Book> GetBook(string id);
	}
}

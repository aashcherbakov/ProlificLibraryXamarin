using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
	public interface IResource
	{
		Task<Book[]> GetBooks();
        Task<Book> GetBook(string id);
	}
}

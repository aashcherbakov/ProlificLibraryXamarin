using System.Threading.Tasks;
using ProlificLibrary.Networking.Endpoints;

namespace ProlificLibrary.Networking
{
	public interface IResource
	{
        Task<T> ExecuteRequest<T>(IEndpoint endpoint);

        Task<Book> GetBook(string id);

        Task<Book> CheckOutBook(string id, string name);

        Task<Book> UpdateBook(Book book);
 
		Task<Book> DeleteBook(Book book);
	}
}

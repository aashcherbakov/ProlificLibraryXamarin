using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
	public interface IResource
	{
		Task<Book[]> GetBooks();
	}
}

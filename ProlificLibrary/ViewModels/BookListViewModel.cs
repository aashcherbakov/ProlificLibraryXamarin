using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
	public class BookListViewModel
	{
        readonly IResource resource;

		public BookListViewModel(IResource resource) {
			this.resource = resource;
		}

		public async Task<Book[]> LoadBooks() 
        {
			return await resource.GetBooks();
		}

        public async Task<Book> GetBook(string id) 
        {
            return await resource.GetBook(id);
        }

	}
}

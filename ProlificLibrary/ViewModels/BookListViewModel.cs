using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
	public class BookListViewModel
	{
		private IResource resource;

		public BookListViewModel(IResource resource) {
			this.resource = resource;
		}

		public async Task<Book[]> LoadBooks() {
			return await resource.GetBooks();
		}
	}
}

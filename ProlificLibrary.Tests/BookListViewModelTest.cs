using NUnit.Framework;
using System;
namespace ProlificLibrary.Tests
{
	[TestFixture()]
	public class BookListViewModelTest
	{
		[Test()]
		public void TestLoadData() {
			var viewModel = new BookListViewModel();
			var books = viewModel.LoadBooks();
			Assert.AreEqual(books.Length, 1);
			Assert.AreEqual(books[0].author, "Some Author");
		}
	}
}

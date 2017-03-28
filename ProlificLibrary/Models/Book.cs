using System;
namespace ProlificLibrary
{
	public class Book
	{
		public readonly string title;
		public readonly string author;
		public readonly string categories = null;
		public readonly string publisher = null;

		public Book(string title, 
		            string author, 
		            string categories = null, 
		            string publisher = null)
		{
			this.title = title;
			this.author = author;
			this.categories = categories;
			this.publisher = publisher;
		}
	}
}

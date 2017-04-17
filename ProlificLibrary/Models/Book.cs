using System;
namespace ProlificLibrary
{
	public class Book
	{
		public readonly string id;
		public readonly string title;
		public readonly string author;
        public readonly string categories;
		public readonly string publisher;
        public readonly string lastCheckedOut;
        public readonly string lastCheckedOutBy;
        public readonly string url;

        public Book(string title, 
                    string author,
                    string id = null,
                    string categories = null, 
                    string publisher = null, 
                    string lastCheckedOut = null, 
                    string lastCheckedOutBy = null, 
                    string url = null)
		{
            this.id = id;
			this.title = title;
			this.author = author;
			this.categories = categories;
			this.publisher = publisher;
            this.lastCheckedOut = lastCheckedOut;
            this.lastCheckedOutBy = lastCheckedOutBy;
            this.url = url;
        }
    }
}

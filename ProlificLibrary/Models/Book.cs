using System;
namespace ProlificLibrary
{
	public class Book
	{
		public readonly string id;
        public readonly string lastCheckedOut;
        public readonly string lastCheckedOutBy;
        public readonly string url;

        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Categories { get; private set; }
        public string Publisher { get; private set; }

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
			this.Title = title;
			this.Author = author;
			this.Categories = categories;
			this.Publisher = publisher;
            this.lastCheckedOut = lastCheckedOut;
            this.lastCheckedOutBy = lastCheckedOutBy;
            this.url = url;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void UpdateAuthor(string author)
        {
            Author = author;
        }

        public void UpdateCategories(string categories)
        {
            Categories = categories;
        }

        public void UpdatePublisher(string publisher)
        {
            Publisher = publisher;
        }
    }
}

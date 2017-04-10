using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
    public class BookDetailsViewModel
    {
        public Book Book { get; private set; }
        readonly IResource resource = new RemoteResource();

        public BookDetailsViewModel(Book book)
        {
            this.Book = book;
        }

        public String Title {
            get { return Book.title; }
        }

        public String Author {
            get { return Book.author; }
        }

        public String Categories {
            get { return Book.categories; }
        }

        public String Publisher {
            get { return Book.publisher; }
        }

        public String LastCheckedOut {
            get {
                if (Book.lastCheckedOut?.Length > 0)
                    return $"Last checked out: {Book.lastCheckedOut}";
                
                return null;
            }
        }

        public String LastCheckedOutBy {
            get {
                if (Book.lastCheckedOutBy?.Length > 0)
                    return $"By {Book.lastCheckedOutBy}";
                
                return null;
            }
        }

        public async Task CheckoutBook(string name)
        {
            Book = await resource.CheckOutBook(Book.id, name);
        }
    }
}

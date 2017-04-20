using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
    public class BookDetailsViewModel
    {
        // Dependencies
        public Book Book { get; private set; }

        IRouter Router;
        IResource Resource;

        // Getters
        public String Title => Book.Title;
        public String Author => Book.Author;
        public String Categories => Book.Categories;
        public String Publisher => Book.Publisher;

        // Constructor
        public BookDetailsViewModel(Book book)
        {
            Book = book;
        }

        public void InjectDependencies(IRouter router, IResource resource)
        {
            Router = router;
            Resource = resource;
        }

        public void UpdateBook(Book book) { Book = book; }

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
            Book = await Resource.CheckOutBook(Book.id, name);
        }
    }
}

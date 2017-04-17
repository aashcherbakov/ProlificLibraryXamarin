using System;
using System.Threading.Tasks;

namespace ProlificLibrary
{
    public class BookEditViewModel
    {
        readonly IResource resource = new RemoteResource();
        public Book Book { get; private set; }
        public string ScreenTitle { 
            get { return Book == null ? "Create Book" : "Edit Book"; }
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Categories { get; set; }
        public string Publisher { get; set; }

        public BookEditViewModel(Book book = null)
        {
            Book = book;

            if (book != null) {
                Title = book.Title;
                Author = book.Author;
                Categories = book.Categories;
                Publisher = book.Publisher;
            }
        }

        public async Task SubmitChanges()
        {
            if (Book != null) 
                await UpdateBook();
            else 
                await AddBook();
        }

        async Task AddBook() 
        {
            if (HasValidData()) {
                var newBook = new Book(Title, Author, Categories, Publisher);
                Book = await resource.AddBook(newBook);
            } else {
                throw new Exception(Constants.ErrorMessages.RequiredFields);
            }
        }

        async Task UpdateBook()
        {
            if (HasValidData()) {
                Book.UpdateTitle(Title);
                Book.UpdateAuthor(Author);
                Book.UpdatePublisher(Publisher);
                Book.UpdateCategories(Categories);

                Book = await resource.UpdateBook(Book);
                return;
            }

            throw new Exception(Constants.ErrorMessages.RequiredFields);
        }

        bool HasValidData() 
        {
            return IsTextValid(Title) && IsTextValid(Author);
        }

        bool IsTextValid(string text)
        {
            var trimmedText = text.Trim();
            return trimmedText.Length > 0;
        }
    }
}

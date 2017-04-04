using System;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace ProlificLibrary
{
    public class BookListViewModel : MvxViewModel
	{
        readonly IBookService service;
        Book[] books;

        public Book[] Books
        {
            get { return books; }
            private set {
                books = value;
                RaisePropertyChanged(() => Books);
                BooksCount = books.Length;
            }
        }

        int booksCount;
        public int BooksCount
        {
            get { return booksCount; }
            set {
                booksCount = value;
                RaisePropertyChanged(() => BooksCount);
            }
        }

        /// <summary>
        /// Loads the library and assigns new books to observable property.
        /// </summary>
        public void LoadLibrary()
        {
            Task.Run(async () => {
                Books = await service.GetBooks();
            });
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Resource.</param>
        public BookListViewModel(IBookService service) {
			this.service = service;
		}

        /// <summary>
        /// Perform view model initialization - default values, etc.
        /// </summary>
        public override void Start()
        {
            base.Start();
        }
    }
}

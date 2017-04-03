using System;
using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;

namespace ProlificLibrary
{
    public class BookListViewModel : MvxViewModel
	{
        readonly IResource resource;
        Book[] books;

        public Book[] Books
        {
            get { return books; }
            private set {
                books = value;
                RaisePropertyChanged(() => Books);
            }
        }

        /// <summary>
        /// Loads the library and assigns new books to observable property.
        /// </summary>
        public void LoadLibrary()
        {
            Task.Run(async () => books = await resource.GetBooks());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="resource">Resource.</param>
		public BookListViewModel(IResource resource) {
			this.resource = resource;
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

using System;
namespace ProlificLibrary.iOS
{
	public static class BookListStateFactory
	{
		public static BookListState Create(Book[] books)
		{
			if (books == null)
			{
				return new BookListLoadingState();
			}
			else
			{
				if (books.Length == 0)
				{
					return new BookListEmptyState();
				}
				else
				{
					return new BookListDataState();
				}
			}
		}
	}
}

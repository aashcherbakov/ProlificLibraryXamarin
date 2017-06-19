namespace ProlificLibrary.Networking.Endpoints
{
    public class GetBooksEndpoint : IEndpoint
    {
        public string Url { get; } = "books";
        public HttpMethodEnum Method { get; } = HttpMethodEnum.Get;
        public object Payload { get; } = null;
    }

    public class AddBookEndpoint : IEndpoint
    {
        public string Url { get; } = "books";
        public HttpMethodEnum Method { get; } = HttpMethodEnum.Post;
        public object Payload { get; }

        public AddBookEndpoint(Book book)
        {
            Payload = book;
        }
    }
}
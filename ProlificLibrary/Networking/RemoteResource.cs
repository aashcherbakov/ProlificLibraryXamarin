using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProlificLibrary.Networking.Endpoints;

namespace ProlificLibrary.Networking
{
    public class RemoteResource : IResource
    {
        private const string BaseUrl = "http://prolific-interview.herokuapp.com/58c1701210480b000a2948d6/";

        public RemoteResource() { } // constructor

        public async Task<T> ExecuteRequest<T>(IEndpoint endpoint)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = null;
                StringContent content = null;

                var url = BaseUrl + endpoint.Url;

                if (endpoint.Payload != null)
                {
                    var data = JsonConvert.SerializeObject(endpoint.Payload);
                    content = new StringContent(data, Encoding.UTF8, "application/json");
                }

                switch (endpoint.Method)
                {
                    case HttpMethodEnum.Get:
                        result = await client.GetAsync(url);
                        break;
                    case HttpMethodEnum.Post:
                        result = await client.PostAsync(url, content);
                        break;
                    case HttpMethodEnum.Put:
                        result = await client.PutAsync(url, content);
                        break;
                    case HttpMethodEnum.Delete:
                        result = await client.DeleteAsync(url);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (result == null) return default(T);
                var response = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }
        }
       
        public async Task<Book[]> GetBooks()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(BaseUrl + "books");
                var content = await result.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<Book[]>(content);
                return books;
            }
        }

        public async Task<Book> GetBook(string id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(BaseUrl + "books/" + id);
                return await DeserializeResult(result);
            }
        }

        public async Task<Book> CheckOutBook(string id, string name)
        {
            using (var client = new HttpClient())
            {
                var updatedTime = new Dictionary<string, string>() { { "lastCheckedOutBy", name } } ;
                var data = JsonConvert.SerializeObject(updatedTime);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var uri = BaseUrl + "books/" + id + "/";
                var result = await client.PutAsync(uri, content);
                return await DeserializeResult(result);
            }
        }

        public async Task<Book> AddBook(Book book)
        {
            using (var client = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(book);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var uri = BaseUrl + "books";
                var result = await client.PostAsync(uri, content);
                return await DeserializeResult(result);
            }
        }

        public async Task<Book> UpdateBook(Book book)
        {
            using (var client = new HttpClient())
            {
                var updatedFields = UpdateParameters(book);

                var data = JsonConvert.SerializeObject(updatedFields);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var uri = BaseUrl + "books/" + book.id + "/";
                var result = await client.PutAsync(uri, content);
                return await DeserializeResult(result);
            }
        }

        public async Task<Book> DeleteBook(Book book)
        {
            using (var client = new HttpClient())
            {
                var url = BaseUrl + "books/" + book.id;
                var result = await client.DeleteAsync(url);
                return await DeserializeResult(result);
            }
        }

        // Private 

        async Task<Book> DeserializeResult(HttpResponseMessage result)
        {
            var content = await result.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);
            return book;
        }

        Dictionary<string, string> UpdateParameters(Book book)
        {
            var updatedFields = new Dictionary<string, string>() { };

            updatedFields.Add("author", book.Author);
            updatedFields.Add("title", book.Title);

            if (book.Categories != null)
                updatedFields.Add("categories", book.Categories);

            if (book.Publisher != null)
                updatedFields.Add("publisher", book.Publisher);

            return updatedFields;
        }
    }   
}

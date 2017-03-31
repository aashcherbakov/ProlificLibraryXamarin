using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProlificLibrary
{
    public class RemoteResource : IResource
    {
        private const string kBaseUrl = "http://prolific-interview.herokuapp.com/58c1701210480b000a2948d6/";

        public RemoteResource() { } // constructor

        public async Task<Book[]> GetBooks()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(kBaseUrl + "books");
                var content = await result.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<Book[]>(content);
                return books;
            }
        }

        public async Task<Book> GetBook(string id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(kBaseUrl + "books/" + id);
                var content = await result.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<Book>(content);
                return book;
            }
        }
    }	
}

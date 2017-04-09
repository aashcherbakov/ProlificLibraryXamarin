﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public async Task<Book> CheckOutBook(string id, string name)
        {
            using (var client = new HttpClient())
            {
                var updatedTime = new Dictionary<string, string>() { { "lastCheckedOutBy", name } } ;
                var data = JsonConvert.SerializeObject(updatedTime);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                var uri = kBaseUrl + "books/" + id + "/";
                var result = await client.PutAsync(uri, content);
                var response = await result.Content.ReadAsStringAsync();
                var book = JsonConvert.DeserializeObject<Book>(response);
                return book;
            }
        }
    }	
}

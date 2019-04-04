using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;
using GoT.Models;
using Newtonsoft.Json;

namespace GoT.Services
{

    class GoTService
    {
        private readonly Uri serverUrl = new Uri("https://www.anapioficeandfire.com");

        public async Task<List<Book>> GetBooksAsync(int page)
        {
            return await GetAsync<List<Book>>(new Uri(serverUrl, "/api/books/?pagesize=50&page="+page.ToString()));
        }

        public async Task<List<Character>> GetCharactersAsync(int page)
        {
            return await GetAsync<List<Character>>(new Uri(serverUrl, "/api/characters/?pagesize=50&page=" + page.ToString()));
        }

        public async Task<List<House>> GetHousesAsync(int page)
        {
            return await GetAsync<List<House>>(new Uri(serverUrl, "/api/houses/?pagesize=50&page=" + page.ToString()));
        }

        public async Task<Book> GetBookAsync(string code)
        {
            return await GetAsync<Book>(new Uri(code));
        }

        public async Task<Character> GetCharacterAsync(string code)
        {
            return await GetAsync<Character>(new Uri(code));
        }

        public async Task<House> GetHouseAsync(string code)
        {
            return await GetAsync<House>(new Uri(code));
        }

        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }
    }
}

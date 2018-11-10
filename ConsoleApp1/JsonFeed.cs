using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url = "";
        static int _results = 42; // joke_id
        
        public JsonFeed() { }
        public JsonFeed(string endpoint, int results)
        {
            _url = endpoint;
            _results = results; 
        }
        
		public static string[] GetRandomJokes(string firstname, string lastname, string category)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "jokes/";
            string[] jokes = new string[_results];

            for(int i =  0; i < _results; i++)
            {
                int id = (new Random()).Next(1, 603);
                //TODO: handle id exception
                url += id.ToString();
                if (firstname != null)
                {
                    if (url.Contains('?'))
                        url += "&";
                    else url += "?";
                    url += "firstName=";
                    url += firstname.ToString();
                }
                if (lastname != null)
                {
                    if (url.Contains('?'))
                        url += "&";
                    else url += "?";
                    url += "lastName=";
                    url += lastname.ToString();
                }
                if (category != null)
                {
                    if (url.Contains('?'))
                        url += "&";
                    else url += "?";
                    url += "limitTo=[";
                    url += category;
                    url += "]";
                }
                jokes[i] = Task.FromResult(client.GetStringAsync(url).Result).Result;
            }            

			return jokes;
		}

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static dynamic GetName()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			var result = client.GetStringAsync("").Result;
			return JsonConvert.DeserializeObject<dynamic>(result);
		}

		public static string[] GetCategories()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);

			return new string[] { Task.FromResult(result: client.GetStringAsync("categories").Result).Result };
		}
    }
}

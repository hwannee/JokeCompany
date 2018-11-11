using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        private static string _url = "";
        private static int _results = 42; // Joke id
        private static Random _random = new Random();

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
            string url;
            int id;
            string[] jokes = new string[_results];
                        
            for (int i = 0; i<_results; i++)
            {
                url = "jokes/";
                id = _random.Next(5, 604);
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
                if (jokes[i].Contains("Exception"))
                {
                    //Console.WriteLine(String.Format("Invalid id({0}). Repeating...", id));
                    i--;
                }
            }
			return jokes;
		}

        private static int randomId(int min, int max)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int n = random.Next();
            Console.WriteLine(n);
            n = n % max + 1;
            Console.WriteLine(n);
            return n;
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

using System;
using System.Net.Http;
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
            _results = results; // update _result to get the number of jokes
        }

        public static string[] GetRandomJokes(string firstname, string lastname, string category)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            string url = "jokes?";
            
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
            // Extract all data at once to avoid retrial for invalid id or categories.
            string str = Task.FromResult(client.GetStringAsync(url).Result).Result;
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(str);
            // Random choose number of jokes and add to results string.            
            string[] results = new string[_results];
            for (int i = 0; i < _results; i++)
            {
                int randomIndex = _random.Next(0, obj.value.Count);
                string result = obj.value[randomIndex].joke;                
                results[i] = result;
            }
            return results;
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

            string str = Task.FromResult(client.GetStringAsync("categories").Result).Result;
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(str);
            string[] results = new string[obj.value.Count];
            for (int i = 0; i < obj.value.Count; i++)
            {
                results[i] = obj.value[i];
            }

            //return new string[] { Task.FromResult(result: client.GetStringAsync("categories").Result).Result };
            return results;
		}
    }
}
